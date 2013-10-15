using System;

using System.Collections.Generic;
using System.Text;

namespace Microsoft.WindowsMobile.SharedSource.Bluetooth
{
    public class BluetoothStreamEventArgs : EventArgs
    {
        #region fields
        string data;
        public string _data
        {
            get { return data; }
        }
        #endregion
        public BluetoothStreamEventArgs(string s)
        {
            this.data = s;
        }
    }
}
