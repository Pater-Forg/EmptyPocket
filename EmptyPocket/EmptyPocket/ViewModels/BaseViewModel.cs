using EmptyPocket.Models;
using EmptyPocket.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmptyPocket.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Transaction> TransDataStore => DependencyService.Get<IDataStore<Transaction>>();
        public IDataStore<Category> CatDataStore => DependencyService.Get<IDataStore<Category>>();
        public IDataStore<Wallet> WalDataStore => DependencyService.Get<IDataStore<Wallet>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected async Task<bool> LoadItems<Type>(ObservableCollection<Type> data, IDataStore<Type> dataStore)
        {
            IsBusy = true;

            try
            {
                data.Clear();
                var items = await dataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    data.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
            finally
            {
                IsBusy = false;
            }
            return true;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
