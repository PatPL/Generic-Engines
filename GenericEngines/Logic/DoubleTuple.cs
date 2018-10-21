using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	/// <summary>
	/// Contains two double variables
	/// </summary>
	public class DoubleTuple {
		
		public double Item1 { get; set; }
		public double Item2 { get; set; }

		public DoubleTuple () {
			Item1 = 0;
			Item2 = 0;
		}

		public DoubleTuple (double i1, double i2) {
			Item1 = i1;
			Item2 = i2;
		}

	}
}
