using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	/// <summary>
	/// This class contains fields with versions
	/// </summary>
	public static class AppInfo {
		/// <summary>
		/// Generic Engines version set in AssemblyInfo.cs
		/// </summary>
		public static readonly string Version = FileVersionInfo.GetVersionInfo (Assembly.GetExecutingAssembly ().Location).FileVersion;

		/// <summary>
		/// Serializer version set in Serializer.cs
		/// </summary>
		public static string SerializerVersion  = Serializer.Version ().ToString ();

	}
}
