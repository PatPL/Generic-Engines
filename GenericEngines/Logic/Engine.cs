using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Data;

namespace GenericEngines {
	/// <summary>
	/// The class containing properties of an engine
	/// </summary>
	public class Engine : INotifyPropertyChanged {

		#region Variables

		private double _atmIsp;
		private double _vacIsp;
		private double _thrust;
		private string _name;

		// Serializer versions
		public bool Active { get; set; } //0
		public string Name { get => _name; set { _name = value; NotifyPropertyChanged ("RealEngineName"); } } //0
		public double Mass { get; set; } //0
		public double Thrust { get => _thrust; set { _thrust = value; NotifyPropertyChanged ("MinimumThrustLabel"); NotifyPropertyChanged ("MassLabel"); } } //0
		public double AtmIsp { get => _atmIsp; set { _atmIsp = value; NotifyPropertyChanged ("ThrustLabel"); } } //0
		public double VacIsp { get => _vacIsp; set { _vacIsp = value; NotifyPropertyChanged ("ThrustLabel"); } } //0
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
		public List<DoubleTuple> ThrustCurve { get; set; } //9
		public bool UseTanks { get; set; } //10
		public bool LimitTanks { get; set; } //10
		public Polymorphism PolyType { get; set; } //11
		public string MasterEngineName { get; set; } //11
		public int MasterEngineCost { get; set; } //12
		public double MasterEngineMass { get; set; } //12
		
		//This is necessary to fix deleting
		public static int UIDc = 1;
		public int UID { get; set; }

		#endregion

		#region InputProperties

		public Engine SetMasterEngine {
			set {
				MasterEngineName = value.Name;

				if (!IsNotSlave && MasterEngineName != "") {
					Width = value.Width;
					Height = value.Width;
					ModelID = value.ModelID;
					MasterEngineCost = value.Cost;
					MasterEngineMass = value.Mass;

					if (!DescriptionNotDefault || EngineDescription == value.EngineDescription) {
						EngineDescription = "";
					}
				}

				NotifyEveryProperty ();
			} get {
				//Just to be displayed
				return new Engine () { Name = MasterEngineName };
			}
		}

		#endregion

		#region ExporterProperties

		public string MasterEngineID => $"GE-{MasterEngineName}";

		/// <summary>
		/// Only used by Multi config slave. Returns config setting engine config's entry cost.
		/// </summary>
		public string EngineConfigEntryCostConfig {
			get {
				return $@"
					@ENTRYCOSTMODS:FOR[xxxRP-0]
					{{
						{EngineID} = {EntryCost}
					}}
				";
			}
		}

		public string GetEngineConfig {
			get {
				return $@"
					CONFIG
					{{
						name = {EngineID}
						description = {EngineDescription}
						maxThrust = {Thrust.Str ()}
						minThrust = {(Thrust * MinThrustPercent).Str ()}
						%powerEffectName = {GetPlumeInfo.PlumeID}
						heatProduction = 100
						massMult = {(PolyType == Polymorphism.MultiConfigSlave ? (Mass / MasterEngineMass).Str () : "1")}
						%techRequired = {TechNodeList.GetID (TechUnlockNode)}
						cost = {(PolyType == Polymorphism.MultiConfigSlave ? Cost - MasterEngineCost : 0)}

						{PropellantConfig}

						atmosphereCurve
						{{
							key = 0 {VacIsp.Str ()}
							key = 1 {AtmIsp.Str ()}
						}}

						{ThrustCurveConfig}

						ullage = {UllageNeeded}
						pressureFed = {PressureFed}
						ignitions = {IgnitionsCount}
						IGNITOR_RESOURCE
						{{
							name = ElectricCharge
							amount = 1
						}}
					}}
				";
			}
		}

		public string GetModuleEngineConfigs {
			get {
				string output = "";

				if (!IsMultimode) {
					output = $@"
						MODULE
						{{
							name = ModuleEngineConfigs
							configuration = {EngineID}
							modded = false
							origMass = {Mass.Str ()}
							
							{GetEngineConfig}

						}}
					";
				}

				return output;
			}
		}

