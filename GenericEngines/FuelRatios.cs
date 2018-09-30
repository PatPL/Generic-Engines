/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class FuelRatio {

		public Dictionary<FuelType, double> ratios;

		public FuelRatio Normalized () {
			double[] ratioValues = ratios.Values.ToArray ();

			double d = 0;

			foreach (double i in ratioValues) {
				d += i;
			}

			FuelRatio output = new FuelRatio ();

			foreach (KeyValuePair<FuelType, double> i in ratios) {
				output.Add (i.Key, i.Value / d);
			}

			return output;
		}

		public override string ToString () {
			string output = "";

			foreach (KeyValuePair<FuelType, double> i in ratios) {
				output += $"{i.Value}:";
			}

			output = output.Trim (':');

			return output;
		}

		public void Add (FuelType fuel, double ratio) {
			ratios.Add (fuel, ratio);
		}

		public FuelRatio () {
			ratios = new Dictionary<FuelType, double> ();
		}

		public FuelRatio (FuelType fuel, double ratio) {
			ratios = new Dictionary<FuelType, double> ();
			ratios.Add (fuel, ratio);
		}

		public static FuelRatio Get (FuelType fuel, double ratio) {
			FuelRatio output = new FuelRatio (fuel, ratio);
			return output;
		}

	}
}
*/