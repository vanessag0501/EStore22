using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EStore.Mobile.ViewModels
{
	public class ProductByQuantityViewModel :ProductViewModel, INotifyPropertyChanged
    {
        public double Quantity { get; set; }

        public double totalPrice
        {
            get
            {
                return price * Quantity;
            }
        }

        public ProductByQuantityViewModel()
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
            return $"{Id} :: {name} :: {description} :: ${price} ::" +
                $" {Quantity} :: ${totalPrice} ";
        }
    }
}

