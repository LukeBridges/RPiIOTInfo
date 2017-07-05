using System.Linq;
using Windows.ApplicationModel;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.System.Profile;

namespace OPG.RPiIOT.Info
{
	public static class APP
	{
		public static string AppVersion { get; } = Package.Current.Id.Version.Major + "." + Package.Current.Id.Version.Minor + "." + Package.Current.Id.Version.Build;
	}

	public static class OS
	{
		public static string Version { get; } = string.Empty;

		static OS()
		{
			ulong version = ulong.Parse(AnalyticsInfo.VersionInfo.DeviceFamilyVersion);
			ulong major = (version & 0xFFFF000000000000L) >> 48;
			ulong minor = (version & 0x0000FFFF00000000L) >> 32;
			ulong build = (version & 0x00000000FFFF0000L) >> 16;
			ulong revision = (version & 0x000000000000FFFFL);
			Version = major + "." + minor + "." + build + "." + revision;
		}
	}

	public static class Network
	{
		public static string HostName { get; } = NetworkInformation.GetHostNames().FirstOrDefault(name => name.Type == HostNameType.DomainName)?.DisplayName ?? "???";

		public static string GetLocalIp()
		{
			if(NetworkInformation.GetInternetConnectionProfile()?.NetworkAdapter == null)
			{
				return string.Empty;
			}

			HostName hostname = NetworkInformation.GetHostNames().SingleOrDefault(hn => hn.IPInformation?.NetworkAdapter != null && hn.IPInformation.NetworkAdapter.NetworkAdapterId == NetworkInformation.GetInternetConnectionProfile().NetworkAdapter.NetworkAdapterId);

			return hostname?.CanonicalName;
		}

		public static bool IsWifi()
		{
			return NetworkInformation.GetInternetConnectionProfile().IsWlanConnectionProfile;
		}

		public static byte? GetWifiStrength()
		{
			return NetworkInformation.GetInternetConnectionProfile().GetSignalBars();
		}
	}
}
