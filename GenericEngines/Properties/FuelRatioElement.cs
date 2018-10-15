using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class FuelRatioElement {

		public FuelType Propellant { get; set; }
		public double Ratio { get; set; }

		public FuelRatioElement () {
			Propellant = FuelType.Hydrazine;
			Ratio = 1.0;
		}

		public FuelRatioElement (
			FuelType _Propellant = FuelType.Hydrazine,
			double _Ratio = 1.0
		) {
			Propellant = _Propellant;
			Ratio = _Ratio;
		}

	}
}
