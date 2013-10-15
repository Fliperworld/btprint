using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;


namespace Microsoft.WindowsMobile.SharedSource.Bluetooth
{
    // http://dalelane.co.uk/files/BTSecure.cs.txt
    public class BluetoothMessages
    {
        public BluetoothMessages()
        {
            this.RegisterForBluetoothNotifications();
        }

        // if we are listening to Bluetooth stack when disposing, 
        //  cleanup first
        ~BluetoothMessages()
        {
            UnregisterForBluetoothNotifications();
        }


        //----------------------------------------------------------
        // --- BLUETOOTH CODE ---
        //----------------------------------------------------------

        // a c# wrapper for the C++ Windows API call
        //  RequestBluetoothNotifications
        //
        // creates a queue where Bluetooth notifications can go, and
        //  starts a thread that will wait for messages to appear on 
        //  that queue
        public void RegisterForBluetoothNotifications()
        {
            /* define the queue we will create for Bluetooth */
            /*  notification messages                        */

            MSGQUEUEOPTIONS mqo = new MSGQUEUEOPTIONS();
            
            mqo.dwSize = Marshal.SizeOf(mqo);
            // allocate message buffers on demand and free after reading
            mqo.dwFlags = 0;// MSGQUEUE_NOPRECOMMIT;
            // number of messages to allow to build up on queue
            mqo.dwMaxMessages = 10;
            // max size of a message
            mqo.cbMaxMessage = SIZEOF_BTEVENT;
            // we will only be getting messages from the queue
            mqo.bReadAccess = true;

            /* create the message queue and store handle */
            msgQueueHandle = CreateMsgQueue(IntPtr.Zero, ref mqo);
            utils.ddump("msgQueue created: handle="+msgQueueHandle.ToInt32().ToString("x"));

            /* register for BTE_CONNECTION and BTE_DISCONNECTION events */

            notificationHandle = RequestBluetoothNotifications(
                BTE_CLASS.ALL,
                //BTE_CLASS.CONNECTIONS | BTE_CLASS.DEVICE | BTE_CLASS.PAIRING | BTE_CLASS.STACK,
                                                               msgQueueHandle);
            utils.ddump("BT notification requested: "+notificationHandle.ToInt32().ToString("x"));

            /* create and start a background thread which will wait for */
            /* (block on) messages from this Queue                      */
            Thread t = new Thread(new ThreadStart(BluetoothEventThread));
            t.IsBackground = true;
            t.Start();
        }

        // informs operating system to stop putting Bluetooth notification
        //   messages on our message queue, and close the queue handle
        public void UnregisterForBluetoothNotifications()
        {
            // we add a call to this in the class destructor, so we 
            //  protect against calling this when we aren't actually
            //  registered here

            if (notificationHandle != IntPtr.Zero)
            {
                StopBluetoothNotifications(notificationHandle);
                notificationHandle = IntPtr.Zero;
            }

            if (msgQueueHandle != IntPtr.Zero)
            {
                CloseMsgQueue(msgQueueHandle);
                msgQueueHandle = IntPtr.Zero;
            }
        }

        // method run by a background thread to wait for Bluetooth 
        //  notification messages
        //
        // calls ReadMsgQueue which blocks until a message is 
        //  available
        // 
        // at exit/cleanup time, the queue will be closed causing
        //  this method to break out of this endless blocking 
        //  throwing an exception - which will be swallowed silently 
        //  by the try...finally 
        private void BluetoothEventThread()
        {
            // allocate space to store the received messages
            IntPtr msgBuffer = Marshal.AllocHGlobal(SIZEOF_BTEVENT);
            utils.ddump("BluetoothEventThread starting");

            try
            {
                // initialise values returned by ReadMsgQueue
                int bytesRead = 0;
                int msgProperties = 0;

                // keep waiting for messages as long as we have
                //  a valid queue handle

                while (msgQueueHandle != IntPtr.Zero)
                {
                    // wait for the next message to arrive on the queue
                    utils.ddump("waiting for msg...");
                    bool success = ReadMsgQueue(msgQueueHandle,   // the open message queue
                                                msgBuffer,        // buffer to store msg
                                                SIZEOF_BTEVENT,   // size of the buffer
                                                out bytesRead,    // bytes stored in buffer
                                                INFINITE,         // wait forever
                                                out msgProperties);

                    if (success)
                    {
                        // marshal the data read from the queue into a BTEVENT structure
                        BTEVENT bte = (BTEVENT)Marshal.PtrToStructure(msgBuffer, typeof(BTEVENT));

                        utils.ddump("New message: " + bte.dwEventId);

                        // we are interested in the event type
                        if (bte.dwEventId == BTE_ID.DISCONNECTION)
                        {
                            // a Bluetooth device has disconnected - lock!
                            //LockDevice();
                        }
                    }

                }
                utils.ddump("NO msq queue!");
            }
            finally
            {
                Marshal.FreeHGlobal(msgBuffer);
            }
        }

        // used to store a pointer we get back from 
        //  RequestBluetoothNotifications, so that we 
        //  can use the same pointer at exit/cleanup 
        //  in StopBluetoothNotifications
        private IntPtr notificationHandle = IntPtr.Zero;

