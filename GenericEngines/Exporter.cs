using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GenericEngines {
	public static class Exporter {

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

		public static string ConvertEngineToConfig (Engine engine) {
			string output = $@"
				PART
				{{
					name = {engine.EngineID}
					module = Part
					author = Generic Engines
					
					{engine.ModelConfig}

					TechRequired = {TechNodes.GetID (engine.TechUnlockNode)}
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
						fxOffset = 0, 0, 0.974338
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
					%engineType = {engine.EngineID}

					{engine.TankConfig}

					@MODULE[ModuleEngines*]
					{{
						@minThrust = {(engine.Thrust * engine.MinThrustPercent).Str ()}
						@maxThrust = {engine.Thrust.Str ()}
						@heatProduction = 180
						@useThrustCurve = {engine.UsesThrustCurve}

						{engine.PropellantConfig}

						@atmosphereCurve
						{{
							@key,0 = 0 {engine.VacIsp.Str ()}
							@key,1 = 1 {engine.AtmIsp.Str ()}
						}}

						{engine.ThrustCurveConfig}

					}}

					MODULE
					{{
						name = ModuleEngineConfigs
						configuration = {engine.EngineID}
						modded = false
						origMass = {engine.Mass.Str ()}
						CONFIG
						{{
							name = {engine.EngineID}
							description = Generic Engine | {engine.Name}
							maxThrust = {engine.Thrust.Str ()}
							minThrust = {(engine.Thrust * engine.MinThrustPercent).Str ()}
							heatProduction = 100
							massMult = 1

							{engine.PropellantConfig}

							atmosphereCurve
							{{
								key = 0 {engine.VacIsp.Str ()}
								key = 1 {engine.AtmIsp.Str ()}
							}}

							{engine.ThrustCurveConfig}

							ullage = {engine.NeedsUllage}
							pressureFed = {engine.PressureFed}
							ignitions = {engine.IgnitionsCount}
							IGNITOR_RESOURCE
							{{
								name = ElectricCharge
								amount = 1
							}}
						}}
					}}

					!RESOURCE,*{{}}
				}}

				{engine.PlumeConfig}

				{engine.TestFlightConfig}
			";
			//====

			output = output.Compact ();
			return output;
		}

	}
}
