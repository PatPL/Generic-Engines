using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class FuelRatioList :List<FuelRatioElement> {

		public override string ToString () {
			string output = "";
			
			foreach (FuelRatioElement i in base.ToArray ()) {
				output += $"{i.Ratio}:";
			}

			output = output.Trim (':');

			return output;
		}

	}
}
