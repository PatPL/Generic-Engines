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
			}
		};

		private static readonly DeserializerFunc<Engine, byte[], int, int>[] deserializers = {
			(byte[] input, out int addedOffset, int offset) => {
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

				//Double - Mass
				output.Mass = BitConverter.ToDouble (input, i);
				i += 8;

				//Double - Thrust
				output.Thrust = BitConverter.ToDouble (input, i);
				i += 8;

				//Double - AtmIsp
				output.AtmIsp = BitConverter.ToDouble (input, i);
				i += 8;

				//Double - VacIsp
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
			
				//Double - Width
				output.Width = BitConverter.ToDouble (input, i);
				i += 8;

				//Double - Height
				output.Height = BitConverter.ToDouble (input, i);
				i += 8;

				//Double - Gimbal
				output.Gimbal = BitConverter.ToDouble (input, i);
				i += 8;

				//int - Cost
				output.Cost = BitConverter.ToInt32 (input, i);
				i += 4;

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
				version += input[offset];
				version *= 256;
				version += input[offset + 1];
			}

			return deserializers[(int) version] (input, out addedOffset, offset);
		}
	}
}