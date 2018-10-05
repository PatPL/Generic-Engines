using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
		public Model ModelID { get; set; } //-
		public Plume PlumeID { get; set; } //-

		// Exporter

		public string ModelConfig {
			get {
				string model = "";

				ModelInfo modelInfo = GetModelInfo;

				double heightScale = Height / modelInfo.OriginalHeight;
				double widthScale = Width / heightScale / modelInfo.OriginalWidth;

				model = $@"
					MODEL
					{{
						model = {GetModelInfo.ModelPath}
						scale = {widthScale.ToString (CultureInfo.InvariantCulture)}, 1, {widthScale.ToString (CultureInfo.InvariantCulture)}
					}}
					scale = 1
					rescaleFactor = {heightScale.ToString (CultureInfo.InvariantCulture)}

					node_stack_top = {modelInfo.NodeStackTop}
					node_stack_bottom = {modelInfo.NodeStackBottom}
					node_attach = {modelInfo.NodeStackAttach}
				";

				return model;
			}
		}

		public string PlumeConfig {
			get {
				string plume = "";

				PlumeInfo plumeInfo = GetPlumeInfo;
				ModelInfo modelInfo = GetModelInfo;

				plume = $@"
					@PART[{EngineID}]:FOR[RealPlume]:NEEDS[SmokeScreen]
					{{
						PLUME
						{{
							name = {plumeInfo.PlumeID}
							transformName = {modelInfo.ThrustTransformName}
							localRotation = 0,0,0
							localPosition = 0,0,{(modelInfo.PlumePosition + plumeInfo.PositionOffset).ToString (CultureInfo.InvariantCulture)}
							fixedScale = {(plumeInfo.Scale * Width).ToString (CultureInfo.InvariantCulture)}
							energy = {(Math.Log (Thrust + 5, 10) / 3 * plumeInfo.EnergyMultiplier).ToString (CultureInfo.InvariantCulture)}
							speed = 1
						}}

						@MODULE[ModuleEngines*]
						{{
							%powerEffectName = {plumeInfo.PlumeID}
							!fxOffset = NULL
						}}

						@MODULE[ModuleEngineConfigs]
						{{
							@CONFIG,*
							{{
								%powerEffectName = {plumeInfo.PlumeID}
							}}
						}}
					}}
				";

				return plume;
			}
		}

		public ModelInfo GetModelInfo {
			get {
				return ModelList.Get (ModelID);
			}
		}

		public PlumeInfo GetPlumeInfo {
			get {
				return PlumeList.Get (PlumeID);
			}
		}

		public string EngineID {
			get {
				string output = $"GE-{Name.Replace (' ', '-')}";
				output = Regex.Replace (output, "[<>,+*=]", "");

				return output;
			}
		}

		public string PropellantConfig { 
			get {
				bool isElectric = false;
				FuelRatioList fuelRatios = new FuelRatioList ();
				if (!FuelVolumeRatios) {

					foreach (FuelRatioElement i in PropellantRatio) {
						if (i.Propellant == FuelType.ElectricCharge) {
							isElectric = true;
						} else {
							fuelRatios.Add (new FuelRatioElement (i.Propellant, i.Ratio / FuelDensity.Get[(int) i.Propellant] / 1000));
						}
					}

				} else {

					foreach (FuelRatioElement i in PropellantRatio) {
						if (i.Propellant == FuelType.ElectricCharge) {
							isElectric = true;
						} else {
							fuelRatios.Add (new FuelRatioElement (i.Propellant, i.Ratio));
						}
					}

				}

				if (isElectric) {

					double normalFuelRatios = 0.0;// Will be used to calculate propellant flow rate
					double averageDensity = 0.0;// t/l
					double electricRatio = PropellantRatio.Find (s => s.Propellant == FuelType.ElectricCharge).Ratio;// kW

					foreach (FuelRatioElement i in fuelRatios) {
						normalFuelRatios += i.Ratio;
						averageDensity += i.Ratio * FuelDensity.Get[(int) i.Propellant];
					}

					averageDensity /= normalFuelRatios; // t/l

					double x = VacIsp; // s
					x *= 9.8066; // N*s/kg
					x = 1 / x; // kg/N*s -> t/kN*s
					x /= averageDensity; // l/kN*s
					x *= Thrust; // l/s
										//normalFuelRatios    = x units/s
										//actualElectricRatio = y units/s (kW)
					electricRatio = electricRatio * normalFuelRatios / x; //result

					fuelRatios.Add (new FuelRatioElement (FuelType.ElectricCharge, electricRatio));
				}



				string propellants = "";
				bool firstPropellant = true;
				foreach (FuelRatioElement i in fuelRatios) {
					propellants += $@"
						PROPELLANT
						{{
							name = {FuelName.Name (i.Propellant)}
							ratio = {i.Ratio.ToString (CultureInfo.InvariantCulture)}
							DrawGauge = {firstPropellant}
						}}
					";

					firstPropellant = false;
				}

				return propellants;
			}
		}

		public string GimbalConfig {
			get {
				string gimbal = "";

				if (AdvancedGimbal) {
					gimbal = $@"
						MODULE
						{{
							name = ModuleGimbal
							gimbalTransformName = thrustTransform
							gimbalRangeYP = {GimbalPY.ToString (CultureInfo.InvariantCulture)}
							gimbalRangeYN = {GimbalNY.ToString (CultureInfo.InvariantCulture)}
							gimbalRangeXP = {GimbalPX.ToString (CultureInfo.InvariantCulture)}
							gimbalRangeXN = {GimbalNX.ToString (CultureInfo.InvariantCulture)}
 							useGimbalResponseSpeed = false
						}}
					";
				} else {
					gimbal = $@"
						MODULE
						{{
							name = ModuleGimbal
							gimbalTransformName = thrustTransform
							useGimbalResponseSpeed = false
							gimbalRange = {Gimbal.ToString (CultureInfo.InvariantCulture)}
						}}
					";
				}

				return gimbal;
			}
		}

		public string TestFlightConfig {
			get {
				string testflight = "";

				if (EnableTestFlight) {
					testflight = $@"
						@PART[*]:HAS[@MODULE[ModuleEngineConfigs]:HAS[@CONFIG[{EngineID}]],!MODULE[TestFlightInterop]]:BEFORE[zTestFlight]
						{{
							TESTFLIGHT
							{{
								name = {EngineID}
								ratedBurnTime = {RatedBurnTime}
								ignitionReliabilityStart = {(StartReliability0 / 100).ToString (CultureInfo.InvariantCulture)}
								ignitionReliabilityEnd = {(StartReliability10k / 100).ToString (CultureInfo.InvariantCulture)}
								cycleReliabilityStart = {(CycleReliability0 / 100).ToString (CultureInfo.InvariantCulture)}
								cycleReliabilityEnd = {(CycleReliability10k / 100).ToString (CultureInfo.InvariantCulture)}
							}}
						}}
					";
				}

				return testflight;
			}
		}

		public string AlternatorConfig {
			get {
				string alternator = "";

				if (AlternatorPower > 0) {
					alternator = $@"
						MODULE
						{{
							name = ModuleAlternator
							RESOURCE
							{{
								name = ElectricCharge
								rate = {AlternatorPower.ToString (CultureInfo.InvariantCulture)}
							}}
						}}
					";
				}

				return alternator;
			}
		}

		public double MinThrustPercent {
			get {
				return (Math.Max (Math.Min (MinThrust, 100), 0) / 100);
			}
		}

		public int IgnitionsCount {
			get {
				return Ignitions < 0 ? 0 : Ignitions;
			}
		}

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

		public string VisualStatus {
			get {
				return ModelList.GetName (ModelID);
			}
		}

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
			ModelID = Model.LR91;
		}

		public static Engine New () {
			return new Engine ();
		}
	}
}
