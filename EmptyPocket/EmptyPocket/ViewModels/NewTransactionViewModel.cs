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
            var items = await WalDataStore.GetAsync();
            foreach (var item in items)
            {
                wallets.Add(item.Name);
            }
        }

        public async void LoadCategories()
        {
            categories.Clear();
            var items = await CatDataStore.GetAsync();
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

            await TransDataStore.UpsertAsync(newItem);

            await UpdateCategory(Category);
            await UpdateWallet(Account);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async Task<bool> UpdateCategory(string categoryName)
        {
            var _category = await CatDataStore.GetByNameAsync(categoryName);
            _category.Sum += Sum;
            _category.Number += 1;
            await CatDataStore.UpsertAsync(_category);

            return true;
        }

        private async Task<bool> UpdateWallet(string walletName)
        {
            var _wallet = await WalDataStore.GetByNameAsync(walletName);
            _wallet.Sum += Sum;
            await WalDataStore.UpsertAsync(_wallet);

            return true;
        }
    }
}
