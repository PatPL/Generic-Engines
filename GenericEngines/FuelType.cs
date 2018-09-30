using System.ComponentModel;

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
		ClF3,
		ClF5,
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
		XenonGas,
		IntakeAir
	}

	public static class FuelDensity {
		public static readonly double[] Get = new double[] {
			0.0, //ElectricCharge
			0.001141, //LqdOxygen
			0.00082, //Kerosene
			0.00007085, //LqdHydrogen
			0.00145, //NTO
			0.000791, //UDMH
			0.0009, //Aerozine50
			0.00088, //MMH
			0.001431, //HTP
			0.000719, //AvGas
			0.001658, //IRFNA_III
			0.00000196, //NitrousOxide
			0.00102, //Aniline
			0.00084175, //Ethanol75
			0.0008101, //Ethanol90
			0.000789, //Ethanol
			0.0007021, //LqdAmmonia
			0.00042561, //LqdMethane
			0.00177, //ClF3
			0.0019, //ClF5
			0.000421, //Diborane
			0.000618, //Pentaborane
			0.000544, //Ethane
			0.000568, //Ethylene
			0.0019, //OF2
			0.001505, //LqdFluorine
			0.001604, //N2F4
			0.0007918, //Methanol
			0.00113, //Furfuryl
			0.000829, //UH25
			0.000873, //Tonka250
			0.000811, //Tonka500
			0.001513, //IWFNA
			0.001995, //IRFNA_IV
			0.001499, //AK20
			0.001494, //AK27
			0.001423, //MON3
			0.001407, //MON10
			0.00086, //Hydyne
			0.000851, //Syntin
			0.001004, //Hydrazine
			0.000001251, //Nitrogen
			0.0000001786, //Helium
			0.001501, //CaveaB
			0.001, //LiquidFuel
			0.001, //Oxidizer
			0.0008, //MonoPropellant
			0.000005894, //XenonGas
			0.001225 //IntakeAir
		};
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