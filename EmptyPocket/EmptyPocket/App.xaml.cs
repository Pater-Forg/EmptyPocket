using EmptyPocket.Services;
using EmptyPocket.Views;
using EmptyPocket.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Diagnostics;
using SQLite;
using System.IO;

namespace EmptyPocket
{
    public partial class App : Application
    {
        static EPDatabase database;

        public static EPDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new EPDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Database.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            DependencyService.Register<TransactionDataStore>();
            DependencyService.Register<CategoryDataStore>();
            DependencyService.Register<WalletDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
