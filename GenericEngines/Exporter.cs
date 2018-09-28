using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public static class Exporter {

		public static string ConvertEngineListToConfig (List<Engine> engines) {
			string output = "";

			foreach (Engine i in engines) {
				if (i.Active) {
					output += ConvertEngineToConfig (i);
				}
			}

			return output;
		}

		public static string ConvertEngineToConfig (Engine engine) {
			string output = "";
			string engineID = $"GE-{engine.Name.Replace (' ', '-')}";

			double heightScale = engine.Height / 1.9;
			double widthScale = engine.Width / heightScale;
			double minThrustPercent = Math.Max (Math.Min (engine.MinThrust, 100), 0) / 100;
			int ignitions = engine.Ignitions < 0 ? 0 : engine.Ignitions;

			string propellants = "";
			bool firstPropellant = true;
			foreach (FuelRatioElement i in engine.PropellantRatio) {
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

			//====
output = $@"
PART
{{
	name = {engineID}
	module = Part
	author = Generic Engines
	mesh = LR-91eng.mu
	rescaleFactor = 1
	
	MODEL
	{{
		model = RealismOverhaul/Models/LR-91eng
        scale = 1, 1, 1
	}}
	scale = 1

	node_stack_top = 0.0, 0.7215, 0.0, 0.0, 1.0, 0.0, 1
	node_stack_bottom = 0.0, -1.1635, 0.0, 0.0, -1.0, 0.0, 1
	node_attach = 0.0, 0.7215, 0.0, 0.0, 1.0, 0.0, 1

	TechRequired = basicRocketry
	entryCost = 0
	cost = {engine.Cost}
	category = Engine
	subcategory = 0
	title = NK43
	manufacturer = Generic inc.
	description = NK43 engine for 2nd stage moon rocket N1
	attachRules = 1,1,1,0,0
	mass = 2.375
	heatConductivity = 0.06
	skinInternalConductionMult = 4.0
	emissiveConstant = 0.8
	dragModelType = default
	maximum_drag = 0.2
	minimum_drag = 0.2
	angularDrag = 2
	crashTolerance = 7
	maxTemp = 2200 // = 3600
	bulkheadProfiles = size1
	tags = REP
	
	MODULE
	{{
		name = ModuleJettison
		jettisonName = 430
		bottomNodeName = bottom
		isFairing = True
		jettisonedObjectMass = 0.1
		jettisonForce = 5
		jettisonDirection = 0 0 1
	}}
		
	MODULE
	{{
		name = ModuleEngines
		thrustVectorTransformName = thrustTransform
		exhaustDamage = True
		ignitionThreshold = 0.1
		minThrust = 0
		maxThrust = 610
		heatProduction = 200
		fxOffset = 0, 0, 0.974338
		EngineType = LiquidFuel
		useThrustCurve = true 
		exhaustDamageDistanceOffset = 0.79
		
		atmosphereCurve
		{{
			key = 0 345
			key = 1 204
			key = 6 0.001
		}}
		thrustCurve
		{{
			key = 1 1
			key = 0 0
		}}
	}}
	MODULE
	{{
		name = FXModuleAnimateThrottle
		animationName = NK43
		responseSpeed = 0.0009
		dependOnEngineState = True
		dependOnThrottle = True
	}}

//MODULE
	{{
	    name = ModuleGimbal
	    gimbalTransformName = thrustTransform
		gimbalRangeYP = 0.7
		gimbalRangeYN = 0.7
		gimbalRangeXP = 0.7
		gimbalRangeXN = 0.7
 	    gimbalResponseSpeed = 15
 	    useGimbalResponseSpeed = true
    }}
	
	MODULE
	{{
		name = ModuleAlternator
		RESOURCE
		{{
			name = ElectricCharge
			rate = 2.0
		}}
	}}
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

@PART[{engineID}]:FOR[RealismOverhaul]
{{

	% RSSROConfig = True

    !mesh = NULL
	
	//  Default: 1.0
	//  Dimensions:
	//  Radius: 1.0m
	//  Height: 1.9m
	//
    @MODEL
    {{
        @scale = {widthScale.ToString (CultureInfo.InvariantCulture)}, 1.0, {widthScale.ToString (CultureInfo.InvariantCulture)}
    }}

    @scale = 1.0
    @rescaleFactor = {heightScale.ToString (CultureInfo.InvariantCulture)}

    @mass = {engine.Mass.ToString (CultureInfo.InvariantCulture)}
    @crashTolerance = 10
    %breakingForce = 250
    %breakingTorque = 250
    @maxTemp = 573.15
    %skinMaxTemp = 673.15
    %stageOffset = 1
    %childStageOffset = 1
    %stagingIcon = LIQUID_ENGINE
    @bulkheadProfiles = srf, size3
    @tags = Generic Engine

    %engineType = {engineID}

    @MODULE[ModuleEngines*]
    {{
        @minThrust = {(engine.Thrust * minThrustPercent).ToString (CultureInfo.InvariantCulture)}
        @maxThrust = {engine.Thrust.ToString (CultureInfo.InvariantCulture)}
        @heatProduction = 180
        @useThrustCurve = False

		{propellants}

        @atmosphereCurve
        {{
            @key,0 = 0 {engine.VacIsp.ToString (CultureInfo.InvariantCulture)}
            @key,1 = 1 {engine.AtmIsp.ToString (CultureInfo.InvariantCulture)}
        }}

        !thrustCurve,*{{}}
    }}

    MODULE
    {{
        name = ModuleGimbal
        gimbalTransformName = thrustTransform
        gimbalRange = {engine.Gimbal.ToString (CultureInfo.InvariantCulture)}
    }}
	
	%title = {engine.Name}
	%manufacturer = Generic Engines
	%description = Generic Engine | {engine.Name}

	MODULE
	{{
		name = ModuleEngineConfigs
		configuration = {engineID}
		modded = false
		origMass = {engine.Mass.ToString (CultureInfo.InvariantCulture)}
		CONFIG
		{{
			name = {engineID}
			description = Generic Engine | {engine.Name}
			maxThrust = {engine.Thrust.ToString (CultureInfo.InvariantCulture)}
			minThrust = {(engine.Thrust * minThrustPercent).ToString (CultureInfo.InvariantCulture)}
			heatProduction = 100
			massMult = 1

			{propellants}

			atmosphereCurve
			{{
				key = 0 {engine.VacIsp.ToString (CultureInfo.InvariantCulture)}
				key = 1 {engine.AtmIsp.ToString (CultureInfo.InvariantCulture)}
			}}

			ullage = {engine.NeedsUllage}
			pressureFed = {engine.PressureFed}
			ignitions = {ignitions}
			IGNITOR_RESOURCE
			{{
				name = ElectricCharge
				amount = 1
			}}
		}}
	}}

	@MODULE[ModuleGimbal]
	{{
		@gimbalRange = {engine.Gimbal.ToString (CultureInfo.InvariantCulture)}
		%useGimbalResponseSpeed = true
		%gimbalResponseSpeed = 50
	}}

	!MODULE[ModuleAlternator],*{{}}

	!RESOURCE,*{{}}
}}

@PART[{engineID}]:FOR[RealPlume]:NEEDS[SmokeScreen]
{{
	PLUME
    {{
        name = Kerolox-Upper
        transformName = thrustTransform
        localRotation = 0,0,0
        localPosition = 0,0,0.8
        fixedScale = {(0.4 * engine.Width).ToString (CultureInfo.InvariantCulture)}
        energy = 1
        speed = 1
    }}

    @MODULE[ModuleEngines*]
    {{
        %powerEffectName = Kerolox-Upper
        !fxOffset = NULL
    }}

	@MODULE[ModuleEngineConfigs]
	{{
        @CONFIG,*
		{{
			%powerEffectName = Kerolox-Upper
		}}
	}}
}}
";
			//====

			return output;
		}

	}
}
