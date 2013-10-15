//
// Copyright (c)  Microsoft Corporation.  All rights reserved.
//
//
// This source code is licensed under Microsoft Shared Source License
// Version 1.0 for Windows CE.
// For a copy of the license visit http://go.microsoft.com/?linkid=2933443.
//

#region Using directives

using System;
using System.Net.Sockets;

#endregion

namespace Microsoft.WindowsMobile.SharedSource.Bluetooth
{
	/// <summary>
	/// Represents a unique Bluetooth device and provides the ability to connect with it on a
	/// specified service.
	/// </summary>
	public class BluetoothDevice
	{
		/// <summary>
		/// Constructs an object to represent the Bluetooth device described with a name and address
		/// </summary>
		/// <param name="name">Describes the Bluetooth device</param>
		/// <param name="address">8 byte bluetooth address</param>
		public BluetoothDevice(string name, byte[] address)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}

			if (address == null)
			{
				throw new ArgumentNullException("address");
			}

			this.name = name;
			this.address = address;
		}

        public override string ToString()
        {
            string s="";
            s += this.name + " (" + this.AddressStr + ")";
            return s;
        }
		
		/// <summary>
		/// Describes a Bluetooth device
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
		}

		/// <summary>
		/// 8 byte Bluetooth address
		/// </summary>
		public byte[] Address
		{
			get
			{
				return address;
			}
		}

        /// <summary>
        /// 8 byte Bluetooth address as hex string
        /// </summary>
        public string AddressStr
        {
            get
            {
                string s="";
                for(int i=address.Length-1;i>=0;i--)
                    s += address[i].ToString("x02");
                return s;
            }
        }

		/// <summary>
		/// Provides the ability to connect to this device and transfer data
		/// </summary>
		/// <param name="serviceGuid">Specifies the Guid of the service to connect with on the remote device</param>
        /// <param name="secure">Indicates whether this connection should be encrypted</param>
        /// <returns>A NetworkStream object used to communicate between the two devices</returns>
		public NetworkStream Connect(Guid serviceGuid, bool secure)
		{
			Socket clientSocket = new Socket((AddressFamily)32, SocketType.Stream, (ProtocolType)3);
            NetworkStream nws = null;
            try
            {
                BluetoothEndPoint endPoint = new BluetoothEndPoint(this, serviceGuid);

                clientSocket.Connect(endPoint);
                if (secure)
                {
                    // turn on authentication
                    SafeNativeMethods.BthAuthenticate(this.address);
                    // turn on link encryption
                    SafeNativeMethods.BthSetEncryption(this.address, 1);
                }                
                nws = new NetworkStream(clientSocket, true);
                connected = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in Connect(): " + ex.Message);
                nws = null;
                connected = false;
            }
			// the network stream will own the socket so that it will clean up nicely
			return nws;
		}

        /// <summary>
        /// Provides the ability to connect to this device and transfer data with encryption turned off
        /// </summary>
        /// <param name="serviceGuid">Specifies the Guid of the service to connect with on the remote device</param>
        /// <returns>A NetworkStream object used to communicate between the two devices</returns>
        public NetworkStream Connect(Guid serviceGuid)
        {
            return Connect(serviceGuid, false);
        }

        public NetworkStream Connect(byte[] btAddress, Guid serviceGuid, bool secure)
        {
            Socket clientSocket = new Socket((AddressFamily)32, SocketType.Stream, (ProtocolType)3);
            NetworkStream nws = null;

            this.address = btAddress;
            try
            {
                BluetoothEndPoint endPoint = new BluetoothEndPoint(btAddress, serviceGuid);

                clientSocket.Connect(endPoint);
                if (secure)
                {
                    // turn on authentication
                    SafeNativeMethods.BthAuthenticate(this.address);
                    // turn on link encryption
                    SafeNativeMethods.BthSetEncryption(this.address, 1);
                }
                nws = new NetworkStream(clientSocket, true);
                connected = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in Connect(): " + ex.Message);
                nws = null;
                connected = false; ;
            }

            return nws;
        }

        public NetworkStream Connect(byte[] btAddress)
        {
            Socket clientSocket = new Socket((AddressFamily)32, SocketType.Stream, (ProtocolType)3);
            NetworkStream nws = null;
            this.address = btAddress;
            try
            {
                nws = this.Connect(btAddress, StandardServices.SerialPortServiceGuid, false);
                nws = new NetworkStream(clientSocket, true);
                connected = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in Connect(): " + ex.Message);
                nws = null;
                connected = false;
            }

            return nws;
        }

        private bool connected;
        public bool _connected
        {
            get
            {
                return connected;
            }
        }
		private string name;
		private byte[] address;

	}
}
