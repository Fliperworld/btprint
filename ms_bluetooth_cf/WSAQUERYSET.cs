using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace Microsoft.WindowsMobile.SharedSource.Bluetooth
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    struct WSAQUERYSET
    {
        public Int32 dwSize;                    //0
        public String szServiceInstanceName;    //4
        public IntPtr lpServiceClassId;         //8
        public IntPtr lpVersion;                //12
        public String lpszComment;
        public Int32 dwNameSpace;
        public IntPtr lpNSProviderId;
        public String lpszContext;
        public Int32 dwNumberOfProtocols;
        public IntPtr lpafpProtocols;
        public IntPtr lpszQueryString;
        public Int32 dwNumberOfCsAddrs;
        public IntPtr lpcsaBuffer;
        public Int32 dwOutputFlags;
        public IntPtr Blob;
    }
    public class myWSAQUERYSET
    {
        const Int32 NS_BTH = 16;
        const Int32 LUP_RETURN_ALL = 0x0FF0; // see winsock1.h
        const Int32 LUP_CONTAINERS = 0x0002;

        WSAQUERYSET wsaqueryset = new WSAQUERYSET();
        public myWSAQUERYSET()
        {
        }
        
        public void discoverDevices()
        {
            ArrayList aDevices = GetConnectedNetworks();
        }

        private ArrayList GetConnectedNetworks()
        {
            ArrayList networkConnections = new ArrayList();
            WSAQUERYSET qsRestrictions;
            Int32 dwControlFlags;
            Int32 valHandle = 0;
                    
            qsRestrictions = new WSAQUERYSET();
            qsRestrictions.dwSize = 15 * 4 * 8;// Marshal.SizeOf(typeof(qsRestrictions));
            qsRestrictions.dwNameSpace = NS_BTH;// 0; //NS_ALL;
            dwControlFlags = 0x0FF0; //LUP_RETURN_ALL;

            int result = SafeNativeMethods.WSALookupServiceBegin(qsRestrictions,
                dwControlFlags, ref valHandle);

            //CheckResult(result);
                
            while (0 == result)
            {
                Int32 dwBuffer = 0x10000; 
                IntPtr pBuffer = Marshal.AllocHGlobal(dwBuffer);
                        
                WSAQUERYSET qsResult = new WSAQUERYSET() ;
                            
                result = SafeNativeMethods.WSALookupServiceNext(valHandle, dwControlFlags, 
                        ref dwBuffer, pBuffer);
                
                if (0==result)
                {
                    Marshal.PtrToStructure(pBuffer, qsResult);
                    networkConnections.Add(
                        qsResult.szServiceInstanceName);
                }
                else
                {
                    //CheckResult(result);
                }
                Marshal.FreeHGlobal(pBuffer);
            }       

            result = SafeNativeMethods.WSALookupServiceEnd(valHandle);

            return networkConnections;
        }
    }
}
