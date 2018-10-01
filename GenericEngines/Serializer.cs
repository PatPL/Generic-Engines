using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public static class Serializer {
		delegate A SerializerFunc<A, B> (B b);
		delegate A DeserializerFunc<A, B, C, D> (B b, out C c, D d);

		private static readonly SerializerFunc<byte[], Engine>[] serializers = {
			(Engine e) => {
				
				//Serializer v.0
				short version = 0;

				int i = 0;
				byte[] output = new byte[
					2 + //short - Version header
					1 + //bool - Active
					(e.Name.Length + 2) + //1B * length + 2B length header - Name
					8 + //Double - Mass
					8 + //Double - Thrust
					8 + //Double - AtmIsp
					8 + //Double - VacIsp
					e.PropellantRatio.Count * 10 + 2 + //(2B + 8B) * count + 2B length header - PropellantRatio
					8 + //Double - Width
					8 + //Double - Height
					8 + //Double - Gimbal
					4 //int - Cost
				];

				//short - Version
				output[i++] = (byte) (version / 256);
				output[i++] = (byte) (version % 256);

				//bool - Active
				output[i++] = (byte) (e.Active ? 1 : 0);

				//String + 2B length header - Name
				//String length header
				output[i++] = (byte) (e.Name.Length / 256);
				output[i++] = (byte) (e.Name.Length % 256);
				//String data
				foreach (char c in e.Name) {
					output[i++] = Convert.ToByte (c);
				}

				//Double - Mass
				foreach (byte b in BitConverter.GetBytes (e.Mass)) {
					output[i++] = b;
				}

				//Double - Thrust
				foreach (byte b in BitConverter.GetBytes (e.Thrust)) {
					output[i++] = b;
				}

				//Double - AtmIsp
				foreach (byte b in BitConverter.GetBytes (e.AtmIsp)) {
					output[i++] = b;
				}

				//Double - VacIsp
				foreach (byte b in BitConverter.GetBytes (e.VacIsp)) {
					output[i++] = b;
				}

				//(2B + 8B) * count + 2B length header - PropellantRatio
				//Length header
				output[i++] = (byte) (e.PropellantRatio.Count / 256);
				output[i++] = (byte) (e.PropellantRatio.Count % 256);
				//Data
				foreach (FuelRatioElement f in e.PropellantRatio) {
					output[i++] = (byte) ((int) f.Propellant / 256);
					output[i++] = (byte) ((int) f.Propellant % 256);
					foreach (byte b in BitConverter.GetBytes (f.Ratio)) {
						output[i++] = b;
					}
				}

				//Double - Width
				foreach (byte b in BitConverter.GetBytes (e.Width)) {
					output[i++] = b;
				}

				//Double - Height
				foreach (byte b in BitConverter.GetBytes (e.Height)) {
					output[i++] = b;
				}

				//Double - Gimbal
				foreach (byte b in BitConverter.GetBytes (e.Gimbal)) {
					output[i++] = b;
				}

				//int - Cost
				foreach (byte b in BitConverter.GetBytes (e.Cost)) {
					output[i++] = b;
				}

				return output;
			},
			(Engine e) => {
				
				//Serializer v.1
				short version = 1;

				int i = 0;
				byte[] output = new byte[
					2 + //short - Version header
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
					1 //bool - NeedsUllage
				];

				//short - Version
				output[i++] = (byte) (version / 256);
				output[i++] = (byte) (version % 256);

				//bool - Active
				output[i++] = (byte) (e.Active ? 1 : 0);

				//String + 2B length header - Name
				//String length header
				output[i++] = (byte) (e.Name.Length / 256);
				output[i++] = (byte) (e.Name.Length % 256);
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
				output[i++] = (byte) (e.PropellantRatio.Count / 256);
				output[i++] = (byte) (e.PropellantRatio.Count % 256);
				//Data
				foreach (FuelRatioElement f in e.PropellantRatio) {
					output[i++] = (byte) ((int) f.Propellant / 256);
					output[i++] = (byte) ((int) f.Propellant % 256);
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

				return output;
			},
			(Engine e) => {
				
				//Serializer v.2
				short version = 2;

				int i = 0;
				byte[] output = new byte[
					2 + //short - Version header
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
					1 //bool - FuelVolumeRatios
				];
				
				//short - Version
				output[i++] = (byte) (version / 256);
				output[i++] = (byte) (version % 256);

				//bool - Active
				output[i++] = (byte) (e.Active ? 1 : 0);

				//String + 2B length header - Name
				//String length header
				output[i++] = (byte) (e.Name.Length / 256);
				output[i++] = (byte) (e.Name.Length % 256);
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
				output[i++] = (byte) (e.PropellantRatio.Count / 256);
				output[i++] = (byte) (e.PropellantRatio.Count % 256);
				//Data
				foreach (FuelRatioElement f in e.PropellantRatio) {
					output[i++] = (byte) ((int) f.Propellant / 256);
					output[i++] = (byte) ((int) f.Propellant % 256);
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

				return output;
			},
			(Engine e) => {
				
				//Serializer v.3
				//Now with more Little Endian
				short version = 3;

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
					8) //double - CycleReliability10k
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

				return output;
			}
		};

		private static readonly DeserializerFunc<Engine, byte[], int, int>[] deserializers = {
			(byte[] input, out int addedOffset, int offset) => {
				
				//Deserializer v.0
				
				Engine output = Engine.New ();
				int i = offset;

				i += 2; //short - Version header

				//bool - Active
				output.Active = input[i++] == 1;

				//String + 2B length header - Name
				{
					int stringLength = 0;
					stringLength += input[i++];
					stringLength *= 256;
					stringLength += input[i++];

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
					dataLength += input[i++];
					dataLength *= 256;
					dataLength += input[i++];

					output.PropellantRatio.Clear (); //Constructor gives one element to this list

					FuelType fuelType = 0;
					for (int c = 0; c < dataLength; ++c) {
						fuelType = 0;
						fuelType += input[i++];
						fuelType = ((FuelType) (((int) fuelType) * 256)); //c# kurwo. fuelType *= 256? pfff
						fuelType += input[i++];

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

				addedOffset = i - offset;
				return output;
			},
			(byte[] input, out int addedOffset, int offset) => {
				
				//Deserializer v.1
				
				Engine output = Engine.New ();
				int i = offset;

				i += 2; //short - Version header

				//bool - Active
				output.Active = input[i++] == 1;

				//String + 2B length header - Name
				{
					int stringLength = 0;
					stringLength += input[i++];
					stringLength *= 256;
					stringLength += input[i++];

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
					dataLength += input[i++];
					dataLength *= 256;
					dataLength += input[i++];

					output.PropellantRatio.Clear (); //Constructor gives one element to this list

					FuelType fuelType = 0;
					for (int c = 0; c < dataLength; ++c) {
						fuelType = 0;
						fuelType += input[i++];
						fuelType = ((FuelType) (((int) fuelType) * 256)); //c# kurwo. fuelType *= 256? pfff
						fuelType += input[i++];

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

				addedOffset = i - offset;
				return output;
			},
			(byte[] input, out int addedOffset, int offset) => {
				
				//Deserializer v.2
				
				Engine output = Engine.New ();
				int i = offset;

				i += 2; //short - Version header

				//bool - Active
				output.Active = input[i++] == 1;

				//String + 2B length header - Name
				{
					int stringLength = 0;
					stringLength += input[i++];
					stringLength *= 256;
					stringLength += input[i++];

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
					dataLength += input[i++];
					dataLength *= 256;
					dataLength += input[i++];

					output.PropellantRatio.Clear (); //Constructor gives one element to this list

					FuelType fuelType = 0;
					for (int c = 0; c < dataLength; ++c) {
						fuelType = 0;
						fuelType += input[i++];
						fuelType = ((FuelType) (((int) fuelType) * 256)); //c# kurwo. fuelType *= 256? pfff
						fuelType += input[i++];

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

				//bool - FuelVolumeRatios
				output.FuelVolumeRatios = input[i++] == 1;

				addedOffset = i - offset;
				return output;
			},
			(byte[] input, out int addedOffset, int offset) => {
				
				//Deserializer v.3
				//Lil' Endian
				Engine output = Engine.New ();
				int i = offset;

				i += 2; //short - Version header

				//bool - Active
				output.Active = input[i++] == 1;

				//String + 2B length header - Name
				{
					int stringLength = 0;
					stringLength += input[i++];
					stringLength += input[i++] * 256;

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
					dataLength += input[i++];
					dataLength += input[i++] * 256;

					output.PropellantRatio.Clear (); //Constructor gives one element to this list

					FuelType fuelType = 0;
					for (int c = 0; c < dataLength; ++c) {
						fuelType = 0;
						fuelType += input[i++];
						fuelType += input[i++] * 256;

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

				//bool - FuelVolumeRatios
				output.FuelVolumeRatios = input[i++] == 1;
				
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

				addedOffset = i - offset;
				return output;
			}
		};
		
		public static byte[] Serialize (Engine e, short? version = null) {
			version = version ?? (short) (serializers.Length - 1);

			return serializers[(int) version] (e);
		}

		public static Engine Deserialize (byte[] input, out int addedOffset, int offset = 0, short? version = null) {
			if (version is null) {
				version = 0;
				//Big endian - backwards compatibility
				version += input[offset];
				version *= 256;
				version += input[offset + 1];
			}

			return deserializers[(int) version] (input, out addedOffset, offset);
		}
	}
}