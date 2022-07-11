using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EStore.Mobile.ViewModels
{
	public class ProductByWeightViewModel : ProductViewModel, INotifyPropertyChanged
	{
        public double Weight
        {
            get;
            set;

        }

        public double totalPrice
        {
            get
            {
                return price * Weight;
            }
        }


        public ProductByWeightViewModel()
        {
        }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        public new void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }


        public override string ToString()
        {
            return $"{Id} :: {name} :: {description} :: ${price} :: {Weight} LB :: ${totalPrice} ";
        }

    }
}

