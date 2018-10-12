using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace GenericEngines {
	/// <summary>
	/// Logika interakcji dla klasy App.xaml
	/// </summary>
	public partial class App : Application {

		public static readonly string crashErrorLogLocation = "crashError.log";
		public static readonly string otherErrorLogLocation = "otherError.log";

		public App () {
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler (ExceptionLogger);
		}

		static void ExceptionLogger (object sender, UnhandledExceptionEventArgs eventData) {
			Exception e = (Exception) eventData.ExceptionObject;
			SaveExceptionToFile (e, crashErrorLogLocation);
		}

		public static void SaveExceptionToFile (Exception e, string path = "") {
			if (path == "") {
				path = otherErrorLogLocation;
			}

			if (!File.Exists (path)) {
				File.Create (path).Close ();
			}

			string output = File.ReadAllText (path);
			output += "==========";
			output += Environment.NewLine;
			output += DateTime.Now.ToString ();
			output += Environment.NewLine;
			output += e.ToString ();
			output += Environment.NewLine;
			File.WriteAllText (path, output);
		}
	}
}
