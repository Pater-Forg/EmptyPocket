using EmptyPocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyPocket.Services
{
    public class TransactionDataStore : IDataStore<Transaction>
    {
        public List<Transaction> items;

        public TransactionDataStore()
        {
            items = new List<Transaction>
            {
                new Transaction
                {
                    Id = 0,
                    Category = "Зарплата",
                    Account = "Сбер",
                    Place = "Работа",
                    Comment = "Наконец-то можно поесть",
                    Currency = "RUB",
                    Sum = 30000,
                    Date = DateTime.UtcNow.Date.AddDays(-2)
                },
                new Transaction
                {
                    Id = 0,
                    Category = "Продукты",
                    Account = "Сбер",
                    Place = "Шестерочка",
                    Comment = "Купил буханку хлеба и сырок...",
                    Currency = "RUB",
                    Sum = -10000,
                    Date = DateTime.UtcNow.Date.AddDays(-1.5)
                }
            };

            App.Database.database.InsertAllAsync(items);
        }


        public async Task<int> AddItemAsync(Transaction item)
        {
            //items.Add(item);

            //return await Task.FromResult(true);
            return await App.Database.database.InsertAsync(item);
        }

        public async Task<int> UpdateItemAsync(Transaction item)
        {
            //var oldItem = items.Where((Transaction arg) => arg.Id == item.Id).FirstOrDefault();
            //items.Remove(oldItem);
            //items.Add(item);

            //return await Task.FromResult(true);
            return await App.Database.database.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync(Transaction item)
        {
            //var oldItem = items.Where((Transaction arg) => arg.Id == id).FirstOrDefault();
            //items.Remove(oldItem);

            //return await Task.FromResult(true);
            return await App.Database.database.DeleteAsync(item);
        }

        public async Task<Transaction> GetItemAsync(int id)
        {
            //return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
            return await App.Database.database.Table<Transaction>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Transaction>> GetItemsAsync(bool forceRefresh = false)
        {
            //return await Task.FromResult(items);
            return await App.Database.database.Table<Transaction>().OrderByDescending(x => x.Date).ToListAsync();
        }
    }
}