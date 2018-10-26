using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace GenericEngines {
	/// <summary>
	/// Stuff related to the Settings system
	/// </summary>
	public static class Settings {

		private static readonly string settingsPath = $"{Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)}\\Generic Engines\\setti.ngs";
		private static Dictionary<string, string> settings = null;

		/// <summary>
		/// Returns the value of the setting. Don't use strings directly, use Setting class.
		/// </summary>
		/// <param name="key">The setting to be returned</param>
		/// <returns></returns>
		public static string Get (string key) {
			if (settings is null) {
				LoadSettings ();
			}

			if (!settings.TryGetValue (key, out string output)) {
				if (!defaultSettings.TryGetValue (key, out output)) {
					output = "UnknownKey";
				}

				Set (key, output);
			}

			return output;
		}

		/// <summary>
		/// Returns the value of the setting as a bool. Don't use strings directly, use Setting class.
		/// </summary>
		/// <param name="key">The setting to be returned</param>
		/// <returns></returns>
		public static bool GetBool (string key) {
			return bool.Parse (Get (key));
		}

		/// <summary>
		/// Sets the value of a setting. Don't use strings directly, use Setting class.
		/// </summary>
		/// <param name="key">Setting to set</param>
		/// <param name="value">Value to set</param>
		public static void Set (string key, string value) {

			if (settings is null) {
				LoadSettings ();
			}

			settings[key] = value;

			SaveSettings ();
		}

		private static void LoadSettings (bool forceReload = false) {

			if (settings != null && !forceReload) {
				return;
			}

			settings = new Dictionary<string, string> ();

			if(!Directory.Exists (Path.GetDirectoryName (settingsPath))) {
				Directory.CreateDirectory (Path.GetDirectoryName (settingsPath));
			}

			StreamReader file;
			if (!File.Exists (settingsPath)) {
				file = new StreamReader (File.Create (settingsPath));
			} else {
				file = new StreamReader (settingsPath);
			}

			string currentLine;
			string[] currentArgs;
			char[] splitters = new char[] { ':' };
			while (!file.EndOfStream) {
				currentLine = file.ReadLine ();

				if (currentLine[0] == '#') {
					continue;
				}

				currentArgs = currentLine.Split (splitters, 2);

				if (currentArgs.Length != 2) {
					continue;
				}

				settings.Add (currentArgs[0], currentArgs[1]);

			}

			file.Close ();
		}

		private static void SaveSettings () {
			string output = "";

			foreach (KeyValuePair<string, string> i in settings) {
				output += $"{i.Key}:{i.Value}{Environment.NewLine}";
			}

			File.WriteAllText (settingsPath, output);
		}

		private static readonly Dictionary<string, string> defaultSettings = new Dictionary<string, string> {
			{ Setting.AdvConfirmBox, "False" },
			{ Setting.DefaultSaveDirectory, $"{Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)}\\Generic Engines\\Saves\\" },
			{ Setting.DefaultExportDirectory, $"{Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)}\\Generic Engines\\Exports\\" },
			{ Setting.MoreEngineInfo, "False" },
			{ Setting.UseCompactMenu, "False" }
		};
	}

	/// <summary>
	/// Contains all settings keys.
	/// </summary>
	public static class Setting {
		public static readonly string AdvConfirmBox = "AdvConfirmBox";
		public static readonly string DefaultSaveDirectory = "DefaultSaveDirectory";
		public static readonly string DefaultExportDirectory = "DefaultExportDirectory";
		public static readonly string MoreEngineInfo = "MoreEngineInfo";
		public static readonly string UseCompactMenu = "UseCompactMenu";

	}
}