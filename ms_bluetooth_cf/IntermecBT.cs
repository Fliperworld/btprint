using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ms_bluetooth_cf
{
    class IntermecBT
    {
        [DllImport("ibt.dll", SetLastError = true)]
        static extern UInt32 IBT_On();

        [DllImport("ibt.dll", SetLastError = true)]
        static extern UInt32 IBT_Off();

        [DllImport("ibt.dll", SetLastError = true)]
        static extern UInt32 IBT_GetPower(ref bool bOnOff);

        [DllImport("ibt.dll", SetLastError = true)]
        static extern UInt32 IBT_SetDiscoverable(bool bOnOff);
 
        public static bool ibt_on()
        {
            if(Microsoft.WindowsMobile.SharedSource.Bluetooth.utils.isIntermec()){
                UInt32 uRes = IBT_On();
                if (uRes == 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool ibt_off()
        {
            if (Microsoft.WindowsMobile.SharedSource.Bluetooth.utils.isIntermec())
            {
                UInt32 uRes = IBT_Off();
                if (uRes == 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool get_bt_power()
        {
            if (Microsoft.WindowsMobile.SharedSource.Bluetooth.utils.isIntermec())
            {
                bool bPower = false;
                UInt32 uRes = IBT_GetPower(ref bPower);
                if (uRes == 0)
                    return bPower;
                else
                    return false;
            }
            else
                return false;

        }

        public static bool set_bt_discoverable(bool bOnOff)
        {
            bool bRet = false;
            UInt32 uRes = IBT_SetDiscoverable(bOnOff);
            if (uRes == 0)
                bRet = true;
            return bRet;
        }
    }
}
