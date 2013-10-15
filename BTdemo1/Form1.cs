using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.WindowsMobile.SharedSource.Bluetooth;
using Microsoft.WindowsMobile.SharedSource;

namespace BTdemo1
{
    public partial class Form1 : Form
    {
        BluetoothRadio btr;
        BluetoothDeviceCollection btdc;

        public Form1()
        {
            InitializeComponent();
            btr = new BluetoothRadio();
            //first use intermec IBT_On()
            btr.BluetoothRadioMode = BluetoothRadioMode.On;
            listPairedDevices();

            BluetoothAddress btAddr = new BluetoothAddress("0000000006660309");
            ddump("btAddress: " + btAddr.ToString());

            BluetoothMessages btmsg = new BluetoothMessages();
        }

        void listPairedDevices()
        {
            listBox1.Items.Clear();
            ddump("Local bt mac: "+btr.getLocalBtAddressStr());
            btdc = btr.PairedDevices;
            foreach (BluetoothDevice bd in btdc)
            {
                ddump("Found paired device: " + bd.AddressStr + ", '" + bd.Name + "'");
                listBox1.Items.Add(bd);
            }
        }

        void ddump(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
            addLog(s);
        }

        BluetoothStream btstream;
        void testConnect2(BluetoothDevice btdev)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                if (button1.Text == "print")
                {
                    btstream = new BluetoothStream(btdev);
                    btstream.recvThreadEvent += new BluetoothStream.recvThreadEventHandler(btstream_recvThreadEvent);
                    int iTry = 3;
                    while (iTry > 0 && btstream._connected == false)
                    {
                        System.Threading.Thread.Sleep(1000);
                        iTry--;
                    }
                    if (btstream._connected)
                    {
                        if(radioFP.Checked)
                            btstream.write(Intermec.Printer.Language.Fingerprint.Demo.FP_2_WalmartLabel());
                        else if(radioIPL.Checked)
                            btstream.write(Intermec.Printer.Language.Fingerprint.Demo.IPL_2_WalmartLabel());
                        button1.Text = "disconnect";
                    }
                    else
                        btstream.Dispose();
                }
                else if (button1.Text == "disconnect")
                {
                    btstream.recvThreadEvent -= btstream_recvThreadEvent;
                    btstream.Dispose();
                    button1.Text = "print";
                }
            }
            catch (Exception ex)
            {
                ddump("testConnect2 Exception " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                Application.DoEvents();
            }
        }

        void btstream_recvThreadEvent(object sender, BluetoothStreamEventArgs bse)
        {
            ddump("recvThreadEvent: " + bse._data);
        }

        void testConnect(BluetoothDevice btdev)
        {
            byte[] bta = new byte[8];
            bta[0] = 0x00;
            bta[1] = 0x00;
            bta[2] = 0x00;
            bta[3] = 0x06;
            bta[4] = 0x66;
            bta[5] = 0x03;
            bta[6] = 0x09;
            bta[7] = 0xE8;

            BluetoothDevice btd = btdev;// new BluetoothDevice("test", bta);
            System.Net.Sockets.NetworkStream nws;
            //first download the layout program
            byte[] buf;
            try
            {
                ddump("connecting to "+btd.AddressStr);
                nws = btd.Connect(StandardServices.SerialPortServiceGuid);
                if (nws == null)
                {
                    ddump("Connect failed!");
                    return;
                }
                buf = null;
                if(radioFP.Checked)
                    buf = Encoding.Unicode.GetBytes(Intermec.Printer.Language.Fingerprint.Demo.FP_2_WalmartLabel());
                else if(radioIPL.Checked)
                    buf = Encoding.Unicode.GetBytes(Intermec.Printer.Language.Fingerprint.Demo.IPL_2_WalmartLabel());
                if (buf == null)
                {
                    return;
                }
                ddump("writing 1...");
                nws.Write(buf, 0, buf.Length);                
                ddump("...writing 1 done");
                
                if (nws.DataAvailable)
                {
                    ddump("reading...");
                    readData(nws);
                    ddump("...reading done");
                }
                
                ddump("closing net stream...");
                nws.Close();
                ddump("... net stream closed");
                
            }
            catch (System.IO.IOException ex)
            {
                ddump("Exception: " + ex.Message);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                ddump("Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                ddump("Exception: " + ex.Message);
            }
        }

        void readData(System.Net.Sockets.NetworkStream nws)
        {
            byte[] buf=new byte[8192];
            int iCount = nws.Read(buf, 0, 8192);
            string s = Encoding.ASCII.GetString(buf, 0, iCount);
            ddump("Read done: '" + s.Replace("\n\n","\r\n") + "'");
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                ddump("No device entry selected!");
                return;
            }
            BluetoothDevice bdev = (BluetoothDevice)listBox1.SelectedItem;
            testConnect(bdev);
        }

        delegate void SetTextCallback(string text);
        public void addLog(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(addLog);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (txtLog.Text.Length > 2000)
                    txtLog.Text = "";
                txtLog.Text += text + "\r\n";
                txtLog.SelectionLength = 0;
                txtLog.SelectionStart = txtLog.Text.Length - 1;
                txtLog.ScrollToCaret();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                ddump("No device entry selected!");
                return;
            }
            BluetoothDevice bdev = (BluetoothDevice)listBox1.SelectedItem;
            testConnect2(bdev);
        }

        private void btnAgain_Click(object sender, EventArgs e)
        {
            if (btstream == null)
            {
                ddump("not connected!");
                return;
            }
            if (btstream._connected)
            {
                ddump("printing again...");
                if(radioIPL.Checked)
                    btstream.write(Intermec.Printer.Language.Fingerprint.Demo.IPL_2_WalmartLabel());// FP_2_WalmartLabel());
                else if(radioFP.Checked)
                    btstream.write(Intermec.Printer.Language.Fingerprint.Demo.FP_2_WalmartLabel());
            }
            else
                ddump("not connected!");
        }

    }
}