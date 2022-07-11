using System;
using System.ComponentModel;

namespace EStore.Mobile.ViewModels
{
	public class PaymentViewModel : INotifyPropertyChanged
	{
		public string pName { get; set; }
		public string Email { get; set; }
		public int CardNum { get; set; }
		public string Expire { get; set; }
		public int CVC { get; set; }
		public string Address { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
		{
			return $"{pName} :: {Email} :: {CardNum} :: {Expire} :: {CVC} :: {Address}";
		}
	}
}

