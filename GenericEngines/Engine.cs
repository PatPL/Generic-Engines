using System;
using System.Collections.Generic;
using System.Globalization;
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
		public bool FuelVolumeRatios { get; set; } //2
		public bool EnableTestFlight { get; set; } //3
		public int RatedBurnTime { get; set; } //3
		public double StartReliability0 { get; set; } //3
		public double StartReliability10k { get; set; } //3
		public double CycleReliability0 { get; set; } //3
		public double CycleReliability10k { get; set; } //3
		public double AlternatorPower { get; set; } //4
		public bool AdvancedGimbal { get; set; } //5
		public double GimbalNX { get; set; } //5
		public double GimbalPX { get; set; } //5
		public double GimbalNY { get; set; } //5
		public double GimbalPY { get; set; } //5

		// Serializer

		public bool TestFlightConfigNotDefault {
			get {
				Engine comparison = new Engine ();
				return
					EnableTestFlight != comparison.EnableTestFlight ||
					RatedBurnTime != comparison.RatedBurnTime ||
					StartReliability0 != comparison.StartReliability0 ||
					StartReliability10k != comparison.StartReliability10k ||
					CycleReliability0 != comparison.CycleReliability0 ||
					CycleReliability10k != comparison.CycleReliability10k
				;
			}
		}

		public bool GimbalConfigNotDefault {
			get {
				Engine comparison = new Engine ();
				return
					AdvancedGimbal != comparison.AdvancedGimbal ||
					GimbalNX != comparison.GimbalNX ||
					GimbalPX != comparison.GimbalPX ||
					GimbalNY != comparison.GimbalNY ||
					GimbalPY != comparison.GimbalPY
				;
			}
		}

		// Labels

		public string GimbalStatus {
			get {
				if (AdvancedGimbal) {
					return $"X:{GimbalNX.ToString (CultureInfo.InvariantCulture)}°:{GimbalPX.ToString (CultureInfo.InvariantCulture)}°, Y:{GimbalNY.ToString (CultureInfo.InvariantCulture)}°:{GimbalPY.ToString (CultureInfo.InvariantCulture)}°";
				} else {
					return $"{Gimbal.ToString (CultureInfo.InvariantCulture)}°";
				}
			}
		}

		public string TestFlightStatus {
			get {
				if (EnableTestFlight) {
					return $"Enabled | {RatedBurnTime}s | {CycleReliability10k.ToString (CultureInfo.InvariantCulture)}%";
				} else if (TestFlightConfigNotDefault) {
					return "Disabled, but configured";
				} else {
					return "Disabled";
				}
			}
		}

		public string Dimensions {
			get {
				return $"{Width.ToString (CultureInfo.InvariantCulture)}m x {Height.ToString (CultureInfo.InvariantCulture)}m";
			}
		}

		public Engine () {
			Active = false;
			Name = "New Engine";
			Mass = 1.0;
			Thrust = 1000.0;
			AtmIsp = 250.0;
			VacIsp = 300.0;
			PropellantRatio = new FuelRatioList () { new FuelRatioElement () };
			Width = 1.0;
			Height = 2.0;
			Gimbal = 6.0;
			Cost = 1000;
			MinThrust = 90.0;
			Ignitions = 1;
			PressureFed = false;
			NeedsUllage = true;
			FuelVolumeRatios = false;
			EnableTestFlight = false;
			RatedBurnTime = 180;
			StartReliability0 = 92.0;
			StartReliability10k = 96.0;
			CycleReliability0 = 90.0;
			CycleReliability10k = 98.0;
			AlternatorPower = 0.0;
			AdvancedGimbal = false;
			GimbalNX = 30.0;
			GimbalPX = 30.0;
			GimbalNY = 0.0;
			GimbalPY = 0.0;
		}

		public static Engine New () {
			return new Engine ();
		}
	}
}
