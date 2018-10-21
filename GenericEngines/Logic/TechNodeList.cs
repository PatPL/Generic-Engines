﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	/// <summary>
	/// Class storing all TechNode info
	/// </summary>
	public static class TechNodeList {
		/// <summary>
		/// Returns the ID of given TechNode
		/// </summary>
		/// <param name="tn">The TechNode</param>
		/// <returns></returns>
		public static string GetID (TechNode tn) {
			return tn.ToString ();
		}

		/// <summary>
		/// Returns the name of given TechNode
		/// </summary>
		/// <param name="tn">The TechNode</param>
		/// <returns></returns>
		public static string GetName (TechNode tn) {
			return names[(int) tn];
		}

		private static readonly string[] names = new string[] {
			@"Start",
			@"Supersonic Plane Development",
			@"Supersonic Flight",
			@"Mature Supersonic Flight",
			@"High Speed Flight",
			@"Advanced Jet Engines",
			@"Mature Turbofans",
			@"Refined Turbofans",
			@"Scramjet Engines",
			@"Experimental Aircraft Engines",
			@"2051-2099 Regular Flight",
			@"2100-2149 Regular Flight",
			@"2150+ Regular Flight",
			@"Hypersonic Flight",
			@"Prototype Spaceplaces",
			@"Effective Spaceplanes",
			@"Space Shuttles",
			@"Improved Spaceplanes",
			@"Advanced Spaceplanes",
			@"High-Tech Spaceplanes",
			@"Experimental Spaceplanes",
			@"SSTO Spaceplanes",
			@"2100-2149 Spaceplanes",
			@"2150+ Spaceplanes",
			@"Basic Capsules",
			@"Second Generation Capsules",
			@"Mature Capsules",
			@"Improved Capsules",
			@"Advanced Capsules",
			@"Modern Capsules",
			@"Near Future Capsules",
			@"High-Tech Capsules",
			@"2100-2149 Command Modules",
			@"2150+ Command Modules",
			@"Space Station Prototypes",
			@"Space Station Development",
			@"Early Space Stations",
			@"Modular Space Stations",
			@"Large Scale Orbital Construction",
			@"Improved Orbital Construction",
			@"Inflatable Habitats",
			@"Improved Habitats",
			@"Advanced Habitats",
			@"Large Scale Habitats",
			@"2100-2149 Space Stations",
			@"2150+ Space Stations",
			@"Early Flight Control",
			@"Stability",
			@"Early Docking Procedures",
			@"Improved Flight Control",
			@"Advanced Flight Control",
			@"Docking and Crew Transfer",
			@"Space Station Attitude Control",
			@"Large Spaceplane Control",
			@"Standardized Docking Ports",
			@"Large Station Attitude Control",
			@"Large Docking Ports",
			@"Grid Fins",
			@"Near Future Flight Control",
			@"2051-2099 Control",
			@"2100-2149 Control",
			@"2150+ Control",
			@"Entry, Descent and Landing",
			@"Human Rated EDL",
			@"Early Landing",
			@"Lunar Rated Heatshields",
			@"Lunar Landing",
			@"Improved Landing Engines",
			@"Advanced Uncrewed Landing",
			@"Interplanetary Rovers",
			@"Large Rover Designs",
			@"Reusability",
			@"Advanced Landing",
			@"Supersonic Inflatable Aerodynamic Decelerator",
			@"Hypersonic Inflatable Aerodynamic Decelerator",
			@"2051-2099 EDL",
			@"2100-2149 EDL",
			@"2150+ EDL",
			@"Prototype Hydrolox Engines",
			@"Early Hydrolox Engines",
			@"Improved Hydrolox Engines",
			@"Large Hydrolox Engines",
			@"1968 Hydrolox Engines",
			@"1972-1975 Hydrolox Engines",
			@"1976-1980 Hydrolox Engines",
			@"1981-1985 Hydrolox Engines",
			@"1986-1991 Hydrolox Engines",
			@"1992-1997 Hydrolox Engines",
			@"1998-2008 Hydrolox Engines",
			@"2009-2018 Hydrolox Engines",
			@"Near Future Hydrolox Engines",
			@"2051-2099 Hydrolox Engines",
			@"2100-2149  Hydrolox Engines",
			@"2150+  Hydrolox Engines",
			@"Post-War Rocketry Testing",
			@"Early Rocketry",
			@"Basic Rocketry",
			@"1956-1957 Orbital Rocketry",
			@"1958 Orbital Rocketry",
			@"1959 Orbital Rocketry",
			@"1960 Orbital Rocketry",
			@"1961 Orbital Rocketry",
			@"1962 Orbital Rocketry",
			@"1963 Orbital Rocketry",
			@"1964 Orbital Rocketry",
			@"1965 Orbital Rocketry",
			@"1966 Orbital Rocketry",
			@"1967-1968 Orbital Rocketry",
			@"1970-1971 Orbital Rocketry",
			@"1972-1975 Orbital Rocketry",
			@"1976-1980 Orbital Rocketry",
			@"1981-1985 Orbital Rocketry",
			@"1986-1991 Orbital Rocketry",
			@"1992-1997 Orbital Rocketry",
			@"1998-2003 Orbital Rocketry",
			@"2004-2008 Orbital Rocketry",
			@"2009-2013 Orbital Rocketry",
			@"2014-2018 Orbital Rocketry",
			@"Near Future Orbital Rocketry",
			@"2051-2099 Orbital Rocketry",
			@"2100-2149  Orbital Rocketry",
			@"2150+  Orbital Rocketry",
			@"First Staged Combustion Engines",
			@"1964 Staged Combustion Engines",
			@"1966 Staged Combustion Engines",
			@"1967-1968 Staged Combustion Engines",
			@"1969 Staged Combustion Engines",
			@"1970-1971 Staged Combustion Engines",
			@"1972-1980 Staged Combustion Engines",
			@"1981-1985 Staged Combustion Engines",
			@"1986-1991 Staged Combustion Engines",
			@"1992-1997 Staged Combustion Engines",
			@"1998-2003 Staged Combustion Engines",
			@"2004-2008 Staged Combustion Engines",
			@"2009-2013 Staged Combustion Engines",
			@"2014-2018 Staged Combustion Engines",
			@"Near Future Staged Combustion Engines",
			@"2051-2099 Staged Combustion",
			@"2100-2149  Staged Combustion",
			@"2150+  Staged Combustion",
			@"Early Solid Rocket Engines",
			@"1956-1957 Solid Rocket Engines",
			@"1958 Solid Rocket Engines",
			@"1959-1960 Solid Rocket Engines",
			@"1962-1963 Solid Rocket Engines",
			@"1964-1965 Solid Rocket Engines",
			@"1966 Solid Rocket Engines",
			@"1967-1968 Solid Rocket Engines",
			@"1969-1971 Solid Rocket Engines",
			@"1972-1975 Solid Rocket Engines",
			@"1976-1980 Solid Rocket Engines",
			@"1981-1985 Solid Rocket Engines",
			@"1986-1991 Solid Rocket Engines",
			@"1992-1997 Solid Rocket Engines",
			@"1998-2008 Solid Rocket Engines",
			@"2009-2018 Solid Rocket Engines",
			@"Near Future Solid Rocket Engines",
			@"2051-2099 Solids",
			@"2100-2149 Solids",
			@"2150+ Solids",
			@"Early Electric Propulsion",
			@"Basic Electric Propulsion",
			@"Improved Electric Propulsion",
			@"Advanced Electric Propulsion",
			@"2051-2099 Electric Propulsion",
			@"2100-2149 Electric Propulsion",
			@"2150+ Electric Propulsion",
			@"Prototype Nuclear Propulsion",
			@"Early Nuclear Propulsion",
			@"Basic Nuclear Propulsion",
			@"Improved Nuclear Propulsion",
			@"Advanced Nuclear Propulsion",
			@"Efficient Nuclear Propulsion",
			@"Near Future Nuclear Propulsion",
			@"Advanced Near Future Nuclear Propulsion",
			@"2051-2099 Nuclear Propulsion",
			@"2100-2149 Nuclear Propulsion",
			@"2150+ Nuclear Propulsion",
			@"Crew Survivability",
			@"Early Life Support and ISRU",
			@"Life Support and ISRU",
			@"Basic Life Support and ISRU",
			@"Improved Life Support and ISRU",
			@"Long-Life Support and ISRU",
			@"Long-Term Life Support and ISRU",
			@"Efficient Life Support and ISRU",
			@"Near Future Life Support and ISRU",
			@"2051-2099 Life Support and ISRU",
			@"2100-2149 Life Support and ISRU",
			@"2150+ Life Support and ISRU",
			@"Post-War Materials Science",
			@"Early Materials Science",
			@"Satellite Era Materials Science",
			@"Early Human Spaceflight Materials Science",
			@"Advanced Capsules Era Materials Science",
			@"Lunar Exploration Era Materials Science",
			@"Space Station Era Materials Science",
			@"Spaceplanes Era Materials Science",
			@"Long-Term Space Habitation Era Materials Science",
			@"International Cooperation Era Materials Science",
			@"Commercial Spaceflight Era Materials Science",
			@"Near Future Era Materials Science",
			@"Colonization Era Materials Science",
			@"Satellite Era Electronics Research",
			@"Early Human Spaceflight Electronics Research",
			@"Advanced Capsules Era Electronics Research",
			@"Lunar Exploration Era Electronics Research",
			@"Space Station Era Electronics Research",
			@"Spaceplanes Era Electronics Research",
			@"Long-Term Space Habitation Era Electronics Research",
			@"International Cooperation Era Electronics Research",
			@"Commercial Spaceflight Era Electronics Research",
			@"Near Future Era Electronics Research",
			@"Colonization Era Electronics Research",
			@"First RTG's",
			@"Early RTG's",
			@"Small Nuclear Fission Reactors",
			@"Improved RTG's",
			@"Multihundred-Watt RTG's",
			@"GPHS-RTG's",
			@"Improved Nuclear Power Generation",
			@"Advanced Nuclear Power Generation",
			@"Modern Nuclear Power Generation",
			@"Near Future Nuclear Power Generation",
			@"2051-2099 Nuclear Power",
			@"2100-2149 Nuclear Power",
			@"2150+ Nuclear Power",
			@"Primitive Solar Panels",
			@"Early Power Generation and Storage",
			@"Basic Power Generation and Storage",
			@"Improved Power Generation and Storage",
			@"Lunar Rated Power Generation",
			@"Space Station Solar Panels",
			@"Mature Power Generation and Storage",
			@"Large Scale Solar Arrays",
			@"Advanced Power Generation and Storage",
			@"Modern Power Generation and Storage",
			@"Near Future Power Generation and Storage",
			@"2051-2099 Power Generation and Storage",
			@"2100-2149 Power Generation and Storage",
			@"2150+ Power Generation and Storage",
			@"Lunar Range Communications",
			@"Interplanetary Communications",
			@"Improved Communications",
			@"Advanced Communications",
			@"Deep Space Communications",
			@"Large Scale Communications",
			@"Massive Scale Communications",
			@"Efficient Communications",
			@"Modern Communications",
			@"Near Future Communications",
			@"2051-2099 Communications",
			@"2100-2149 Communications",
			@"2150+ Communications",
			@"Post-War Avionics",
			@"Avionics Prototypes",
			@"Early Avionics and Probes",
			@"Basic Avionics and Probes",
			@"Interplanetary Probes",
			@"Improved Avionics",
			@"Mature Avionics and Probes",
			@"Large Scale Avionics",
			@"Advanced Avionics and Probes",
			@"Next Generation Avionics and Probes",
			@"Long-Term Space Habitation Era Avionics and Probes",
			@"International Era Avionics and Probes",
			@"Modern Avionics and Probes",
			@"Near Future Avionics and Probes",
			@"2051-2099 Avionics and Probes",
			@"2100-2149 Avionics and Probes",
			@"2150+ Avionics and Probes",
			@"Early Science",
			@"Satellite Era Science",
			@"Early Human Spaceflight Era Science",
			@"Interplanetary Era Science",
			@"Lunar Exploration Era Science",
			@"Surface Science",
			@"Deep Space Science Experiments",
			@"Exploration Era Science",
			@"Sample Return Science Experiments",
			@"Advanced Science Experiments",
			@"Advanced Surface Experiments",
			@"Near Future Science",
			@"2051-2099 Science",
			@"2100-2149 Science",
			@"2150+ Science"
		};
	}

	/// <summary>
	/// Enum with all RP-(0/1)? tech nodes.
	/// </summary>
	public enum TechNode {
		start,
		supersonicDev,
		supersonicFlightRP0,
		matureSupersonic,
		highSpeedFlight,
		advancedJetEngines,
		matureTurbofans,
		refinedTurbofans,
		scramjetEngines,
		experimentalAircraft,
		colonization2051Flight,
		colonization2100Flight,
		colonization2150Flight,
		hypersonicFlightRP0,
		prototypeSpaceplanes,
		effectiveSpaceplanes,
		spaceShuttles,
		improvedSpaceplanes,
		advancedSpaceplanes,
		highTechSpaceplanes,
		experimentalSpaceplanes,
		sstoSpaceplanes,
		colonization2100Spaceplanes,
		colonization2150Spaceplanes,
		basicCapsules,
		secondGenCapsules,
		matureCapsules,
		improvedCapsules,
		advancedCapsules,
		modernCapsules,
		capsulesNF,
		highTechCapsules,
		colonization2100Command,
		colonization2150Command,
		spaceStationPrototypes,
		spaceStationDev,
		earlySpaceStations,
		modularSpaceStations,
		largeScaleOrbitalCon,
		improvedOrbitalConstruction,
		inflatableHabitats,
		improvedHabitats,
		advancedHabitats,
		largeScaleHabitats,
		colonization2100SpaceStations,
		colonization2150SpaceStations,
		earlyFlightControl,
		stabilityRP0,
		earlyDocking,
		improvedFlightControl,
		advancedFlightControl,
		dockingCrewTransfer,
		spaceStationControl,
		largeSpaceplaneControl,
		standardDockingPorts,
		largeStationControl,
		largeDockingPorts,
		gridFins,
		flightControlNF,
		colonization2051Control,
		colonization2100Control,
		colonization2150Control,
		entryDescentLanding,
		humanRatedEDL,
		earlyLanding,
		lunarRatedHeatshields,
		lunarLanding,
		improvedLandingEngines,
		advancedUncrewedLanding,
		interplanetaryRovers,
		largeRoverDesigns,
		reusability,
		advancedLanding,
		SIAD,
		HIAD,
		colonization2051EDL,
		colonization2100EDL,
		colonization2150EDL,
		prototypeHydrolox,
		earlyHydrolox,
		improvedHydrolox,
		largeHydrolox,
		hydrolox1968,
		hydrolox1972,
		hydrolox1976,
		hydrolox1981,
		hydrolox1986,
		hydrolox1992,
		hydrolox1998,
		hydrolox2009,
		hydroloxNF,
		colonization2051Hydrolox,
		colonization2100Hydrolox,
		colonization2150Hydrolox,
		rocketryTesting,
		earlyRocketry,
		basicRocketryRP0,
		orbitalRocketry1956,
		orbitalRocketry1958,
		orbitalRocketry1959,
		orbitalRocketry1960,
		orbitalRocketry1961,
		orbitalRocketry1962,
		orbitalRocketry1963,
		orbitalRocketry1964,
		orbitalRocketry1965,
		orbitalRocketry1966,
		orbitalRocketry1967,
		orbitalRocketry1970,
		orbitalRocketry1972,
		orbitalRocketry1976,
		orbitalRocketry1981,
		orbitalRocketry1986,
		orbitalRocketry1992,
		orbitalRocketry1998,
		orbitalRocketry2004,
		orbitalRocketry2009,
		orbitalRocketry2014,
		orbitalRocketryNF,
		colonization2051Orbital,
		colonization2100Orbital,
		colonization2150Orbital,
		firstStagedCombustion,
		stagedCombustion1964,
		stagedCombustion1966,
		stagedCombustion1967,
		stagedCombustion1969,
		stagedCombustion1970,
		stagedCombustion1972,
		stagedCombustion1981,
		stagedCombustion1986,
		stagedCombustion1992,
		stagedCombustion1998,
		stagedCombustion2004,
		stagedCombustion2009,
		stagedCombustion2014,
		stagedCombustionNF,
		colonization2051Staged,
		colonization2100Staged,
		colonization2150Staged,
		earlySolids,
		solids1956,
		solids1958,
		solids1959,
		solids1962,
		solids1964,
		solids1966,
		solids1967,
		solids1969,
		solids1972,
		solids1976,
		solids1981,
		solids1986,
		solids1992,
		solids1998,
		solids2009,
		solidsNF,
		colonization2051Solid,
		colonization2100Solid,
		colonization2150Solid,
		earlyElecPropulsion,
		basicElecPropulsion,
		improvedElecPropulsion,
		advancedElecPropulsion,
		colonization2051ElecProp,
		colonization2100ElecProp,
		colonization2150ElecProp,
		prototypeNuclearPropulsion,
		earlyNuclearPropulsion,
		basicNuclearPropulsion,
		improvedNuclearPropulsion,
		advancedNuclearPropulsion,
		efficientNuclearPropulsion,
		nuclearPropulsionNF,
		nuclearPropulsionNF2,
		colonization2051NuclearProp,
		colonization2100NuclearProp,
		colonization2150NuclearProp,
		crewSurvivability,
		earlyLifeSupport,
		lifeSupportISRU,
		basicLifeSupport,
		improvedLifeSupport,
		longTermLifeSupport,
		advancedLifeSupport,
		efficientLifeSupport,
		lifeSupportNF,
		colonization2051LifeSupport,
		colonization2100LifeSupport,
		colonization2150LifeSupport,
		postWarMaterialsScience,
		earlyMaterialsScience,
		materialsScienceSatellite,
		materialsScienceHuman,
		materialsScienceAdvCapsules,
		materialsScienceLunar,
		materialsScienceSpaceStation,
		materialsScienceSpaceplanes,
		materialsScienceLongTerm,
		materialsScienceInternational,
		materialsScienceCommercial,
		materialsScienceNF,
		materialsScienceColonization,
		electronicsSatellite,
		electronicsHuman,
		electronicsAdvCapsules,
		electronicsLunar,
		electronicsSpaceStation,
		electronicsSpaceplanes,
		electronicsLongTerm,
		electronicsInternational,
		electronicsCommercial,
		electronicsNF,
		electronicsColonization,
		firstRTG,
		earlyRTG,
		nuclearFissionReactors,
		improvedRTG,
		multihundredWattRTG,
		gphsRTG,
		improvedNuclearPower,
		advancedNuclearPower,
		modernNuclearPower,
		nuclearPowerNF,
		colonization2051NuclearPower,
		colonization2100NuclearPower,
		colonization2150NuclearPower,
		primitiveSolarPanels,
		earlyPower,
		basicPower,
		improvedPower,
		lunarRatedPower,
		spaceStationSolarPanels,
		maturePower,
		largeScaleSolarArrays,
		advancedPower,
		modernPower,
		powerNF,
		colonization2051Power,
		colonization2100Power,
		colonization2150Power,
		lunarRangeComms,
		interplanetaryComms,
		improvedComms,
		advancedComms,
		deepSpaceComms,
		largeScaleComms,
		massiveScaleComms,
		efficientComms,
		modernComms,
		commsNF,
		colonization2051Comms,
		colonization2100Comms,
		colonization2150Comms,
		postWarAvionics,
		avionicsPrototypes,
		earlyAvionics,
		basicAvionics,
		interplanetaryProbes,
		improvedAvionics,
		matureAvionics,
		largeScaleAvionics,
		advancedAvionics,
		nextGenAvionics,
		longTermAvionics,
		internationalAvionics,
		modernAvionics,
		avionicsNF,
		colonization2051Avionics,
		colonization2100Avionics,
		colonization2150Avionics,
		earlyScience,
		scienceSatellite,
		scienceHuman,
		scienceAdvCapsules,
		scienceLunar,
		surfaceScience,
		deepSpaceScience,
		scienceExploration,
		sampleReturnScience,
		advancedScience,
		advancedSurfaceScience,
		scienceNF,
		colonization2051Science,
		colonization2100Science,
		colonization2150Science
	}
}
