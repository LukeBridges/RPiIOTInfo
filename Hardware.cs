using System;
using System.Linq;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;

namespace OPG.Signage.Info
{
	public static class Hardware
	{
        public static class Iot
        {
            public static class Pi
            {
                public static bool IsPi = IoTCoreDefaultApp.Utils.DeviceTypeInformation.IsRaspberryPi;
            }

            public static bool IsIotDevice = (IoTCoreDefaultApp.Utils.DeviceTypeInformation.Type != IoTCoreDefaultApp.Utils.DeviceTypes.Unknown);
            internal static string Board = IoTCoreDefaultApp.Utils.DeviceTypeInformation.ProductName;
        }

        private static EasClientDeviceInformation eas = new EasClientDeviceInformation();

        public static string Manufacturer 
        {
            get
            {
                return eas.SystemManufacturer;
            }
        }

        public static string ProductName 
        {
            get
            {
                if(Iot.IsIotDevice)
                {
                    return Iot.Board;
                }
                return eas.SystemProductName;
            }
        }

        public static string Board 
        {
            get
            {
                if(Iot.IsIotDevice)
                {
                    return Iot.Board;
                }
                return "";
            }
        }

        public static string CPU 
        {
            get
            {
                return "";
            }
        }

        public static string FormFactor 
        {
            get
            {
                return AnalyticsInfo.DeviceForm;
            }
        }

        public static string DeviceFamily 
        {
            get
            {
                return AnalyticsInfo.VersionInfo.DeviceFamily;
            }
        }

        public static string DeviceFamilyVersion {
            get
            {
                return AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            }
        }

        public static class Network
        {
            private static string _HostName = string.Empty;
            private static string _IP = string.Empty;

            public static string HostName {
                get
                {
                    if (String.IsNullOrEmpty(_HostName))
                    {
                        _HostName = NetworkInformation.GetHostNames().FirstOrDefault(name => name.Type == HostNameType.DomainName)?.DisplayName ?? "???";
                    }
                    return _HostName;
                }
            }

            public static string IP {
                get
                {
                    if (String.IsNullOrEmpty(_IP))
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
}
