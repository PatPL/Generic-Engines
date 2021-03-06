﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	/// <summary>
	/// This class is used to generate the 'All' tank definition CFG.
	/// </summary>
	public static class AllTankDefinition {
		private static string SingleResourceTankDefinition (FuelType t) {
			return $@"
				TANK
				{{
					name = {FuelTypeList.GetFuelName (t)}
					mass = 0.00007
					utilization = {FuelTypeList.GetFuelUtilisation (t)}
					fillable = True
					amount = 0.0
					maxAmount = 0.0
				}}
			";
		}

		private static string AllResourcesTankDefinitions () {
			string output = "";
			
			foreach (FuelType i in Enum.GetValues (typeof (FuelType))) {
				output += SingleResourceTankDefinition (i);
			}

			return output;
		}
		
		/// <summary>
		/// Get the entire config for 'All' tank.
		/// </summary>
		public static string Get {
			get {
				string output = "";
				
				output = $@"
					TANK_DEFINITION {{
						name = All
						highltPressurized = true
						basemass = 0.00007 * volume

						{AllResourcesTankDefinitions ()}

					}}
				";

				return output.Compact ();
			}
		}
		
	}
}
