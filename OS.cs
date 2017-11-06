using System;
using System.Globalization;
using Windows.System.Profile;

namespace OPG.Signage.Info
{
    public static class OS
	{
        private static string _Version = string.Empty;

        public static string Version 
        {
            get
            {
                if(String.IsNullOrEmpty(_Version))
                {
                    ulong version = ulong.Parse(AnalyticsInfo.VersionInfo.DeviceFamilyVersion, CultureInfo.CurrentCulture);
                    ulong major = (version & 0xFFFF000000000000L) >> 48;
                    ulong minor = (version & 0x0000FFFF00000000L) >> 32;
                    ulong build = (version & 0x00000000FFFF0000L) >> 16;
                    ulong revision = (version & 0x000000000000FFFFL);
                    _Version = major + "." + minor + "." + build + "." + revision;
                }
                return _Version;
            }
        }
	}
}
