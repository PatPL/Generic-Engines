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
	}
}
