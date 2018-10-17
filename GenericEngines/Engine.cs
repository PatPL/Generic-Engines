using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;
using System.ComponentModel;

namespace GenericEngines {
	public class Engine : INotifyPropertyChanged {
		private double _atmIsp;
		private double _vacIsp;
		private double _thrust;
		private string _name;

		// Serializer versions
		public bool Active { get; set; } //0
		public string Name { get => _name; set { _name = value; NotifyPropertyChanged ("RealEngineName"); } } //0
		public double Mass { get; set; } //0
		public double Thrust { get => _thrust; set { _thrust = value; NotifyPropertyChanged ("MinimumThrustStatus"); NotifyPropertyChanged ("MassStatus"); } } //0
		public double AtmIsp { get => _atmIsp; set { _atmIsp = value; NotifyPropertyChanged ("ThrustStatus"); } } //0
		public double VacIsp { get => _vacIsp; set { _vacIsp = value; NotifyPropertyChanged ("ThrustStatus"); } } //0
		public List<FuelRatioElement> PropellantRatio { get; set; } //0
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
		public Model ModelID { get; set; } //6
		public Plume PlumeID { get; set; } //6
		public TechNode TechUnlockNode { get; set; } //7
		public int EntryCost { get; set; } //7
		public string EngineName { get; set; } //7
		public string EngineManufacturer { get; set; } //7
		public string EngineDescription { get; set; } //7
		public bool UseBaseWidth { get; set; } //8
		public EngineType EngineVariant { get; set; } //9
		public double TanksVolume { get; set; } //9
		public List<FuelRatioElement> TanksContents { get; set; } //9
		public List<Tuple<double, double>> ThrustCurve { get; set; } //9

		//This is necessary to fix deleting
		public static int UIDc = 1;
		public int UID { get; set; }

		// Exporter
		
		public string TankConfig {
			get {
				string output = "";
				
				if (TanksVolume > 0.0) {
					string contents = "";
					double usedVolume = 0.0;
					foreach (FuelRatioElement i in TanksContents) {
						
						//Volume in real L
						//volume 1 xenon = 100 units
						double volume = Math.Min (i.Ratio / FuelUtilisation.Get (i.Propellant), TanksVolume - usedVolume);

						contents += $@"
							TANK
							{{
								name = {FuelName.Name (i.Propellant)}
								amount = {(volume * FuelUtilisation.Get (i.Propellant)).Str ()}
								maxAmount = {(volume * FuelUtilisation.Get (i.Propellant)).Str ()}
							}}
						";

						usedVolume += volume;

						if (usedVolume == TanksVolume) {
							break;
						}
					}

					output = $@"
						MODULE
						{{
							name = ModuleFuelTanks
							basemass = -1
							type = All
							volume = {TanksVolume.Str ()}

							{contents}

						}}
					";
				}

				return output;
			}
		}