		/// <summary>
		/// The CONFIG in ModuleEngineConfigs
		/// </summary>
		public string GetAsEngineConfig {
			get {
				return $@"
				
				";
			}
		}

		/// <summary>
		/// Returns the model rescaleFactor with current model and height
		/// </summary>
		public double HeightScale => Height / GetModelInfo.OriginalHeight;

		/// <summary>
		/// Returns the model x,z scales with current model, width and height
		/// </summary>
		public double WidthScale => Width / HeightScale / GetOriginalWidth;

		/// <summary>
		/// Returns the thrustCurve and thrustResource
		/// </summary>
		public string ThrustCurveConfig {
			get {
				string output = "";
				
				if (ThrustCurve.Count <= 0) {

				} else {
					string keys = "";

					List<DoubleTuple> tupleList = ThrustCurve;

					tupleList.Sort (delegate (DoubleTuple a, DoubleTuple b) {
						//Will sort descending
						if (a.Item1 > b.Item1) {
							return -1;
						} else if (a.Item1 < b.Item1) {
							return 1;
						} else {
							return 0;
						}
					});

					double lastTangent = 0;
					tupleList.Add (new DoubleTuple (Double.MinValue, tupleList.Last ().Item2));
					double newTangent = 0;

					for (int i = 0; i < tupleList.Count - 1; ++i) {
						newTangent = (tupleList[i + 1].Item2 - tupleList[i].Item2) / (tupleList[i + 1].Item1 - tupleList[i].Item1);
						keys += $@"
							key = {(tupleList[i].Item1 / 100).Str (4)} {(tupleList[i].Item2 / 100).Str (4)} {newTangent.Str ()} {lastTangent.Str ()}
						";
						lastTangent = newTangent;
					}

					tupleList.RemoveAt (tupleList.Count - 1);

					output += $@"
						curveResource = {FuelTypeList.GetFuelName (PropellantRatio[0].Propellant)}
						thrustCurve
						{{
							{keys}
						}}
					";
				}

				return output;
			}
		}

		/// <summary>
		/// Does the engine have custom thrustCurve?
		/// </summary>
		public string UsesThrustCurve => (ThrustCurve.Count > 0 ? "True" : "False");

		/// <summary>
		/// Returns tank config if the engine has tanks
		/// </summary>
		public string TankConfig {
			get {
				string output = "";

				if (UseTanks) {

					double volume = 0;
					string contents = "";

					foreach (FuelRatioElement i in GetConstrainedTankContents) {
						volume += i.Ratio;
						contents += $@"
							TANK
							{{
								name = {FuelTypeList.GetFuelName (i.Propellant)}
								amount = {(i.Ratio * FuelTypeList.GetFuelUtilisation (i.Propellant)).Str ()}
								maxAmount = {(i.Ratio * FuelTypeList.GetFuelUtilisation (i.Propellant)).Str ()}
							}}
						";
					}

					output = $@"
						MODULE
						{{
							name = ModuleFuelTanks
							basemass = -1
							type = All
							volume = {(LimitTanks ? TanksVolume.Str () : volume.Str ())}

							{contents}

						}}
					";
				}

				return output;
			}
		}

		/// <summary>
		/// Returns the config for engine's model and nodes
		/// </summary>
		public string ModelConfig {
			get {
				string model = "";

				ModelInfo modelInfo = GetModelInfo;

				double heightScale = HeightScale;
				double widthScale = WidthScale;

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

					{AttachmentNode}

				";

				return model;
			}
		}

		/// <summary>
		/// Returns the config of the plume
		/// </summary>
		public string PlumeConfig {
			get {
				string plume = "";

				PlumeInfo plumeInfo = GetPlumeInfo;
				ModelInfo modelInfo = GetModelInfo;

				string targetID = ((!IsNotSlave ? MasterEngineID : EngineID));

				plume = $@"
					@PART[{targetID}]:FOR[RealPlume]:HAS[!PLUME[{plumeInfo.PlumeID}]]:NEEDS[SmokeScreen]
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
					}}
				";


