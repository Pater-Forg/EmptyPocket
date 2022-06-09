using EmptyPocket.ViewModels;
using EmptyPocket.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EmptyPocket
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TransactionDetailPage), typeof(TransactionDetailPage));
            Routing.RegisterRoute(nameof(NewTransactionPage), typeof(NewTransactionPage));
        }

    }
}