		public string ModelConfig {
			get {
				string model = "";

				ModelInfo modelInfo = GetModelInfo;

				double heightScale = Height / modelInfo.OriginalHeight;
				double widthScale = Width / heightScale / GetOriginalWidth;

				model = $@"
					MODEL
					{{
						model = {modelInfo.ModelPath}
						{modelInfo.TextureDefinitions}
						scale = {widthScale.Str ()}, 1, {widthScale.Str ()}
					}}
					scale = 1
					rescaleFactor = {heightScale.Str ()}

					node_stack_top = 0.0, {modelInfo.NodeStackTop.Str ()}, 0.0, 0.0, 1.0, 0.0, 1
					node_stack_bottom = 0.0, {modelInfo.NodeStackBottom.Str ()}, 0.0, 0.0, -1.0, 0.0, 1
					node_stack_hide = 0.0, {(modelInfo.NodeStackBottom + 0.001).Str ()}, 0.0, 0.0, 0.0, 1.0, 0
					{/* Hopefully no one will try to attach things sideways */""}
					node_attach = 0.0, {modelInfo.NodeStackTop.Str ()}, 0.0, 0.0, 1.0, 0.0, 1
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
							localPosition = 0,0,{(modelInfo.PlumePosition + plumeInfo.PositionOffset + plumeInfo.FinalOffset).Str ()}
							fixedScale = {(modelInfo.PlumeSizeMultiplier * plumeInfo.Scale * Width / GetOriginalWidth).Str ()}
							flareScale = 0
							energy = {(Math.Log (Thrust + 5, 10) / 3 * plumeInfo.EnergyMultiplier).Str ()}
							speed = {Math.Max ((Math.Log (VacIsp, 2) / 1.5) - 4.5, 0.2).Str ()}
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

		public string HiddenObjectsConfig {
			get {
				string output = "";
				ModelInfo modelInfo = GetModelInfo;

				if (modelInfo.HiddenMuObjects != null) {
					foreach (string i in modelInfo.HiddenMuObjects) {
						output += $@"
							MODULE
							{{
								name = ModuleJettison
								jettisonName = {i}
								bottomNodeName = hide
								isFairing = True
							}}
						";
					}
				}

				return output;
			}
		}

		public string EngineID {
			get {
				string output = $"GE-{Name.Replace (' ', '-')}";
				output = Regex.Replace (output, "[<>,+*=_]", "-");

				return output;
			}
		}

		public string PropellantConfig {
			get {
				bool isElectric = false;
				List<FuelRatioElement> fuelRatios = new List<FuelRatioElement> ();
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
							ratio = {i.Ratio.Str ()}
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
							gimbalTransformName = {GetModelInfo.GimbalTransformName}
							gimbalRangeYP = {GimbalPY.Str ()}
							gimbalRangeYN = {GimbalNY.Str ()}
							gimbalRangeXP = {GimbalPX.Str ()}
							gimbalRangeXN = {GimbalNX.Str ()}
 							useGimbalResponseSpeed = false
						}}
					";
				} else {
					gimbal = $@"
						MODULE
						{{
							name = ModuleGimbal
							gimbalTransformName = {GetModelInfo.GimbalTransformName}
							useGimbalResponseSpeed = false
							gimbalRange = {Gimbal.Str ()}
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
								ignitionReliabilityStart = {(StartReliability0 / 100).Str ()}
								ignitionReliabilityEnd = {(StartReliability10k / 100).Str ()}
								cycleReliabilityStart = {(CycleReliability0 / 100).Str ()}
								cycleReliabilityEnd = {(CycleReliability10k / 100).Str ()}
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
								rate = {AlternatorPower.Str ()}
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

		public double GetOriginalWidth => (UseBaseWidth ? GetModelInfo.OriginalBaseWidth : GetModelInfo.OriginalWidth);

		public string RealEngineName => (EngineName == "" ? Name : EngineName);

		public string StagingIcon {
			get {
				switch (EngineVariant) {
					case EngineType.Liquid:
					return "LIQUID_ENGINE";
					case EngineType.Solid:
					return "SOLID_BOOSTER";
					//case EngineType.RCS:
					//return "RCS_MODULE";
					default:
					return "unknown";
				}
			}
		}

		public string EngineTypeConfig {
			get {
				switch (EngineVariant) {
					case EngineType.Liquid:
					return "LiquidFuel";
					case EngineType.Solid:
					return "SolidBooster";
					default:
					return "unknown";
				}
			}
		}

		public string AllowShutdown {
			get {
				switch (EngineVariant) {
					case EngineType.Liquid:
					return "True";
					case EngineType.Solid:
					return "False";
					default:
					return "True";
				}
			}
		}

		public string UseEngineResponseTime {
			get {
				switch (EngineVariant) {
					case EngineType.Liquid:
					return "True";
					case EngineType.Solid:
					return "False";
					default:
					return "True";
				}
			}
		}

		public string LockThrottle {
			get {
				switch (EngineVariant) {
					case EngineType.Liquid:
					return "False";
					case EngineType.Solid:
					return "True";
					default:
					return "False";
				}
			}
		}

		public int IgnitionsCount {
			get {
				return Ignitions < 0 ? 0 : Ignitions;
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

		public bool ManufacturerNotDefault {
			get {
				Engine comparison = new Engine ();
				return EngineManufacturer != comparison.EngineManufacturer;
			}
		}

		public bool DescriptionNotDefault {
			get {
				Engine comparison = new Engine ();
				return EngineDescription != comparison.EngineDescription;
			}
		}

		// Labels

		public string PropellantRatioLabel {
			get {
				string output = "";
				string electricSuffix = "";

				foreach (FuelRatioElement i in PropellantRatio.ToArray ()) {
					if (i.Propellant != FuelType.ElectricCharge) {
						output += $"{i.Ratio.Str ()}:";
					} else {
						electricSuffix = $" | Electric: {i.Ratio.Str ()}kW";
					}
				}

				output = output.Trim (':');

				if (PropellantRatio.Count <= 2) {
					output += " ";

					foreach (FuelRatioElement i in PropellantRatio) {
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

		public string TankStatus {
			get {
				if (TanksVolume > 0.0) {
					return $"{TanksVolume.Str (3)}L";
				} else {
					return "Disabled";
				}
			}
		}

		public string MassStatus {
			get {
				if (Settings.GetBool (Setting.MoreEngineInfo)) {
					return $"{Mass.Str ()}t (TWR: {(Thrust / 9.80665 / Mass).Str (3)})";
				} else {
					return $"{Mass.Str ()}t";
				}
			}
		}

		public string IgnitionsStatus {
			get {
				return Ignitions <= 0 ? "Infinite" : Ignitions.ToString ();
			}
		}

		public string ThrustStatus {
			get {
				if (Settings.GetBool (Setting.MoreEngineInfo)) {
					return $"{Thrust.Str ()}kN (SL: {(Thrust * AtmIsp / VacIsp).Str (3)}kN)";
				} else {
					return $"{Thrust.Str ()}kN";
				}
			}
		}

		public string MinimumThrustStatus {
			get {
				if (Settings.GetBool (Setting.MoreEngineInfo)) {
					return $"{MinThrust}% (Vac: {(Thrust * MinThrust / 100).Str (3)}kN)";
				} else {
					return $"{MinThrust}%";
				}
			}
		}

		public string VisualStatus {
			get {
				return $"{ModelList.GetName (ModelID)}, {PlumeList.GetName (PlumeID)}";
			}
		}

		public string GimbalStatus {
			get {
				if (AdvancedGimbal) {
					return $"X:{GimbalNX.Str ()}°:{GimbalPX.Str ()}°, Y:{GimbalNY.Str ()}°:{GimbalPY.Str ()}°";
				} else {
					return $"{Gimbal.Str ()}°";
				}
			}
		}

		public string TestFlightStatus {
			get {
				if (EnableTestFlight) {
					return $"Enabled | {StartReliability0.Str ()}% - {StartReliability10k.Str ()}% | MTBF: {(int) Math.Round ((1 / (1 - (CycleReliability0 / 100))) * RatedBurnTime)}s - {(int) Math.Round ((1 / (1 - (CycleReliability10k / 100))) * RatedBurnTime)}s";
				} else if (TestFlightConfigNotDefault) {
					return "Disabled, but configured";
				} else {
					return "Disabled";
				}
			}
		}

		public string Dimensions {
			get {
				return $"{Width.Str ()}m x {Height.Str ()}m";
			}
		}

		public string TechNodeStatus => (TechNodes.GetName (TechUnlockNode));

		public Dictionary<TechNode, string> TechNodesWithLabels => TechNodeEnumWrapper.Get;

		public List<ModelEnumWrapper> ModelsWithLabels => ModelEnumWrapper.Get;

		public string CurrentModelTooltip => ModelList.GetTooltip (ModelID);

		public Engine () {
			UID = UIDc++;
			
			Active = false;
			Name = "New Engine";
			Mass = 1.0;
			Thrust = 1000.0;
			AtmIsp = 250.0;
			VacIsp = 300.0;
			PropellantRatio = new List<FuelRatioElement> () { new FuelRatioElement () };
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
			PlumeID = Plume.Kerolox_Upper;
			TechUnlockNode = TechNode.start;
			EntryCost = 10000;
			EngineName = "";
			EngineManufacturer = "Generic Engines";
			EngineDescription = "This engine was generated by Generic Engines";
			UseBaseWidth = false;
			EngineVariant = EngineType.Liquid;
			TanksVolume = 0.0;
			TanksContents = new List<FuelRatioElement> (0);
			ThrustCurve = new List<Tuple<double, double>> (0);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged (string name) {
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (name));
		}

		public void NotifyEveryProperty () {
			foreach (PropertyInfo i in typeof (Engine).GetProperties ()) {
				NotifyPropertyChanged (i.Name);
			}
		}

		public static Engine New () {
			return new Engine ();
		}
	}
}