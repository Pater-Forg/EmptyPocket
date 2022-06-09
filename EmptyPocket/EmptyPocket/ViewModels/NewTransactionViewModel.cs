using EmptyPocket.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace EmptyPocket.ViewModels
{
    [QueryProperty(nameof(TransactionType), nameof(TransactionType))]
    public class NewTransactionViewModel : BaseViewModel
    {
        public string transactionType;
        public string category;
        public string account;
        public string place;
        public string comment;
        public string currency;
        public int sum;
        public DateTime date;

        public List<string> categories { get; }
        public List<string> wallets { get; }

        public async void LoadTransactionLayout()
        {

        }

        public async void LoadWallets()
        {
            wallets.Clear();
            var items = await WalDataStore.GetItemsAsync();
            foreach (var item in items)
            {
                wallets.Add(item.Name);
            }
        }

        public async void LoadCategories()
        {
            categories.Clear();
            var items = await App.Database.database.Table<Category>().Where(x => x.Type == TransactionType).ToListAsync();
            foreach (var item in items)
            {
                categories.Add(item.Name);
            }
        }
        public NewTransactionViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            categories = new List<string>();
            LoadCategories();
            wallets = new List<string>();
            LoadWallets();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(account)
                && !(sum == 0);
        }

        #region Properties

        public string TransactionType
        {
            get => transactionType;
            set
            {
                SetProperty(ref transactionType, value);
                LoadTransactionLayout();
            }
        }

        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }
        public string Account
        {
            get => account;
            set => SetProperty(ref account, value);
        }
        public string Place
        {
            get => place;
            set => SetProperty(ref place, value);
        }
        public string Comment
        {
            get => comment;
            set => SetProperty(ref comment, value);
        }
        public string Currency
        {
            get => currency;
            set => SetProperty(ref currency, value);
        }
        public int Sum
        {
            get => sum;
            set => SetProperty(ref sum, value);
        }
        public DateTime Date
        {
            get => Date;
            set => SetProperty(ref date, value);
        }

        #endregion

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            if (TransactionType == "Расход")
            {
                Sum *= -1;
            }

            Transaction newItem = new Transaction()
            {
                Id = 0,
                Category = Category,
                Account = Account,
                Place = Place,
                Comment = Comment,
                Currency = "RUB",
                Sum = Sum,
                Date = DateTime.UtcNow.Date
            };

            await TransDataStore.AddItemAsync(newItem);

            await UpdateCategory(Category);
            await UpdateWallet(Account);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async Task<bool> UpdateCategory(string category)
        {
            //Category _category = await CatDataStore.GetItemAsync(category);
            //_category.Sum += Sum;
            //_category.Number += 1;
            //await CatDataStore.UpdateItemAsync(_category);

            var _category = await App.Database.database.GetAsync<Category>(x => x.Name == category);
            _category.Sum += Sum;
            _category.Number += 1;
            await CatDataStore.UpdateItemAsync(_category);

            return true;
        }

        private async Task<bool> UpdateWallet(string wallet)
        {
            //Wallet _wallet = await WalDataStore.GetItemAsync(wallet);
            //_wallet.Sum += Sum;
            //await WalDataStore.UpdateItemAsync(_wallet);

            var _wallet = await App.Database.database.GetAsync<Wallet>(x => x.Name == wallet);
            _wallet.Sum += Sum;
            await WalDataStore.UpdateItemAsync(_wallet);

            return true;
        }
    }
}
