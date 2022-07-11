using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EStore.Mobile.ViewModels
{
	public class ProductViewModel : INotifyPropertyChanged
	{
        public string? name { get; set; }
        public string? description { get; set; }
        public double price { get; set; }

        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public override string ToString()
        {
            return $"{Id} :: {name} :: {description} :: {price} ::";
        }

    }
}

