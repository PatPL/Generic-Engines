using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GenericEngines {
	/// <summary>
	/// The class used for turning Engine objects into byte arrays and back.
	/// </summary>
	public static class Serializer {
		delegate A SerializerFunc<A, B> (B b);
		delegate A DeserializerFunc<A, B, C, D> (B b, out C c, D d);
		
		/// <summary>
		/// Returns the version of the serializer
		/// </summary>
		/// <returns></returns>
		public static int Version () {
			return SerializerVersion;
		}

		private readonly static short SerializerVersion = 12;
		private static byte[] LatestSerializer (Engine e) {
			
			short version = SerializerVersion;

			int i = 0;
			byte[] output = new byte[ //This is getting out of hand. Still, I think it's readable
				2 + //short - Version (BIG ENDIAN - BACKWARDS COMPATIBILITY)
				1 + //bool - Active
				(e.Name.Length + 2) + //1B * length + 2B length header - Name
				8 + //double - Mass
				8 + //double - Thrust
				8 + //double - AtmIsp
				8 + //double - VacIsp
				e.PropellantRatio.Count * 10 + 2 + //(2B + 8B) * count + 2B length header - PropellantRatio
				8 + //double - Width
				8 + //double - Height
				8 + //double - Gimbal
				4 + //int - Cost
				8 + //double - MinThrust
				4 + //int - Ignitions
				1 + //bool - PressureFed
				1 + //bool - NeedsUllage
				1 + //bool - FuelVolumeRatios
				1 + //bool - TestFlightConfigNotDefault
				(e.TestFlightConfigNotDefault ? 1 : 0) * ( //Include all properties inside brackets only if any Test Flight properties were changed
				1 + //bool - EnableTestFlight
				4 + //int - RatedBurnTime
				8 + //double - StartReliability0
				8 + //double - StartReliability10k
				8 + //double - CycleReliability0
				8)+ //double - CycleReliability10k
				8 + //double - AlternatorPower
				1 + //bool - GimbalConfigNotDefault
				(e.GimbalConfigNotDefault ? 1 : 0) * ( //Include all properties inside brackets only if any Gimbal properties were changed
				1 + //bool - AdvancedGimbal
				8 + //double - GimbalNX
				8 + //double - GimbalPX
				8 + //double - GimbalNY
				8)+ //double - GimbalPY
				2 + //short - ModelID
				2 + //short - PlumeID
				2 + //short - TechUnlockNode
				4 + //int - EntryCost
				(e.EngineName.Length + 2) + //1B * length + 2B length header - EngineName
				1 + //bool - ManufacturerNotDefault
				(e.ManufacturerNotDefault ? 1 : 0) * (e.EngineManufacturer.Length + 2) + //(1B * length + 2B length header) if manufacturer was changed - EngineManufacturer
				1 + //bool - DescriptionNotDefault
				(e.DescriptionNotDefault ? 1 : 0) * (e.EngineDescription.Length + 2) + //(1B * length + 2B length header) if description was changed - EngineDescription
				1 + //bool - UseBaseWidth
				1 + //EngineType - EngineVariant
				8 + //double - TanksVolume
				e.TanksContents.Count * 10 + 2 + //(2B + 8B) * count + 2B length header - TanksContents
				e.ThrustCurve.Count * 16 + 2 + //(8B + 8B) * count + 2B length header - ThrustCurve
				1 + //bool - UseTanks
				1 + //bool - LimitTanks
				1 + //Polymorphism - PolyType
				e.MasterEngineName.Length + 2 + //1B * length + 2B length header - MasterEngineName
				4 + //int - MasterEngineCost
				8 //double - MasterEngineMass
			];

			//short - Version (BIG ENDIAN - BACKWARDS COMPATIBILITY)
			output[i++] = (byte) (version / 256);
			output[i++] = (byte) (version % 256);

			//bool - Active
			output[i++] = (byte) (e.Active ? 1 : 0);

			//String + 2B length header - Name
			//String length header
			output[i++] = (byte) (e.Name.Length % 256);
			output[i++] = (byte) (e.Name.Length / 256);
			//String data
			foreach (char c in e.Name) {
				output[i++] = Convert.ToByte (c);
			}

			//double - Mass
			foreach (byte b in BitConverter.GetBytes (e.Mass)) {
				output[i++] = b;
			}

			//double - Thrust
			foreach (byte b in BitConverter.GetBytes (e.Thrust)) {
				output[i++] = b;
			}

			//double - AtmIsp
			foreach (byte b in BitConverter.GetBytes (e.AtmIsp)) {
				output[i++] = b;
			}

			//double - VacIsp
			foreach (byte b in BitConverter.GetBytes (e.VacIsp)) {
				output[i++] = b;
			}

			//(2B + 8B) * count + 2B length header - PropellantRatio
			//Length header
			output[i++] = (byte) (e.PropellantRatio.Count % 256);
			output[i++] = (byte) (e.PropellantRatio.Count / 256);
			//Data
			foreach (FuelRatioElement f in e.PropellantRatio) {
				output[i++] = (byte) ((int) f.Propellant % 256);
				output[i++] = (byte) ((int) f.Propellant / 256);
				foreach (byte b in BitConverter.GetBytes (f.Ratio)) {
					output[i++] = b;
				}
			}

			//double - Width
			foreach (byte b in BitConverter.GetBytes (e.Width)) {
				output[i++] = b;
			}

			//double - Height
			foreach (byte b in BitConverter.GetBytes (e.Height)) {
				output[i++] = b;
			}

			//double - Gimbal
			foreach (byte b in BitConverter.GetBytes (e.Gimbal)) {
				output[i++] = b;
			}

			//int - Cost
			foreach (byte b in BitConverter.GetBytes (e.Cost)) {
				output[i++] = b;
			}

			//double - MinThrust
			foreach (byte b in BitConverter.GetBytes (e.MinThrust)) {
				output[i++] = b;
			}

			//int - Ignitions
			foreach (byte b in BitConverter.GetBytes (e.Ignitions)) {
				output[i++] = b;
			}

			//bool - PressureFed
			output[i++] = (byte) (e.PressureFed ? 1 : 0);

			//bool - NeedsUllage
			output[i++] = (byte) (e.NeedsUllage ? 1 : 0);

			//bool - FuelVolumeRatios
			output[i++] = (byte) (e.FuelVolumeRatios ? 1 : 0);

			//bool - TestFlightConfigNotDefault
			output[i++] = (byte) (e.TestFlightConfigNotDefault ? 1 : 0);

			//Include all properties inside brackets only if any Test Flight properties were changed
			if (e.TestFlightConfigNotDefault) {
				//bool - EnableTestFlight
				output[i++] = (byte) (e.EnableTestFlight ? 1 : 0);

				//int - RatedBurnTime
				foreach (byte b in BitConverter.GetBytes (e.RatedBurnTime)) {
					output[i++] = b;
				}

				//double - StartReliability0
				foreach (byte b in BitConverter.GetBytes (e.StartReliability0)) {
					output[i++] = b;
				}

				//double - StartReliability10k
				foreach (byte b in BitConverter.GetBytes (e.StartReliability10k)) {
					output[i++] = b;
				}

				//double - CycleReliability0
				foreach (byte b in BitConverter.GetBytes (e.CycleReliability0)) {
					output[i++] = b;
				}

				//double - CycleReliability10k
				foreach (byte b in BitConverter.GetBytes (e.CycleReliability10k)) {
					output[i++] = b;
				}
			}

			//double - AlternatorPower
			foreach (byte b in BitConverter.GetBytes (e.AlternatorPower)) {
				output[i++] = b;
			}

			//bool - GimbalConfigNotDefault
			output[i++] = (byte) (e.GimbalConfigNotDefault ? 1 : 0);

			//Include all properties inside brackets only if any Gimbal properties were changed
			if (e.GimbalConfigNotDefault) {
				//bool - AdvancedGimbal
				output[i++] = (byte) (e.AdvancedGimbal ? 1 : 0);

				//double - GimbalNX
				foreach (byte b in BitConverter.GetBytes (e.GimbalNX)) {
					output[i++] = b;
				}

				//double - GimbalPX
				foreach (byte b in BitConverter.GetBytes (e.GimbalPX)) {
					output[i++] = b;
				}

				//double - GimbalNY
				foreach (byte b in BitConverter.GetBytes (e.GimbalNY)) {
					output[i++] = b;
				}

				//double - GimbalPY
				foreach (byte b in BitConverter.GetBytes (e.GimbalPY)) {
					output[i++] = b;
				}
			}

			//short - ModelID
			output[i++] = (byte) (((short) e.ModelID) % 256);
			output[i++] = (byte) (((short) e.ModelID) / 256);

			//short - PlumeID
			output[i++] = (byte) (((short) e.PlumeID) % 256);
			output[i++] = (byte) (((short) e.PlumeID) / 256);

			//short - TechUnlockNode
			output[i++] = (byte) (((short) e.TechUnlockNode) % 256);
			output[i++] = (byte) (((short) e.TechUnlockNode) / 256);

			//int - EntryCost
			foreach (byte b in BitConverter.GetBytes (e.EntryCost)) {
				output[i++] = b;
			}

			//1B * length + 2B length header - EngineName
			//String header
			output[i++] = (byte) (e.EngineName.Length % 256);
			output[i++] = (byte) (e.EngineName.Length / 256);
			//String data
			foreach (char c in e.EngineName) {
				output[i++] = Convert.ToByte (c);
			}

			//bool - ManufacturerNotDefault
			output[i++] = (byte) (e.ManufacturerNotDefault ? 1 : 0);

			//Write Manufacturer to file if it was changed
			if (e.ManufacturerNotDefault) {
				//(1B * length + 2B length header) - EngineManufacturer
				//String header
				output[i++] = (byte) (e.EngineManufacturer.Length % 256);
				output[i++] = (byte) (e.EngineManufacturer.Length / 256);
				//String data
				foreach (char c in e.EngineManufacturer) {
					output[i++] = Convert.ToByte (c);
				}
			}

			//bool - DescriptionNotDefault
			output[i++] = (byte) (e.DescriptionNotDefault ? 1 : 0);

			//Write description to file if it was changed
			if (e.DescriptionNotDefault) {
				//(1B * length + 2B length header) - EngineDescription
				//String header
				output[i++] = (byte) (e.EngineDescription.Length % 256);
				output[i++] = (byte) (e.EngineDescription.Length / 256);
				//String data
				foreach (char c in e.EngineDescription) {
					output[i++] = Convert.ToByte (c);
				}
			}

			//bool - UseBaseWidth
			output[i++] = (byte) (e.UseBaseWidth ? 1 : 0);

			//EngineType - EngineVariant
			output[i++] = (byte) e.EngineVariant;

			//double - TanksVolume
			foreach (byte b in BitConverter.GetBytes (e.TanksVolume)) {
				output[i++] = b;
			}

			//(2B + 8B) * count + 2B length header - TanksContents
			//Length header
			output[i++] = (byte) (e.TanksContents.Count % 256);
			output[i++] = (byte) (e.TanksContents.Count / 256);
			//Data
			foreach (FuelRatioElement f in e.TanksContents) {
				output[i++] = (byte) ((int) f.Propellant % 256);
				output[i++] = (byte) ((int) f.Propellant / 256);
				foreach (byte b in BitConverter.GetBytes (f.Ratio)) {
					output[i++] = b;
				}
			}

			//(8B + 8B) * count + 2B length header - ThrustCurve
			//Length header
			output[i++] = (byte) (e.ThrustCurve.Count % 256);
			output[i++] = (byte) (e.ThrustCurve.Count / 256);
			//Data
			foreach (DoubleTuple t in e.ThrustCurve) {
				foreach (byte b in BitConverter.GetBytes (t.Item1)) {
					output[i++] = b;
				}

				foreach (byte b in BitConverter.GetBytes (t.Item2)) {
					output[i++] = b;
				}
			}

			//bool - UseTanks
			output[i++] = (byte) (e.UseTanks ? 1 : 0);

			//bool - LimitTanks
			output[i++] = (byte) (e.LimitTanks ? 1 : 0);

			//Polymorphism - PolyType
			output[i++] = (byte) e.PolyType;

			//String + 2B length header - MasterEngineName
			//String length header
			output[i++] = (byte) (e.MasterEngineName.Length % 256);
			output[i++] = (byte) (e.MasterEngineName.Length / 256);
			//String data
			foreach (char c in e.MasterEngineName) {
				output[i++] = Convert.ToByte (c);
			}

			//int - MasterEngineCost
			foreach (byte b in BitConverter.GetBytes (e.MasterEngineCost)) {
				output[i++] = b;
			}

			//double - MasterEngineMass
			foreach (byte b in BitConverter.GetBytes (e.MasterEngineMass)) {
				output[i++] = b;
			}

			return output;
		}

		private static Engine UltimateDeserializer (byte[] input, out int addedOffset, int offset) {
			
			Engine output = Engine.New ();
			int i = offset;

			//short - Version header
			short version = 0;
			version += input[i++];
			version *= 256;
			version += input[i++];

			if (version >= 0) {
				//bool - Active
				output.Active = input[i++] == 1;

				//String + 2B length header - Name
				{
					int stringLength = 0;
					if (version >= 3) {
						stringLength += input[i++];
						stringLength += input[i++] * 256;
					} else {
						stringLength += input[i++] * 256;
						stringLength += input[i++];
					}

					output.Name = "";
					for (int c = 0; c < stringLength; ++c) {
						output.Name += Convert.ToChar (input[i++]);
					}
					output.Name = Regex.Replace (output.Name, "[<>,+*=_ ]", "-");
					
				}

				//double - Mass
				output.Mass = BitConverter.ToDouble (input, i);
				i += 8;

				//double - Thrust
				output.Thrust = BitConverter.ToDouble (input, i);
				i += 8;

				//double - AtmIsp
				output.AtmIsp = BitConverter.ToDouble (input, i);
				i += 8;

				//double - VacIsp
				output.VacIsp = BitConverter.ToDouble (input, i);
				i += 8;

				//(2B + 8B) * count + 2B length header - PropellantRatio
				{
					int dataLength = 0;
					if (version >= 3) {
						dataLength += input[i++];
						dataLength += input[i++] * 256;
					} else {
						dataLength += input[i++] * 256;
						dataLength += input[i++];
					}

					output.PropellantRatio.Clear (); //Constructor gives one element to this list

					FuelType fuelType = 0;
					for (int c = 0; c < dataLength; ++c) {
						fuelType = 0;
						if (version >= 3) {
							fuelType += input[i++];
							fuelType += input[i++] * 256;
						} else {
							fuelType += input[i++] * 256;
							fuelType += input[i++];
						}

						output.PropellantRatio.Add (new FuelRatioElement (fuelType, BitConverter.ToDouble (input, i)));
						i += 8;
					}
				}

				//double - Width
				output.Width = BitConverter.ToDouble (input, i);
				i += 8;

				//double - Height
				output.Height = BitConverter.ToDouble (input, i);
				i += 8;

				//double - Gimbal
				output.Gimbal = BitConverter.ToDouble (input, i);
				i += 8;

				//int - Cost
				output.Cost = BitConverter.ToInt32 (input, i);
				i += 4;
			}

			if (version >= 1) {
				//double - MinThrust
				output.MinThrust = BitConverter.ToDouble (input, i);
				i += 8;

				//int - Ignitions
				output.Ignitions = BitConverter.ToInt32 (input, i);
				i += 4;

				//bool - PressureFed
				output.PressureFed = input[i++] == 1;

				//bool - NeedsUllage
				output.NeedsUllage = input[i++] == 1;
			}

			if (version >= 2) {
				//bool - FuelVolumeRatios
				output.FuelVolumeRatios = input[i++] == 1;
			}

			if (version >= 3) {
				//bool - TestFlightConfigNotDefault
				if (input[i++] == 1) {
					//bool - EnableTestFlight
					output.EnableTestFlight = input[i++] == 1;

					//int - RatedBurnTime
					output.RatedBurnTime = BitConverter.ToInt32 (input, i);
					i += 4;

					//double - StartReliability0
					output.StartReliability0 = BitConverter.ToDouble (input, i);
					i += 8;

					//double - StartReliability10k
					output.StartReliability10k = BitConverter.ToDouble (input, i);
					i += 8;

					//double - CycleReliability0
					output.CycleReliability0 = BitConverter.ToDouble (input, i);
					i += 8;

					//double - CycleReliability10k
					output.CycleReliability10k = BitConverter.ToDouble (input, i);
					i += 8;
				}
			}

			if (version >= 4) {
				//double - AlternatorPower
				output.AlternatorPower = BitConverter.ToDouble (input, i);
				i += 8;
			}

			if (version >= 5) {
				//bool - GimbalConfigNotDefault
				if (input[i++] == 1) {
					//bool - AdvancedGimbal
					output.AdvancedGimbal = input[i++] == 1;

					//double - GimbalNX
					output.GimbalNX = BitConverter.ToDouble (input, i);
					i += 8;

					//double - GimbalPX
					output.GimbalPX = BitConverter.ToDouble (input, i);
					i += 8;

					//double - GimbalNY
					output.GimbalNY = BitConverter.ToDouble (input, i);
					i += 8;

					//double - GimbalPY
					output.GimbalPY = BitConverter.ToDouble (input, i);
					i += 8;
				}
			}

			if (version >= 6) {
				//short - ModelID
				output.ModelID += input[i++];
				output.ModelID += input[i++] * 256;

				//short - PlumeID
				output.PlumeID += input[i++];
				output.PlumeID += input[i++] * 256;
			}

			if (version >= 7) {
				//short - TechUnlockNode
				output.TechUnlockNode += input[i++];
				output.TechUnlockNode += input[i++] * 256;

				//int - EntryCost
				output.EntryCost = BitConverter.ToInt32 (input, i);
				i += 4;

				//1B * length + 2B length header - EngineName
				{
					int stringLength = 0;
					stringLength += input[i++];
					stringLength += input[i++] * 256;
					
					output.EngineName = "";
					for (int c = 0; c < stringLength; ++c) {
						output.EngineName += Convert.ToChar (input[i++]);
					}
				}

				//bool - ManufacturerNotDefault
				if (input[i++] == 1) {
					//(1B * length + 2B length header) if Manufacturer was changed - EngineManufacturer
					{
						int stringLength = 0;
						stringLength += input[i++];
						stringLength += input[i++] * 256;

						output.EngineManufacturer = "";
						for (int c = 0; c < stringLength; ++c) {
							output.EngineManufacturer += Convert.ToChar (input[i++]);
						}
					}
				}

				//bool - DescriptionNotDefault
				if (input[i++] == 1) {
					//(1B * length + 2B length header) if description was changed - EngineDescription
					{
						int stringLength = 0;
						stringLength += input[i++];
						stringLength += input[i++] * 256;

						output.EngineDescription = "";
						for (int c = 0; c < stringLength; ++c) {
							output.EngineDescription += Convert.ToChar (input[i++]);
						}
					}
				}
			}

			if (version >= 8) {
				//bool - UseBaseWidth
				output.UseBaseWidth = input[i++] == 1;
			} else { //version < 8
				//Default value before version 8 was false
				output.UseBaseWidth = false;
				//Now the default value is true
			}

			if (version >= 9) {
				//EngineType - EngineVariant
				output.EngineVariant = (EngineType) input[i++];

				//double - TanksVolume
				output.TanksVolume = BitConverter.ToDouble (input, i);
				i += 8;

				//(2B + 8B) * count + 2B length header - TanksContents
				{
					int dataLength = 0;
					dataLength += input[i++];
					dataLength += input[i++] * 256;

					FuelType fuelType = 0;
					for (int c = 0; c < dataLength; ++c) {
						fuelType = 0;
						fuelType += input[i++];
						fuelType += input[i++] * 256;

						output.TanksContents.Add (new FuelRatioElement (fuelType, BitConverter.ToDouble (input, i)));
						i += 8;
					}
				}

				//(8B + 8B) * count + 2B length header - ThrustCurve
				{
					int dataLength = 0;
					dataLength += input[i++];
					dataLength += input[i++] * 256;

					for (int c = 0; c < dataLength; ++c) {
						double tmp = BitConverter.ToDouble (input, i);
						i += 8;

						output.ThrustCurve.Add (new DoubleTuple (tmp, BitConverter.ToDouble (input, i)));
						i += 8;
					}
				}
			}

			if (version >= 10) {
				//bool - UseTanks
				output.UseTanks = input[i++] == 1;

				//bool - LimitTanks
				output.LimitTanks = input[i++] == 1;
			}

			if (version >= 11) {
				//Polymorphism - PolyType
				output.PolyType = (Polymorphism) input[i++];

				//(1B * length + 2B length header) - MasterEngineName
				{
					int stringLength = 0;
					stringLength += input[i++];
					stringLength += input[i++] * 256;

					output.MasterEngineName = "";
					for (int c = 0; c < stringLength; ++c) {
						output.MasterEngineName += Convert.ToChar (input[i++]);
					}
				}
			}

			if (version >= 12) {
				//int - MasterEngineCost
				output.MasterEngineCost = BitConverter.ToInt32 (input, i);
				i += 4;

				//double - MasterEngineMass
				output.MasterEngineMass = BitConverter.ToDouble (input, i);
				i += 8;
			}

			addedOffset = i - offset;
			return output;
		}

		/// <summary>
		/// Convert an Engine object into a byte array.
		/// </summary>
		/// <param name="e">The Engine to be serialized</param>
		/// <returns></returns>
		public static byte[] Serialize (Engine e) {
			return LatestSerializer (e);
		}

		/// <summary>
		/// Convert a byte array into an engine.
		/// </summary>
		/// <param name="input">The byte array</param>
		/// <param name="addedOffset">How many bytes long was the deserialized engine?</param>
		/// <param name="offset">Starting byte array offset</param>
		/// <returns></returns>
		public static Engine Deserialize (byte[] input, out int addedOffset, int offset = 0) {
			return UltimateDeserializer (input, out addedOffset, offset);
		}
	}
}