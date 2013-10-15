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

#endregion

namespace Microsoft.WindowsMobile.SharedSource.Bluetooth
{
	/// <summary>
	/// Lists GUIDs for standard Bluetooth services
	/// </summary>
	public class StandardServices
	{
		/// <summary>
		/// constructor so default constructor is not created since all methods are static
		/// </summary>
		private StandardServices()
		{
			;
		}

		/// <summary>
		/// Guid representing the Serial Port Profile
		/// </summary>
		static public Guid SerialPortServiceGuid
		{
			get
			{
				if (serialPortServiceGuid == Guid.Empty)
				{
					serialPortServiceGuid = new Guid(0x00001101, 0x0000, 0x1000, 0x80, 0x00, 0x00, 0x80, 0x5F, 0x9B, 0x34, 0xFB);
				}

				return serialPortServiceGuid;
			}
		}

		private static Guid serialPortServiceGuid = Guid.Empty;
	}
}
