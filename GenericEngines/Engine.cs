using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class Engine {
		// Serializer versions
		public bool Active { get; set; } //0
		public string Name { get; set; } //0
		public double Mass { get; set; } //0
		public double Thrust { get; set; } //0
		public double AtmIsp { get; set; } //0
		public double VacIsp { get; set; } //0
		public FuelRatioList PropellantRatio { get; set; } //0
		public double Width { get; set; } //0
		public double Height { get; set; } //0
		public double Gimbal { get; set; } //0
		public int Cost { get; set; } //0
		public double MinThrust { get; set; } //1
		public int Ignitions { get; set; } //1
		public bool PressureFed { get; set; } //1
		public bool NeedsUllage { get; set; } //1

		public Engine (
			bool _Active = false,
			string _Name = "New Engine",
			double _Mass = 1.0,
			double _Thrust = 100.0,
			double _AtmIsp = 100.0,
			double _VacIsp = 200.0,
			FuelRatioList _PropellantRatio = null,
			double _Width = 1.0,
			double _Height = 1.0,
			double _Gimbal = 5.0,
			int _Cost = 500,
			double _MinThrust = 80.0,
			int _Ignitions = 1,
			bool _PressureFed = false,
			bool _NeedsUllage = true
		) {
			Active = _Active;
			Name = _Name;
			Mass = _Mass;
			Thrust = _Thrust;
			AtmIsp = _AtmIsp;
			VacIsp = _VacIsp;
			PropellantRatio = _PropellantRatio ?? new FuelRatioList () { new FuelRatioElement () };
			Width = _Width;
			Height = _Height;
			Gimbal = _Gimbal;
			Cost = _Cost;
			MinThrust = _MinThrust;
			Ignitions = _Ignitions;
			PressureFed = _PressureFed;
			NeedsUllage = _NeedsUllage;
		}

		public static Engine New () {
			return new Engine ();
		}
	}
}
