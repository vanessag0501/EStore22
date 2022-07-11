using System;
using System.Threading.Tasks;
using Library.EStore.Models;
using Library.EStore.Services;

using Xamarin.Forms;

namespace EStore.Mobile.Dialogs
{	
	public partial class CartDialogs : ContentPage
	{	
		public CartDialogs ()
		{
			InitializeComponent ();
			BindingContext = new CartDialogs();
		}

		public CartDialogs(Cart selectedItem)
		{
			this.InitializeComponent();
			this.BindingContext = selectedItem;
		}

		private void ContentDialog_PrimaryButtonClick(ContentPage sender, EventArgs s)
		{

			var viewModel = BindingContext as CartViewModel;


			CartService.Current.Create(BindingContext as Product);
		}
		private void ContentDialog_SecondaryButtonClick(ContentPage sender, EventArgs s)
		{

		}

		internal Task ShowAsync()
		{
			throw new NotImplementedException();
		}
	}
}








