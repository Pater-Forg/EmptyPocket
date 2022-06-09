using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyPocket.Models;
using EmptyPocket.ViewModels;
using EmptyPocket.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmptyPocket.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {
        TransactionsViewModel _viewModel;

        public TransactionsPage()
        {

            InitializeComponent();

            BindingContext = _viewModel = new TransactionsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        public async void picker_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (catPicker.SelectedIndex == -1) return;

            var type = (string)catPicker.SelectedItem;
            await Shell.Current.GoToAsync($"{nameof(NewTransactionPage)}?{nameof(NewTransactionViewModel.TransactionType)}={type}");
            catPicker.SelectedIndex = -1;
        }
    }
}