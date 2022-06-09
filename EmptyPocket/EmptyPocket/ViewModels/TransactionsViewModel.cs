using EmptyPocket.Models;
using EmptyPocket.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using EmptyPocket.Services;

namespace EmptyPocket.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        private Transaction _selectedItem;

        public ObservableCollection<Transaction> Transactions { get; }
        public ObservableCollection<Category> Categories { get; }
        public ObservableCollection<Wallet> Wallets { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Transaction> ItemTapped { get; }
        public TransactionsViewModel()
        {
            Transactions = new ObservableCollection<Transaction>();
            LoadItemsCommand = new Command(async () => await LoadItems(Transactions, TransDataStore));

            ItemTapped = new Command<Transaction>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            Categories = new ObservableCollection<Category>();
            Wallets = new ObservableCollection<Wallet>();

        }

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            await LoadItems(Categories, CatDataStore);
            await LoadItems(Wallets, WalDataStore);
            Title = Wallets.Sum(x => x.Sum).ToString();
        }

        public Transaction SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public async void OnAddItem(object obj)
        {
            var picker = obj as Picker;
            picker.IsEnabled = true;
            //picker.IsVisible = true;
            picker.Focus();
            //await Shell.Current.GoToAsync(nameof(NewTransactionPage));
        }

        async void OnItemSelected(Transaction item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(TransactionDetailPage)}?{nameof(TransactionDetailViewModel.ItemId)}={item.Id}");
        }

    }
}