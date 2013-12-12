using System;

using System.Collections.Generic;
using System.Text;

namespace Microsoft.WindowsMobile.SharedSource.Bluetooth
{
    class utils
    {
        public static void ddump(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }
        public static bool isIntermec()
        {
            bool bRet = false;
            try
            {
                if (System.IO.File.Exists(@"\windows\ibt.dll"))
                    bRet = true;
                else
                    bRet = false;
            }
            catch (Exception) { }
            return bRet;
        }
    }
}