        // stores a pointer to the queue where 
        //  bluetooth notification messages will be
        //  put
        private IntPtr msgQueueHandle = IntPtr.Zero;

        // information about the queue we are waiting
        //  for bluetooth notifications on
        //
        // p/invoke definition based on typedef in
        //  http://msdn.microsoft.com/en-us/library/ms886759.aspx
        [StructLayout(LayoutKind.Sequential)]
        internal struct MSGQUEUEOPTIONS
        {
            internal int dwSize;
            internal int dwFlags;
            internal int dwMaxMessages;
            internal int cbMaxMessage;
            [MarshalAs(UnmanagedType.Bool)]
            internal bool bReadAccess;
        }
        // WINBASE.h header constants
        private const int MSGQUEUE_NOPRECOMMIT = 1;
        private const int MSGQUEUE_ALLOW_BROKEN = 2;

        // MSGQUEUEOPTIONS constants
        private const bool ACCESS_READWRITE = false;
        private const bool ACCESS_READONLY = true;


        // Bluetooth event class
        //  definitions copied from 
        //   http://msdn.microsoft.com/en-us/library/aa916855.aspx
        internal enum BTE_CLASS
        {
            CONNECTIONS = 1,
            BTE_CONNECTION = 100,
            BTE_DISCONNECTION = 101,
            BTE_ROLE_SWITCH = 102,
            BTE_MODE_CHANGE = 103,
            BTE_PAGE_TIMEOUT = 104,
            PAIRING = 2,
            BTE_KEY_NOTIFY = 200,
            BTE_KEY_REVOKED = 201,
            DEVICE = 4,
            BTE_LOCAL_NAME = 300,
            BTE_COD = 301,
            STACK = 8,
            BTE_STACK_UP = 400,
            BTE_STACK_DOWN = 401,
            AVDTP = 16,
            PAN = 32,
            SERVICE = 64,
            ALL = 255,
        }

        // information about the Bluetooth notification
        //  received from the OS
        //
        // p/invoke definition based on typedef in 
        //  http://msdn.microsoft.com/en-us/library/aa916247.aspx
        [StructLayout(LayoutKind.Sequential)]
        internal struct BTEVENT
        {
            internal BTE_ID dwEventId;
            internal int dwReserved;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            internal byte[] baEventData;
        }
        // size of a BTEVENT structure:
        //   DWORD dwEventId           = 4 bytes
        //   DWORD dwReserved          = 4 bytes
        //   BYTE  baEventData[64]     = 64 bytes
        private const int SIZEOF_BTEVENT = 72;

        // bluetooth event types
        // 
        // p/invoke definition based on table in MSDN doc
        //  http://msdn.microsoft.com/en-us/library/aa916855.aspx
        internal enum BTE_ID
        {
            // dwEventId values valid for BTE_CLASS.CONNECTIONS
            CONNECTION = 100,
            DISCONNECTION = 101,
            ROLE_SWITCH = 102,
            MODE_CHANGE = 103,
            PAGE_TIMEOUT = 104,

            // dwEventId values valid for BTE_CLASS.PAIRING
            KEY_NOTIFY = 200,
            KEY_REVOKED = 201,

            // dwEventId values valid for BTE_CLASS.DEVICE
            LOCAL_NAME = 300,
            COD = 301,

            // dwEventId values valid for BTE_CLASS.STACK
            STACK_UP = 400,
            STACK_DOWN = 401,

            // dwEventId values valid for BTE_CLASS.AVDTP
            STATE = 500,
        }

        // ReadMsgQueue constants
        //  read operation should block until data is 
        //  available or the status of the queue changes
        private const int INFINITE = -1;


        // p/invoke definitions        

        // definition taken from http://msdn.microsoft.com/en-us/library/bb202792.aspx
        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr CreateMsgQueue(string lpszName, ref MSGQUEUEOPTIONS lpOptions);
        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr CreateMsgQueue(IntPtr hString, ref MSGQUEUEOPTIONS lpOptions);

        // definition taken from http://msdn.microsoft.com/en-us/library/aa909162.aspx
        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern bool ReadMsgQueue(IntPtr hMsgQ, IntPtr lpBuffer, int cbBufferSize, out int lpNumberOfBytesRead, int dwTimeout, out int pdwFlags);
        // definition taken from http://msdn.microsoft.com/en-us/library/aa915038.aspx
        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern bool CloseMsgQueue(IntPtr hMsgQ);

        // p/invoke definitions for Bluetooth Notification API

        // definition taken from http://msdn.microsoft.com/en-us/library/aa916492.aspx
        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr RequestBluetoothNotifications(BTE_CLASS dwClass, IntPtr hMsgQ);
        // definition taken from http://msdn.microsoft.com/en-us/library/aa915853.aspx
        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern bool StopBluetoothNotifications(IntPtr h);


        //-------------------------------------------------------------
        // -- LOCKING CODE --
        //-------------------------------------------------------------

        public void LockDevice()
        {
            SHDeviceLockAndPrompt();
        }

        // definition taken from http://msdn.microsoft.com/en-us/library/bb160713.aspx
        [DllImport("aygshell.dll", SetLastError = true)]
        private extern static IntPtr SHDeviceLockAndPrompt();
    }
}
