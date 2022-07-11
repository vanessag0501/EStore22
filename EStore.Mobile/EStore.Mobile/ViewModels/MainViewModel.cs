using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EStore.Mobile.Dialogs;
using Library.EStore.Models;
using Library.EStore.Services;
using Xamarin.Forms;

namespace EStore.Mobile.ViewModels
{

    public class MainViewModel : INotifyPropertyChanged
	{
        public string Query { get; set; }
        public Product SelecetedItem { get; set; }

        private ProductService _productService;

      

        public ObservableCollection<Product> Products
        {
            get
            {

                if (_productService == null)
                {
                    return new ObservableCollection<Product>();
                }

                if(string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Product>(_productService.Products);

                }

                else
                {
                    return new ObservableCollection<Product>
                        (_productService.Products.Where(
                            i => i.name.ToUpper().Contains(Query.ToUpper()) ||
                            i.description.ToUpper().Contains(Query.ToUpper())));

                }


            }
        }

        public MainViewModel()
        {
            _productService = ProductService.Current;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        }

        public async Task Add(itemType iType)
        {
            ContentPage diag = null;

            if(iType == itemType.Product)
            {
                diag = new ProductsDialogs();
            }

            else if(iType == itemType.Cart)
            {
                diag = new CartDialogs();
            }
            else
            {
                throw new NotImplementedException();
            }

            diag = new ProductsDialogs();
            //await diag.ShowAsync();
            NotifyPropertyChanged("Products");

            //_productService.Create(new ProductByQuantity());

            //NotifyPropertyChanged("Products");

        }


        public async Task Remove()
        {
            var id = SelecetedItem?.Id ?? -1;

            if(id >= 1)
            {
                _productService.Delete(SelecetedItem.Id);

            }
            NotifyPropertyChanged("Products");

        }

        public async Task Update()
        {

            if(SelecetedItem!=null)
            {
                var diag = new ProductsDialogs(SelecetedItem);
                await diag.ShowAsync();
                NotifyPropertyChanged("Products");

            }

        }

        public void Save()
        {
            _productService.Save();
            NotifyPropertyChanged("Products");
        }

        public void Load()
        {
            _productService.Load();
            NotifyPropertyChanged("Products");
        }

        public void Refresh()
        {
            NotifyPropertyChanged("Products");
        }

    }

    public enum itemType
    {
        Product, Cart
    }
}

