using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class FuelRatioList :List<FuelRatioElement> {

		public override string ToString () {
			string output = "";

			foreach (FuelRatioElement i in base.ToArray ()) {
				output += $"{i.Ratio.ToString (CultureInfo.InvariantCulture)}:";
			}

			output = output.Trim (':');

			if (base.Count <= 2) {
				output += " ";

				foreach (FuelRatioElement i in base.ToArray ()) {
					output += $"{FuelName.Name (i.Propellant)}:";
				}

				output = output.Trim (':');
			}

			return output;
		}

	}
}
