using System;

using System.Collections.Generic;
using System.Text;

namespace Microsoft.WindowsMobile.SharedSource.Bluetooth
{
    public class BluetoothAddress
    {
        byte[] bBTAddress;
        public BluetoothAddress()
        {
            bBTAddress = new byte[8];
        }

        public BluetoothAddress(byte[] btAddress)
        {
            if (btAddress.Length == 8)
                this.bBTAddress = btAddress;
        }
        
        public BluetoothAddress(string sBtAddress)
        {
            if (sBtAddress.Length != 12 && sBtAddress.Length != 16)
            {
                this.bBTAddress = new byte[8];
                return;
            }
            List<byte> bList = new List<byte>();
            for(int i=sBtAddress.Length-1;i>0;i-=2){
                bList.Add(Convert.ToByte(sBtAddress.Substring(i-1, 2),16));
            }
            this.bBTAddress = bList.ToArray();
        }

        public override string ToString()
        {
            string s = "";
            for (int i = bBTAddress.Length - 1; i >= 0; i--)
                s += bBTAddress[i].ToString("x02");
            return s;
        }

        public byte[] BTAddress
        {
            get
            {
                return bBTAddress;
            }
            set
            {
                if (value.Length == 8)
                    bBTAddress = value;
            }
        }
    }
}
