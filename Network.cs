using System;
using System.Linq;
using Windows.Networking;
using Windows.Networking.Connectivity;

namespace OPG.Signage.Info
{
	public static class Network
	{
		private static string _HostName = string.Empty;
		private static string _IP = string.Empty;
		
		public static string HostName
		{
			get
			{
				if(String.IsNullOrEmpty(_HostName))
				{
					_HostName = NetworkInformation.GetHostNames().FirstOrDefault(name => name.Type == HostNameType.DomainName)?.DisplayName ?? "???";
				}
				return _HostName;
			}
		}

		public static string IP
		{
			get
			{
				if(String.IsNullOrEmpty(_IP))
				{
					if (NetworkInformation.GetInternetConnectionProfile()?.NetworkAdapter == null)
					{
						return string.Empty;
					}

					HostName hostname = NetworkInformation.GetHostNames().SingleOrDefault(hn => hn.IPInformation?.NetworkAdapter != null && hn.IPInformation.NetworkAdapter.NetworkAdapterId == NetworkInformation.GetInternetConnectionProfile().NetworkAdapter.NetworkAdapterId);

					_IP = hostname?.CanonicalName;
				}
				return _IP;
			}
			
		}

		public static bool IsWifi()
		{
			return NetworkInformation.GetInternetConnectionProfile().IsWlanConnectionProfile;
		}

		public static byte? GetWifiStrength()
		{
			return NetworkInformation.GetInternetConnectionProfile().GetSignalBars();
		}

		public static string GetWifiAPSSID()
		{
			return NetworkInformation.GetInternetConnectionProfile().WlanConnectionProfileDetails.GetConnectedSsid();
		}
	}
}
