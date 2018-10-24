using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Text.RegularExpressions;

namespace GenericEngines {
	public static class EngineUtility {
		
		/// <summary>
		/// Exports engines to the path. Returns how many Active engines were exported and ReturnStatus
		/// </summary>
		/// <param name="path"></param>
		public static (int, ReturnStatus) ExportEnginesToFile (List<Engine> engines, string path) {

			string fileBackup = "";
			
			try {
				if (File.Exists (path)) {
					fileBackup = File.ReadAllText (path);
				}

				File.WriteAllText (path, Exporter.ConvertEngineListToConfig (engines, out int exportedEnginesCount));

				string pathDirectory = new FileInfo (path).Directory.FullName;

				// Additional files
				File.WriteAllBytes ($"{pathDirectory}/PlumeScaleFixer.dll", Properties.Resources.GenericEnginesPlumeScaleFixer);
				File.WriteAllText ($"{pathDirectory}/GEAllTankDefinition.cfg", AllTankDefinition.Get);
				// /Additional files

				return (exportedEnginesCount, ReturnStatus.Success);
			} catch (Exception e) {
				App.SaveExceptionToFile (e);

				try {
					if (fileBackup != "") {
						File.WriteAllText (path, fileBackup);
					}
				} catch (Exception wtf) {
					App.SaveExceptionToFile (wtf);
					// Just in case. File IO is spooky when you let user choose the path.
				}

				return (0, ReturnStatus.Error);
			}
		}
		
		/// <summary>
		/// Saves engines to the path. Returns ReturnStatus and how many engines were saved.
		/// </summary>
		/// <param name="engines"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		public static (int, ReturnStatus) SaveEnginesToFile (List<Engine> engines, string path) {
			try {
				FileStream file = new FileStream (path, FileMode.OpenOrCreate, FileAccess.Write);
				file.SetLength (0);

				byte[] serializedEngine;
				foreach (Engine i in engines) {

					//serializedEngine = i.Serialize ();
					serializedEngine = Serializer.Serialize (i);

					file.Write (serializedEngine, 0, serializedEngine.Length);
				}

				file.Close ();
				
				return (engines.Count, ReturnStatus.Success);
			} catch (Exception e) {
				App.SaveExceptionToFile (e);
				return (0, ReturnStatus.Error);
			}
		}

		/// <summary>
		/// Reads engine list from file and returns all read engined together with ReturnStatus
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static (List<Engine>, ReturnStatus) ReadEnginesFromFile (string path) {
			try {
				FileStream file = new FileStream (path, FileMode.Open, FileAccess.Read);

				List<Engine> newEngines = new List<Engine> ();

				byte[] data = new byte[file.Length];
				file.Read (data, 0, (int) file.Length);

				int offset = 0;

				while (offset < data.Length) {
					//newEngines.Add (Engine.Deserialize (data, out int addedOffset, offset));
					newEngines.Add (Serializer.Deserialize (data, out int addedOffset, offset));

					offset += addedOffset;
				}
				file.Close ();

				return (newEngines, ReturnStatus.Success);

			} catch (Exception e) {
				App.SaveExceptionToFile (e);
				return (null, ReturnStatus.Error);
			}
		}
		
		/// <summary>
		/// Appends the engines from file to the engine list. Returns whether the operation was successful
		/// </summary>
		/// <param name="engines"></param>
		/// <returns></returns>
		public static ReturnStatus AppendEnginesToList (string path, List<Engine> engines) {

			(List<Engine> newEngines, ReturnStatus status) = ReadEnginesFromFile (path);

			if (status == ReturnStatus.Success) {
				engines.AddRange (newEngines);
			}

			return status;
			
		}

		public static List<string> Validate (List<Engine> engines) {
			List<string> errors = new List<string> ();

			errors.AddRange (EnsureEnginePolymorphismConsistency (engines));
			errors.AddRange (CheckDuplicateIDs (engines));

			return errors;
		}
		
		public static List<string> CheckDuplicateIDs (List<Engine> engines) {
			HashSet<string> takenIDs = new HashSet<string> ();
			List<string> errors = new List<string> ();

			foreach (Engine i in engines) {
				if (!i.Active) {
					continue;
				}

				if (Regex.IsMatch (i.Name, "[^a-zA-Z0-9-]")) {
					errors.Add ($"ID contains illegal characters: {i.Name}. Change the IDs.");
					continue;
				}

				if (takenIDs.Contains (i.Name)) {
					errors.Add ($"ID duplicate found: {i.Name}. Change the IDs.");
				} else {
					takenIDs.Add (i.Name);
				}
			}

			return errors;
		}

		/// <summary>
		/// Fixes Polymorphism config errors and alerts the user if error was found. Returns error messages.
		/// </summary>
		public static List<string> EnsureEnginePolymorphismConsistency (List<Engine> engines) {
			HashSet<string> LinkedMultiModeMasters = new HashSet<string> ();
			List<string> Errors = new List<string> ();

			foreach (Engine i in engines) {
				if (!i.Active) {
					continue;
				}

				if (i.PolyType == Polymorphism.MultiModeSlave && i.MasterEngineName != "") {
					if (engines.Exists (x => x.Active && x.PolyType == Polymorphism.MultiModeMaster && x.Name == i.MasterEngineName)) {
						if (LinkedMultiModeMasters.Contains (i.MasterEngineName)) {
							Errors.Add ($"Error in engine {i.Name}: MultiModeMaster can have at most 1 slave engine. {i.MasterEngineName} already has a slave.");
						} else {
							LinkedMultiModeMasters.Add (i.MasterEngineName);
						}
					} else {
						Errors.Add ($"Error in engine {i.Name}: {i.MasterEngineName} is inactive or its Polymorphism is not set to 'MultiModeMaster'.");
					}
				}

				if (i.PolyType == Polymorphism.MultiConfigSlave && i.MasterEngineName != "") {
					if (engines.Exists (x => x.Active && x.PolyType == Polymorphism.MultiConfigMaster && x.Name == i.MasterEngineName)) {

					} else {
						Errors.Add ($"Error in engine {i.Name}: {i.MasterEngineName} is inactive or its Polymorphism is not set to 'MultiConfigMaster'.");
					}
				}
			}

			return Errors;

		}

	}
}
