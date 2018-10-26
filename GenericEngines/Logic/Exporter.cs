using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GenericEngines {
	/// <summary>
	/// The class responsible for turning Engine objects into KSP configs
	/// </summary>
	public static class Exporter {
		/// <summary>
		/// Returns the compacted configs of all active engines in the list
		/// </summary>
		/// <param name="engines">The engine list to be exported</param>
		/// <param name="activeEngineCount">How many of the engines in the list were active</param>
		/// <returns></returns>
		public static string ConvertEngineListToConfig (List<Engine> engines, out int activeEngineCount) {
			string output = "";

			activeEngineCount = 0;
			foreach (Engine i in engines) {
				if (i.Active) {
					output += ConvertEngineToConfig (i);
					++activeEngineCount;
				}
			}

			return output;
		}

		/// <summary>
		/// Returns the compacted config of an engine
		/// </summary>
		/// <param name="engine">The engine to be exported</param>
		/// <returns></returns>
		public static string ConvertEngineToConfig (Engine engine) {
			string output = "";

			switch (engine.PolyType) {
				case Polymorphism.Single:
				case Polymorphism.MultiConfigMaster:
				case Polymorphism.MultiModeMaster:
				output = RegularEngineConfig (engine);
				break;
				case Polymorphism.MultiModeSlave:
				output = MultiModeSlaveConfig (engine);
				break;
				case Polymorphism.MultiConfigSlave:
				output = MultiConfigSlaveConfig (engine);
				break;
			}
			
			output = output.Compact ();
			return output;
		}

		private static string RegularEngineConfig (Engine engine) {
			return $@"
				PART
				{{
					name = {engine.EngineID}
					module = Part
					author = Generic Engines
					
					{engine.ModelConfig}

					TechRequired = {TechNodeList.GetID (engine.TechUnlockNode)}
					entryCost = {engine.EntryCost}
					cost = {engine.Cost}
					category = Engine
					subcategory = 0
					title = {engine.RealEngineName}
					manufacturer = {engine.EngineManufacturer}
					description = {engine.EngineDescription}
					attachRules = 1,1,1,{engine.CanAttachToEngine},0
					mass = {engine.Mass.Str ()}
					heatConductivity = 0.06
					skinInternalConductionMult = 4.0
					emissiveConstant = 0.8
					dragModelType = default
					maximum_drag = 0.2
					minimum_drag = 0.2
					angularDrag = 2
					crashTolerance = 12
					maxTemp = 2200 // = 3600
					bulkheadProfiles = size1
					tags = REP

					MODULE
					{{
						name = GenericEnginesPlumeScaleFixer
					}}

					{engine.HiddenObjectsConfig}
		
					MODULE
					{{
						name = ModuleEngines
						thrustVectorTransformName = thrustTransform
						exhaustDamage = True
						allowShutdown = {engine.AllowShutdown}
						useEngineResponseTime = {engine.UseEngineResponseTime}
						throttleLocked = {engine.LockThrottle}
						ignitionThreshold = 0.1
						minThrust = 0
						maxThrust = 610
						heatProduction = 200
						EngineType = {engine.EngineTypeConfig}
						useThrustCurve = {engine.UsesThrustCurve}
						exhaustDamageDistanceOffset = 0.79
		
						atmosphereCurve
						{{
							key = 0 345
							key = 1 204
							key = 6 0.001
						}}
						
						{engine.ThrustCurveConfig}
						
					}}

					{engine.GimbalConfig}

					{engine.AlternatorConfig}
	
					MODULE
					{{
						name = ModuleSurfaceFX
						thrustProviderModuleIndex = 0
						fxMax = 0.5
						maxDistance = 30
						falloff = 1.7
						thrustTransformName = thrustTransform
					}}
				}}

				@PART[{engine.EngineID}]:FOR[RealismOverhaul]
				{{
					%RSSROConfig = True
					%RP0conf = True
					
					%breakingForce = 250
					%breakingTorque = 250
					@maxTemp = 573.15
					%skinMaxTemp = 673.15
					%stageOffset = 1
					%childStageOffset = 1
					%stagingIcon = {engine.StagingIcon}
					@bulkheadProfiles = srf, size3
					@tags = Generic Engine
					{/*%engineType = {engine.EngineID}*/""}

					{engine.TankConfig}

					@MODULE[ModuleEngines*]
					{{
						%engineID = PrimaryMode
						@minThrust = {(engine.Thrust * engine.MinThrustPercent).Str ()}
						@maxThrust = {engine.Thrust.Str ()}
						@heatProduction = 180
						@useThrustCurve = {engine.UsesThrustCurve}
						%powerEffectName = {engine.GetPlumeInfo.PlumeID}

						{engine.PropellantConfig}

						@atmosphereCurve
						{{
							@key,0 = 0 {engine.VacIsp.Str ()}
							@key,1 = 1 {engine.AtmIsp.Str ()}
						}}

						{engine.ThrustCurveConfig}

					}}

					{engine.GetModuleEngineConfigs}

					!RESOURCE,*{{}}
				}}

				{engine.PlumeConfig}

				{engine.TestFlightConfig}
			";
		}

		private static string MultiModeSlaveConfig (Engine engine) {
			return $@"
				@PART[{engine.MasterEngineID}]
				{{
					MODULE
					{{
						name = MultiModeEngine
						primaryEngineID = PrimaryMode
						primaryEngineModeDisplayName = Primary mode ({engine.MasterEngineID})
						secondaryEngineID = SecondaryMode
						secondaryEngineModeDisplayName = Secondary mode ({engine.EngineID})
					}}
				}}
				
				@PART[{engine.MasterEngineID}]:FOR[RealismOverhaul]
				{{
					+MODULE[ModuleEngines*]
					{{
						@engineID = SecondaryMode
						@minThrust = {(engine.Thrust * engine.MinThrustPercent).Str ()}
						@maxThrust = {engine.Thrust.Str ()}
						@heatProduction = 180
						@useThrustCurve = {engine.UsesThrustCurve}
						%powerEffectName = {engine.GetPlumeInfo.PlumeID}

						!PROPELLANT,*
						{{
						}}

						{engine.PropellantConfig}

						@atmosphereCurve
						{{
							@key,0 = 0 {engine.VacIsp.Str ()}
							@key,1 = 1 {engine.AtmIsp.Str ()}
						}}

						{engine.ThrustCurveConfig}

					}}
				}}

				{engine.PlumeConfig}
			";
		}

		private static string MultiConfigSlaveConfig (Engine engine) {
			return $@"
				@PART[{engine.MasterEngineID}]:FOR[RealismOverhaul]
				{{
					@MODULE[ModuleEngineConfigs]
					{{
						{engine.GetEngineConfig}
					}}
				}}

				{engine.PlumeConfig}

				{engine.TestFlightConfig}

				{engine.EngineConfigEntryCostConfig}

			";
		}
	}
}
