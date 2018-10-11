using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class FuelRatioList : List<FuelRatioElement> {

		public override string ToString () {
			string output = "";
			string electricSuffix = "";

			foreach (FuelRatioElement i in base.ToArray ()) {
				if (i.Propellant != FuelType.ElectricCharge) {
					output += $"{i.Ratio.Str ()}:";
				} else {
					electricSuffix = $" | Electric: {i.Ratio.Str ()}kW";
				}
			}

			output = output.Trim (':');

			if (base.Count <= 2) {
				output += " ";

				foreach (FuelRatioElement i in base.ToArray ()) {
					if (i.Propellant != FuelType.ElectricCharge) {
						output += $"{FuelName.Name (i.Propellant)}:";
					}
				}

				output = output.Trim (':');
			}

			output += electricSuffix;

			return output;
		}

	}
}
