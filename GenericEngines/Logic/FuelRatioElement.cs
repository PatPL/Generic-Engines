using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	/// <summary>
	/// Class containing FuelType and a double
	/// </summary>
	public class FuelRatioElement : INotifyPropertyChanged {
		private double _ratio;
		private FuelType _propellant;

		public FuelType Propellant { get => _propellant; set { _propellant = value; NotifyPropertyChanged ("MassLabel"); } }

		/// <summary>
		/// Represents Ratio when used in ratio type input and L in tank content type input
		/// </summary>
		public double Ratio { get => _ratio; set { _ratio = value; NotifyPropertyChanged ("MassLabel"); } }

		public FuelRatioElement () {
			Propellant = FuelType.Hydrazine;
			Ratio = 1.0;
		}

		public FuelRatioElement (
			FuelType _Propellant = FuelType.Hydrazine,
			double _Ratio = 1.0
		) {
			Propellant = _Propellant;
			Ratio = _Ratio;
		}

		public string RatioAsVolumeLabel => $"{Ratio.Str (3)}L";
		public string MassLabel => $"{Mass.Str (6)}t";

		/// <summary>
		/// Used by tank content input
		/// </summary>
		public double Mass {
			get {
				// l -> t
				// l * (t/l) = t
				return Ratio * FuelTypeList.GetFuelDensity (Propellant);
			}
			set {
				// t -> l
				// t / (t/l) = l
				Ratio = value / FuelTypeList.GetFuelDensity (Propellant);
				NotifyPropertyChanged ("RatioAsVolumeLabel");
			}
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
		/// Updates EVERY property of this FuelRatioElement
		/// </summary>
		public void NotifyEveryProperty () {
			foreach (PropertyInfo i in typeof (Engine).GetProperties ()) {
				NotifyPropertyChanged (i.Name);
			}
		}
	}
}
