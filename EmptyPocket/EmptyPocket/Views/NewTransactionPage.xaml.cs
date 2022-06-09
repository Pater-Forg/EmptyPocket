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
    public partial class NewTransactionPage : ContentPage
    {
        public NewTransactionViewModel _viewModel;
        public NewTransactionPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NewTransactionViewModel();
        }

        

    }
}