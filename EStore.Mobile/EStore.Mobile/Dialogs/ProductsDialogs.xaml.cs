using System;
using System.Threading.Tasks;
using EStore.Mobile.ViewModels;
using Library.EStore.Models;
using Library.EStore.Services;
using Xamarin.Forms;

namespace EStore.Mobile.Dialogs
{
	public partial class ProductsDialogs : ContentPage
	{

		public ProductsDialogs()
		{
			InitializeComponent();
			BindingContext = new Product();

		}

		public ProductsDialogs(Product selectedItem)
		{
			this.InitializeComponent();
			this.BindingContext = selectedItem;
		}

		private void ContentDialog_PrimaryButtonClick(ContentPage sender, EventArgs s)
		{

			var viewModel = BindingContext as ProductViewModel;


			ProductService.Current.Create(BindingContext as Product);
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