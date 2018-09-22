using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class Engine {
		public bool Active { get; set; }
		public string Name { get; set; }
		public double Mass { get; set; }
		public double Thrust { get; set; }
		public double AtmIsp { get; set; }
		public double VacIsp { get; set; }

		public Engine (
			bool _Active = false,
			string _Name = "New Engine",
			double _Mass = 1.0,
			double _Thrust = 100.0,
			double _AtmIsp = 100.0,
			double _VacIsp = 200.0
		) {
			Active = _Active;
			Name = _Name;
			Mass = _Mass;
			Thrust = _Thrust;
			AtmIsp = _AtmIsp;
			VacIsp = _VacIsp;
		}

		public static byte[] Serialize (Engine e) {
			int i = 0;
			byte[] output = new byte[
				1 + //Boolean - Active
				(e.Name.Length + 2) + //String + 2B length header - Name
				8 + //Double - Mass
				8 + //Double - Thrust
				8 + //Double - AtmIsp
				8 //Double - VacIsp
			];

			//Boolean - Active
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

			return output;
		}

		public byte[] Serialize () {
			return Serialize (this);
		}

		public static Engine Deserialize (byte[] input) {
			Engine output = new Engine ();
			int i = 0;

			//Boolean - Active
			output.Active = input[i++] == 1;

			//String + 2B length header - Name
			//String length header
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

			return output;
		}
	}
}
