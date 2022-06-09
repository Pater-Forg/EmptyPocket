using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyPocket.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmptyPocket.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletPage : ContentPage
    {
        WalletViewModel _viewModel;
        public WalletPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new WalletViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}