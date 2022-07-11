using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Library.EStore.Models;
using Xamarin.Essentials;
using System.Runtime.CompilerServices;
using EStore.Mobile.Dialogs;
using System.Collections.ObjectModel;
using Library.EStore.Services;
using EStore.Mobile.ViewModels;

namespace EStore.Mobile
{
    public sealed partial class MainPage : ContentPage
    {       

        //public ObservableCollection<Product> Products { get; set; }
       
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            
        }

        void Searched_Clicked(System.Object sender, System.EventArgs e)
        {



            (BindingContext as MainViewModel).Refresh();




        }

        private async void Add_Clicked(object sender, EventArgs e)
        {

            var vm = BindingContext as MainViewModel;

            if(vm !=null)
            {
                await vm.Add(itemType.Product);
            }




        }

        private async void Add_CartClicked(object sender, EventArgs e)
        {

            var vm = BindingContext as MainViewModel;

            if (vm != null)
            {
                await vm.Add(itemType.Cart);
            }




        }
        private void Removed_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as MainViewModel;

            if (vm != null)
            {
                vm.Remove();
            }

        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as MainViewModel;

            if (vm != null)
            {
                vm.Update();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).Save();

        }

        private void Load_Click(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).Load();
        }
    }
}

