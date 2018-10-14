using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public static class Serializer {
		delegate A SerializerFunc<A, B> (B b);
		delegate A DeserializerFunc<A, B, C, D> (B b, out C c, D d);
		
		public static int Version () {
			return SerializerVersion;
		}

		private readonly static short SerializerVersion = 7;
		private static byte[] LatestSerializer (Engine e) {
			
			short version = SerializerVersion;

			int i = 0;
			byte[] output = new byte[
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
				8) + //double - CycleReliability10k
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
				(e.DescriptionNotDefault ? 1 : 0) * (e.EngineDescription.Length + 2) //(1B * length + 2B length header) if description was changed - EngineDescription
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

			addedOffset = i - offset;
			return output;
		}
		

		public static byte[] Serialize (Engine e) {
			return LatestSerializer (e);
		}

		public static Engine Deserialize (byte[] input, out int addedOffset, int offset = 0) {
			return UltimateDeserializer (input, out addedOffset, offset);
		}
	}
}