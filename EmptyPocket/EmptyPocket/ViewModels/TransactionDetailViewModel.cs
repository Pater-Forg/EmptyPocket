using EmptyPocket.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmptyPocket.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class TransactionDetailViewModel : BaseViewModel
    {
        public int itemId;
        public string category;
        public string account;
        public string place;
        public string comment;
        public string currency;
        public float sum;
        public DateTime date;
        public string Id { get; set; }
        public string Category {
            get => category;
            set => SetProperty(ref category, value);
        }
        public string Account {
            get => account;
            set => SetProperty(ref account, value);
        }
        public string Place {
            get => place;
            set => SetProperty(ref place, value);
        }
        public string Comment {
            get => comment;
            set => SetProperty(ref comment, value);
        }
        public string Currency {
            get => currency;
            set => SetProperty(ref currency, value);
        }
        public float Sum {
            get => sum;
            set => SetProperty(ref sum, value);
        }
        public DateTime Date {
            get => date;
            set => SetProperty(ref date, value);
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await TransDataStore.GetItemAsync(itemId);
                Id = item.Id.ToString();
                Category = item.Category;
                Account = item.Account;
                Place = item.Place;
                Comment = item.Comment;
                Currency = item.Currency;
                Sum = item.Sum;
                Date = item.Date;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Transaction");
            }
        }
    }
}
