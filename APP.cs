using System.Globalization;
using Windows.ApplicationModel;

namespace OPG.Signage.Info
{
	public static class APP
	{
		private static string _AppVersion = string.Empty;

		public static string AppVersion
		{
			get
			{
				if(string.IsNullOrEmpty(_AppVersion))
				{
					_AppVersion = Package.Current.Id.Version.Major.ToString(CultureInfo.CurrentCulture) + "." + Package.Current.Id.Version.Minor.ToString(CultureInfo.CurrentCulture) + "." + Package.Current.Id.Version.Build.ToString(CultureInfo.CurrentCulture);
				}
				return _AppVersion;
			}
		}
	}
}
