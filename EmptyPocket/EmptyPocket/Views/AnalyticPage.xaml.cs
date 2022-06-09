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
    public partial class AnalyticPage : ContentPage
    {
        AnalyticViewModel _viewModel;
        public AnalyticPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new AnalyticViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}