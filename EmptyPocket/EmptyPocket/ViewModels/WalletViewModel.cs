using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using EmptyPocket.Models;
using Xamarin.Forms;

namespace EmptyPocket.ViewModels
{
    public class WalletViewModel : BaseViewModel
    {
        public ObservableCollection<Wallet> Items { get; }

        public Command LoadItemsCommand { get; }

        public WalletViewModel()
        {
            Items = new ObservableCollection<Wallet>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await WalDataStore.GetAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