				return plume;
			}
		}

		/// <summary>
		/// Returns the configs that hide all GameObjects with names set in ModelInfo's HiddenMuObjects array
		/// </summary>
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

		/// <summary>
		/// Does the engine need ullage?
		/// </summary>
		public string UllageNeeded => ((NeedsUllage && EngineVariant != EngineType.Solid) ? "True" : "False");

		/// <summary>
		/// The engine part's name
		/// </summary>
		public string EngineID => $"GE-{Name}";
		// This is now validated on input

		/// <summary>
		/// Returns the attachment node config
		/// </summary>
		public string AttachmentNode {
			get {
				ModelInfo modelInfo = GetModelInfo;
				if (modelInfo.RadialAttachment) {
					return $"node_attach = {(modelInfo.RadialAttachmentPoint * WidthScale).Str ()}, 0.0, 0.0, 1.0, 0.0, 0.0";
				} else {
					return $"node_attach = 0.0, {modelInfo.NodeStackTop.Str ()}, 0.0, 0.0, 1.0, 0.0";
				}
			}
		}

		/// <summary>
		/// Returns the propellant config with correct KSP ratios
		/// </summary>
		public string PropellantConfig {
			get {
				bool isElectric = false;
				List<FuelRatioElement> fuelRatios = new List<FuelRatioElement> ();
				if (!FuelVolumeRatios) {

					foreach (FuelRatioElement i in PropellantRatio) {
						if (i.Propellant == FuelType.ElectricCharge) {
							isElectric = true;
						} else {
							fuelRatios.Add (new FuelRatioElement (i.Propellant, i.Ratio / FuelTypeList.GetFuelDensity (i.Propellant) / 1000));
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
						averageDensity += i.Ratio * FuelTypeList.GetFuelDensity (i.Propellant);
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
							name = {FuelTypeList.GetFuelName (i.Propellant)}
							ratio = {i.Ratio.Str ()}
							DrawGauge = {firstPropellant}
						}}
					";

					firstPropellant = false;
				}

				return propellants;
			}
		}

		/// <summary>
		/// Returns the gimbal config
		/// </summary>
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

		/// <summary>
		/// Returns the Test Flight config
		/// </summary>
		public string TestFlightConfig {
			get {
				string testflight = "";

				if (EnableTestFlight && !IsMultimode) {
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

		/// <summary>
		/// Returns the alternator config
		/// </summary>
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

		/// <summary>
		/// Returns the minimum thrust multiplier (0.0 - 1.0)
		/// </summary>
		public double MinThrustPercent {
			get {
				return (Math.Max (Math.Min (MinThrust, 100), 0) / 100);
			}
		}

		/// <summary>
		/// Returns model's original bell width or base width, depending on UseBaseWidth
		/// </summary>
		public double GetOriginalWidth => (UseBaseWidth ? GetModelInfo.OriginalBaseWidth : GetModelInfo.OriginalWidth);

		/// <summary>
		/// Returns engine's name shown in game
		/// </summary>
		public string RealEngineName => (EngineName == "" ? Name : EngineName);

		/// <summary>
		/// Returns 0 or 1 depending on whether you can attach parts to the engine
		/// </summary>
		public string CanAttachToEngine => (GetModelInfo.CanAttachOnModel ? "1" : "0");

		/// <summary>
		/// Returns the staging icon's name depending on engine's EngineVariant
		/// </summary>
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

		/// <summary>
		/// Returns EngineType used by ModuleEngines module
		/// </summary>
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

		/// <summary>
		/// Returns whether the engine can be shut down
		/// </summary>
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

		/// <summary>
		/// Returns whether the engine should use throttle response time
		/// </summary>
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

		/// <summary>
		/// Returns whether engine can be throttled in flight
		/// </summary>
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

		/// <summary>
		/// Returns the ignition count. 0 for infinite.
		/// </summary>
		public int IgnitionsCount {
			get {
				return Ignitions < 0 ? 0 : Ignitions;
			}
		}

		/// <summary>
		/// Returns this engine's ModelInfo
		/// </summary>
		public ModelInfo GetModelInfo {
			get {
				return ModelList.Get (ModelID);
			}
		}

		/// <summary>
		/// Returns this engine's PlumeInfo
		/// </summary>
		public PlumeInfo GetPlumeInfo {
			get {
				return PlumeList.Get (PlumeID);
			}
		}

		#endregion

		#region SerializerProperties

		/// <summary>
		/// Returns whether any of the Test Flight related properties were changed
		/// </summary>
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

		/// <summary>
		/// Returns whether any of the Advanced gimbal related properties were changed
		/// </summary>
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

		/// <summary>
		/// Returns whether the Manufacturer was changed
		/// </summary>
		public bool ManufacturerNotDefault {
			get {
				Engine comparison = new Engine ();
				return EngineManufacturer != comparison.EngineManufacturer;
			}
		}

		/// <summary>
		/// Returns whether the Description was changed
		/// </summary>
		public bool DescriptionNotDefault {
			get {
				Engine comparison = new Engine ();
				return EngineDescription != comparison.EngineDescription;
			}
		}

		#endregion

		#region UILabelsProperties

		/// <summary>
		/// Returns the estimated volume label
		/// </summary>
		public string TankVolumeEstimateLabel {
			get {
				return $"Estimated volume: {ModelTankVolume.Str (3)}L";
			}
		}

		/// <summary>
		/// Returns the thrust curve label
		/// </summary>
		public string ThrustCurveLabel {
			get {
				if (ThrustCurve.Count <= 0) {
					return "Default";
				} else {
					return "Custom";
				}
			}
		}

		/// <summary>
		/// Returns the Propellant ratios label
		/// </summary>
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
							output += $"{FuelTypeList.GetFuelName (i.Propellant)}:";
						}
					}

					output = output.Trim (':');
				}

				output += electricSuffix;

				return output;
			}
		}

		public string NameLabel => IsNotSlave ? RealEngineName : EngineDescription;

		/// <summary>
		/// Returns the tank label
		/// </summary>
		public string TankLabel {
			get {
				if (UseTanks) {
					if (LimitTanks) {
						if (TanksVolume == 0) {
							return "Enabled, but empty";
						} else {
							double volume = 0;
							foreach (FuelRatioElement i in GetConstrainedTankContents) {
								volume += i.Ratio;
							}
							return $"Enabled, {volume.Str (3)}L/{TanksVolume.Str (3)}L";
						}
					} else {
						if (TanksContents.Count == 0) {
							return "Enabled, but empty";
						} else {
							double volume = 0;
							foreach (FuelRatioElement i in GetConstrainedTankContents) {
								volume += i.Ratio;
							}
							return $"Enabled, {volume.Str (3)}L";
						}
					}
				} else {
					return "Disabled";
				}
			}
		}

		/// <summary>
		/// Returns the Polymorphism status label
		/// </summary>
		public string PolyLabel {
			get {
				switch (PolyType) {
					case Polymorphism.Single:
					return "Single";
					case Polymorphism.MultiModeMaster:
					return "Multimode Master";
					case Polymorphism.MultiModeSlave:
					return $"Multimode slave to {MasterEngineName}";
					case Polymorphism.MultiConfigMaster:
					return "Multiconfig Master";
					case Polymorphism.MultiConfigSlave:
					return $"Multiconfig slave to {MasterEngineName}";
					default:
					return "Something went wrong";
				}
			}
		}

		/// <summary>
		/// Returns the mass label
		/// </summary>
		public string MassLabel {
			get {
				if (Settings.GetBool (Setting.MoreEngineInfo)) {
					if (UseTanks && GetConstrainedTankContents.Count > 0) {
						double fm = FullTanksMass;
						return $"{Mass.Str (6)}t (Full: {fm.Str (6)}t) (Full tanks TWR: {(Thrust / 9.80665 / FullTanksMass).Str (3)})";
					} else {
						return $"{Mass.Str (6)}t (TWR: {(Thrust / 9.80665 / Mass).Str (3)})";
					}
				} else {
					if (TanksContents.Count > 0) {
						return $"{Mass.Str (6)}t (Full: {FullTanksMass.Str (6)}t)";
					} else {
						return $"{Mass.Str (6)}t";
					}
				}
			}
		}

		/// <summary>
		/// Returns the mass of engine with full tanks
		/// </summary>
		public double FullTanksMass {
			get {
				double output = 0.0;

				output += Mass;

				foreach (FuelRatioElement i in GetConstrainedTankContents) {
					output += FuelTypeList.GetFuelDensity (i.Propellant) * i.Ratio; // t/l * l = t
				}

				return output;
			}
		}

		/// <summary>
		/// Returns the label with amount of ignitions
		/// </summary>
		public string IgnitionsLabel {
			get {
				return Ignitions <= 0 ? "Infinite" : Ignitions.ToString ();
			}
		}

		/// <summary>
		/// Returns the label with engine's thrust
		/// </summary>
		public string ThrustLabel {
			get {
				if (Settings.GetBool (Setting.MoreEngineInfo)) {
					return $"{Thrust.Str ()}kN (SL: {(Thrust * AtmIsp / VacIsp).Str (3)}kN)";
				} else {
					return $"{Thrust.Str ()}kN";
				}
			}
		}

		/// <summary>
		/// Returns the label with engine's minimum thrust
		/// </summary>
		public string MinimumThrustLabel {
			get {
				if (Settings.GetBool (Setting.MoreEngineInfo)) {
					return $"{MinThrust}% (Vac: {(Thrust * MinThrust / 100).Str (3)}kN)";
				} else {
					return $"{MinThrust}%";
				}
			}
		}

		/// <summary>
		/// Returns the label with model's name and plume's name
		/// </summary>
		public string VisualLabel {
			get {
				if (IsNotSlave) {
					return $"{ModelList.GetName (ModelID)}, {PlumeList.GetName (PlumeID)}";
				} else {
					return $"{PlumeList.GetName (PlumeID)}";
				}
			}
		}

		/// <summary>
		/// Returns the label with engine's gimbal range
		/// </summary>
		public string GimbalLabel {
			get {
				if (AdvancedGimbal) {
					return $"X:{GimbalNX.Str ()}°:{GimbalPX.Str ()}°, Y:{GimbalNY.Str ()}°:{GimbalPY.Str ()}°";
				} else {
					return $"{Gimbal.Str ()}°";
				}
			}
		}

		/// <summary>
		/// Returns the label with engine's ingition success chance and MTBF
		/// </summary>
		public string TestFlightLabel {
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

		/// <summary>
		/// Returns the label with engine's width and height
		/// </summary>
		public string DimensionsLabel {
			get {
				return $"{Width.Str ()}m x {Height.Str ()}m";
			}
		}

		/// <summary>
		/// Returns the label with engine's R&D unlock node's name
		/// </summary>
		public string TechNodeLabel => (TechNodeList.GetName (TechUnlockNode));

		/// <summary>
		/// Returns a dictionary with TechNode as a key, and their names as values.
		/// Used to show proper names in ComboBox
		/// </summary>
		public Dictionary<TechNode, string> TechNodesWithLabels => TechNodeEnumWrapper.Get;

		/// <summary>
		/// Returns ListCollectionView with values needed to show model properly.
		/// Used to show proper names in ComboBox, and add image prewiew
		/// </summary>
		public ListCollectionView ModelsWithLabels => ModelEnumWrapper.Get;

		/// <summary>
		/// Returns a dictionary with Polymorphism type as a key, and their names as values.
		/// Used to show proper names in ComboBox
		/// </summary>
		public Dictionary<Polymorphism, string> PolyTypesWithLabels => PolymorphismEnumWrapper.Get;

		/// <summary>
		/// Returns the path to the preview image of this engine's model
		/// </summary>
		public string CurrentModelPreviewImagePath => ModelList.GetPreviewImagePath (ModelID);

		#endregion

		#region OtherProperties

		/// <summary>
		/// Is the engine multimode?
		/// </summary>
		public bool IsMultimode {
			get {
				return
					PolyType == Polymorphism.MultiModeMaster ||
					PolyType == Polymorphism.MultiModeSlave
				;
			}
		}

		/// <summary>
		/// Is the engine not a slave?
		/// </summary>
		public bool IsNotSlave {
			get {
				return !(
					PolyType == Polymorphism.MultiModeSlave ||
					PolyType == Polymorphism.MultiConfigSlave
				);
			}
		}

		/// <summary>
		/// Validates ID input if Setting is set
		/// </summary>
		public string EngineIDInterface {
			get {
				return Name;
			} set {
				string input = "";

				if (Settings.GetBool (Setting.ValidateIDOnInput)) {
					foreach (char i in value) {
						if (Regex.IsMatch (i.ToString (), "[a-zA-Z0-9-]{1}")) {
							input += i;
						}
					}
				} else {
					input = value;
				}

				input = input.Replace (' ', '-');

				if (input == "") {
					input = "EnterCorrectID";
				}

				Name = input;
			}
		}

		/// <summary>
		/// Returns actual tank contents. Limits them to TanksVolume, if LimitTank is true
		/// </summary>
		public List<FuelRatioElement> GetConstrainedTankContents {
			get {
				List<FuelRatioElement> output = new List<FuelRatioElement> ();

				double usedVolume = 0.0;
				foreach (FuelRatioElement i in TanksContents) {
					
					double volume = Math.Min (i.Ratio / FuelTypeList.GetFuelUtilisation (i.Propellant), (LimitTanks ? TanksVolume - usedVolume : double.MaxValue));
					usedVolume += volume;

					output.Add (new FuelRatioElement (i.Propellant, volume));

					if (LimitTanks && usedVolume == TanksVolume) {
						break;
					}
				}

				return output;
			}
		}

		/// <summary>
		/// Returns engine's base width, regardless of UseBaseWidth
		/// </summary>
		public double BaseWidth {
			get {
				if (UseBaseWidth) {
					return Width;
				} else {
					ModelInfo modelInfo = GetModelInfo;
					return Width * modelInfo.OriginalBaseWidth / modelInfo.OriginalWidth;
				}
			}
		}

		/// <summary>
		/// Returns estimated tank volume in L
		/// </summary>
		public double ModelTankVolume {
			get {
				double output = 0;

				ModelInfo modelInfo = GetModelInfo;

				if (modelInfo.TankOnModel) {
					output = modelInfo.OriginalTankVolume;

					output *= BaseWidth / modelInfo.OriginalBaseWidth;
					output *= BaseWidth / modelInfo.OriginalBaseWidth;
					output *= Height / modelInfo.OriginalHeight;
				}

				return output;
			}
		}

		#endregion

		/// <summary>
		/// The constructor with default engine
		/// </summary> 1000
		public Engine () {
			UID = UIDc++;
			
			Active = false;
			Name = "New-Engine";
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
			UseBaseWidth = true;
			EngineVariant = EngineType.Liquid;
			TanksVolume = 0.0;
			TanksContents = new List<FuelRatioElement> (0);
			ThrustCurve = new List<DoubleTuple> (0);
			UseTanks = false;
			LimitTanks = true;
			PolyType = Polymorphism.Single;
			MasterEngineName = "";
		}

		/// <summary>
		/// Has to be public to implement INotifyPropertyChanged. Don't use directly
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Update the property in UI
		/// </summary>
		/// <param name="name">The property to be updated</param>
		public void NotifyPropertyChanged (string name) {
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (name));
		}

		/// <summary>
		/// Updates EVERY property of this engine.
		/// </summary>
		public void NotifyEveryProperty () {
			foreach (PropertyInfo i in typeof (Engine).GetProperties ()) {
				NotifyPropertyChanged (i.Name);
			}
		}
		
		/// <summary>
		/// Same as 'new Engine ()'. Returns default engine
		/// </summary>
		/// <returns></returns>
		public static Engine New () {
			return new Engine ();
		}
	}
}