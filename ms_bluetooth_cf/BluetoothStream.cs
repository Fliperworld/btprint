using System;

using System.Collections.Generic;
using System.Text;

using System.Threading;
using System.Net.Sockets;

namespace Microsoft.WindowsMobile.SharedSource.Bluetooth
{
    public class BluetoothStream:IDisposable
    {
//        BluetoothDevice bluetoothdevice = null;
        Socket socket=null;
        string _sPIN = String.Empty;
        byte[] _btAddress;
        NetworkStream networkstream=null;
        Guid guid_spp = StandardServices.SerialPortServiceGuid;

        Encoding encoder = Encoding.UTF8;

        bool connected = false;
        Thread thSend;
        Thread thRecv;
        bool bRunThreads = true;
        Queue<byte[]> sendQueue=new Queue<byte[]>();
        Queue<byte[]> recvQueue = new Queue<byte[]>();
        object lockSend = new object();
        object lockRecv = new object();

        public delegate void recvThreadEventHandler(object sender, BluetoothStreamEventArgs bse);
        public event recvThreadEventHandler recvThreadEvent;

        public bool _connected
        {
            get
            {
                if (socket != null)
                    return this.socket.Connected;
                else
                    return false;
            }
        }

        void setPIN(byte[] btAddress, string sPIN)
        {
            //see registry
            // [HKEY_LOCAL_MACHINE\Comm\Security\Bluetooth]
            // "key0006660309e8"=.....
            byte[] bPIN = encoder.GetBytes(sPIN);
            int iRes = SafeNativeMethods.BthSetPIN(btAddress, bPIN.Length, bPIN);
            if (iRes != 0)
                throw new ArgumentException("Setting Pin '" + sPIN + "' failed");

            iRes = SafeNativeMethods.BthPairRequest(btAddress, bPIN.Length, bPIN);
            if (iRes != 0)
                throw new ArgumentException("BthPairRequest failed with code " + iRes.ToString());
            _sPIN = sPIN;
        }
        void revokePIN()
        {
            SafeNativeMethods.BthRevokePIN(_btAddress);
        }
        public BluetoothStream(byte[] btAddress):this(btAddress,string.Empty)
        {
        }
        public BluetoothStream(byte[] btAddress, string sPin)//:this(btAddress)
        {
            _btAddress = btAddress;
            if (sPin != String.Empty && sPin != "")
                setPIN(btAddress, sPin);
            socket = new Socket((AddressFamily)32, SocketType.Stream, (ProtocolType)3);
            try
            {
                BluetoothEndPoint btep = new BluetoothEndPoint(btAddress, guid_spp);
                socket.Connect(btep);
                
                networkstream = new NetworkStream(socket, true);

                thSend = new Thread(new ThreadStart(sendThread));
                thSend.Name = "SendThread";
                thSend.Start();
                utils.ddump("Thread started: " + thSend.Name + ", " + thSend.ManagedThreadId.ToString());

                thRecv = new Thread(new ThreadStart(recvThread));
                thRecv.Name = "RecvThread";
                thRecv.Start();
                utils.ddump("Thread started: " + thRecv.Name + ", " + thRecv.ManagedThreadId.ToString());
            }
            catch (Exception ex)
            {
                connected = false;
                utils.ddump("BluetoothStream init failed with "+ex.Message);
            }
        }
        
        public BluetoothStream(BluetoothDevice btdev)
        {
            socket = new Socket((AddressFamily)32, SocketType.Stream, (ProtocolType)3);
            byte[] btAddress = btdev.Address;
            try
            {
                BluetoothEndPoint btep = new BluetoothEndPoint(btAddress, guid_spp);
                utils.ddump("connecting to " + btdev.AddressStr);
                socket.Connect(btep);
                utils.ddump("connected! Create networkstream...");
                networkstream = new NetworkStream(socket, true);

                utils.ddump("starting threads...");
                thSend = new Thread(new ThreadStart(sendThread));
                thSend.Name = "SendThread"; thSend.IsBackground = true;
                thSend.Start();
                utils.ddump("Thread started: " + thSend.Name + ", " + thSend.ManagedThreadId.ToString("x08"));

                thRecv = new Thread(new ThreadStart(recvThread));
                thRecv.Name = "RecvThread"; thRecv.IsBackground = true;
                thRecv.Start();
                utils.ddump("Thread started: " + thRecv.Name + ", " + thRecv.ManagedThreadId.ToString("x08"));
            }
            catch (Exception ex)
            {
                connected = false;
                utils.ddump("BluetoothStream init failed with " + ex.Message);
            }
        }

        public void Dispose()
        {
            bRunThreads = false;
            if (socket.Connected)
            {
                socket.Shutdown(SocketShutdown.Both);
                networkstream.Close();
                socket.Close();
                thRecv.Abort();
                thSend.Abort();
            }
            if(_sPIN!=String.Empty)
                this.revokePIN();
            utils.ddump("BluetoothStream disposed");
        }

        public void write(string s)
        {
            lock (lockSend){
                sendQueue.Enqueue(encoder.GetBytes(s));
            }
        }

        void sendThread()
        {
            utils.ddump("#### sendThread started");
            try
            {
                while (bRunThreads)
                {
                    lock (lockSend)
                    {
                        if (sendQueue.Count > 0)
                        {
                            byte[] buf = sendQueue.Dequeue();
                            utils.ddump("dequeued " + buf.Length.ToString() + " bytes from sendQueue"); 
                            socket.Send(buf);
                        }
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (ThreadAbortException ex)
            {
                utils.ddump("sendThread, ThreadAbortException: " + ex.Message);
            }
            catch (Exception ex)
            {
                utils.ddump("sendThread, Exception: " + ex.Message);
            }
            utils.ddump("#### sendThread ended");
            return;
        }

        void recvThread()
        {
            utils.ddump("#### recvThread started");
            try
            {
                while (bRunThreads)
                {
                    if (networkstream.DataAvailable)
                    {
                        byte[] buf = new byte[8192];
                        int iCnt = networkstream.Read(buf, 0, buf.Length);
                        byte[] buf1 = new byte[iCnt];
                        Array.Copy(buf, buf1, iCnt);
                        lock (lockRecv)
                        {
                            recvQueue.Enqueue(buf1);
                            utils.ddump("enqueued " + buf1.Length.ToString() + " bytes into recvQueue");
                        }
                        this.onNewData(encoder.GetString(buf1,0,buf1.Length).Replace("\n\n","\r\n"));
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (ThreadAbortException ex)
            {
                utils.ddump("recvThread, ThreadAbortException: " + ex.Message);
            }
            catch (Exception ex)
            {
                utils.ddump("recvThread, Exception: " + ex.Message);
            }
            utils.ddump("#### recvThread ended");
            return;
        }

        private void onNewData(string s)
        {
            if (this.recvThreadEvent == null)
            {
                return;
            }
            else
            {
                this.recvThreadEvent(this, new BluetoothStreamEventArgs(s));
            }
        }
    }
}
