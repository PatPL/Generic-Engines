using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public static class AppInfo {
		public static readonly string Version = "0.1.4";
		public static string SerializerVersion {
			get {
				return Serializer.Version ().ToString ();
			}
		}
	}
}
