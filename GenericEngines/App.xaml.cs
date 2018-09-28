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
		
		public App () {
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler (ExceptionLogger);
		}

		static void ExceptionLogger (object sender, UnhandledExceptionEventArgs eventData) {
			Exception e = (Exception) eventData.ExceptionObject;
			if (!File.Exists ("crashError.log")) {
				File.Create ("crashError.log").Close ();
			}
			string output = File.ReadAllText ("crashError.log");
			output += "==========";
			output += Environment.NewLine;
			output += DateTime.Now.ToString ();
			output += Environment.NewLine;
			output += e.ToString ();
			output += Environment.NewLine;
			File.WriteAllText ("crashError.log", output);
		}
	}
}
