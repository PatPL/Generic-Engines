using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class PlumeInfo {

		public double Scale; //Scale that makes the plume fit 1m wide engine
		public double EnergyMultiplier;
		public double PositionOffset;
		public double FinalOffset;
		public string PlumeID;
		
	}

	public static class PlumeList {

		private static readonly List<PlumeInfo> plumes = new List<PlumeInfo> () {
			new PlumeInfo {
				PlumeID = "Kerolox-Upper",
				Scale = 0.4,
				PositionOffset = -0.002, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Kerolox-Lower",
				Scale = 0.4,
				PositionOffset = -0.002, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Kerolox-Vernier",
				Scale = 8.5,
				PositionOffset = 0.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 0.5
			}, new PlumeInfo {
				PlumeID = "Cryogenic-UpperLower-125",
				Scale = 0.3,
				PositionOffset = 0.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Cryogenic-UpperLower-25",
				Scale = 0.3,
				PositionOffset = 0.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Cryogenic-UpperLower-375",
				Scale = 0.15,
				PositionOffset = 0.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Alcolox-Lower-A6",
				Scale = 0.6,
				PositionOffset = 0.032638, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Ammonialox",
				Scale = 0.85,
				PositionOffset = 1.0319, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Hydrogen-NTR",
				Scale = 0.8,
				PositionOffset = -0.8, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Hydrolox-Lower",
				Scale = 0.7,
				PositionOffset = 1.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Hydrolox-Upper",
				Scale = 0.8,
				PositionOffset = 1.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Hydynelox-A7",
				Scale = 0.7,
				PositionOffset = -0.854729, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Hypergolic-Lower",
				Scale = 0.95,
				PositionOffset = 0.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Hypergolic-Upper",
				Scale = 1.1,
				PositionOffset = 0.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Hypergolic-OMS-Red",
				Scale = 1.7,
				PositionOffset = 0.514995, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Hypergolic-OMS-White", // (?) - Doesn't have plume definition in RealPlume preset, only flare
				Scale = 0.6,                      //       Flares get scale 0, so the plume might not show at all
				PositionOffset = -0.854729, //mu offset    (Of flare)
				FinalOffset = 0.3,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Hypergolic-Vernier",
				Scale = 4.0,
				PositionOffset = 1.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Ion-Argon-Gridded",
				Scale = 0.4,
				PositionOffset = 0.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Ion-Krypton-Gridded",
				Scale = 0.4,
				PositionOffset = -0.854729, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Ion-Krypton-Hall",
				Scale = 0.4,
				PositionOffset = -0.015503, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Ion-Xenon-Gridded",
				Scale = 0.4,
				PositionOffset = 1.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Ion-Xenon-Hall",
				Scale = 0.4,
				PositionOffset = -0.015503, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Solid-Lower",
				Scale = 0.3,
				PositionOffset = -0.002, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Solid-Upper",
				Scale = 0.3,
				PositionOffset = -0.002, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Solid-Sepmotor",
				Scale = 3.0,
				PositionOffset = 0.0, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Solid-Vacuum",
				Scale = 1.44,
				PositionOffset = 0.35831, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Turbofan",
				Scale = 1.2,
				PositionOffset = -0.41932, //mu offset
				FinalOffset = 0.0,
				EnergyMultiplier = 1.0
			}, new PlumeInfo {
				PlumeID = "Turbojet",
				Scale = 1.2,
				PositionOffset = 1.0, //mu offset
				FinalOffset = -0.6,
				EnergyMultiplier = 1.0
			}
		};

		public static PlumeInfo Get (Plume index) {
			return plumes[(int) index];
		}

		public static string GetName (Plume index) {
			switch (index) {
				default:
				return index.ToString ();
			}
		}

	}

	public enum Plume {
		Kerolox_Upper,
		Kerolox_Lower,
		Kerolox_Vernier,
		Cryogenic_UpperLower_125,
		Cryogenic_UpperLower_25,
		Cryogenic_UpperLower_375,
		Alcolox_Lower,
		Amonnialox,
		Hydrogen_NTR,
		Hydrolox_Lower,
		Hydrolox_Upper,
		Hydynelox_A7,
		Hypergolic_Lower,
		Hypergolic_Upper,
		Hypergolic_OMS_Red,
		Hypergolic_OMS_White,
		Hypergolic_Vernier,
		Ion_Argon_Gridded,
		Ion_Krypton_Gridded,
		Ion_Krypton_Hall,
		Ion_Xenon_Gridded,
		Ion_Xenon_Hall,
		Solid_Lower,
		Solid_Upper,
		Solid_Sepmotor,
		Solid_Vacuum,
		Turbofan,
		Turbojet
	}
}
