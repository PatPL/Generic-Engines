using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public static class AppInfo {
		public static readonly string Version = FileVersionInfo.GetVersionInfo (Assembly.GetExecutingAssembly ().Location).FileVersion;
		public static string SerializerVersion {
			get {
				return Serializer.Version ().ToString ();
			}
		}
	}
}
