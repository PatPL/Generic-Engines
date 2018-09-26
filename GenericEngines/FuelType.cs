﻿using System.ComponentModel;

namespace GenericEngines {
	public enum FuelType {
		ElectricCharge,
		LqdOxygen,
		Kerosene,
		LqdHydrogen,
		NTO,
		UDMH,
		Aerozine50,
		MMH,
		HTP,
		AvGas,
		IRFNA_III,
		NitrousOxide,
		Aniline,
		Ethanol75,
		Ethanol90,
		Ethanol,
		LqdAmmonia,
		LqdMethane,
		CIF3,
		CIF5,
		Diborane,
		Pentaborane,
		Ethane,
		Ethylene,
		OF2,
		LqdFluorine,
		N2F4,
		Methanol,
		Furfuryl,
		UH25,
		Tonka250,
		Tonka500,
		IWFNA,
		IRFNA_IV,
		AK20,
		AK27,
		MON3,
		MON10,
		Hydyne,
		Syntin,
		Hydrazine,
		Nitrogen,
		Helium,
		CaveaB,
		LiquidFuel,
		Oxidizer,
		MonoPropellant,
		XenonGas
	}

	public static class FuelName {
		public static string Name (FuelType fuel) {
			switch (fuel) {
				case FuelType.IRFNA_III:
				return "IRFNA-III";
				case FuelType.IRFNA_IV:
				return "IRFNA-III";
				default:
				return fuel.ToString ();
			}
		}
	}
}