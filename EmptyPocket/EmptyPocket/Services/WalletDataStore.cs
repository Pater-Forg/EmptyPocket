using EmptyPocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyPocket.Services 
{
    public class WalletDataStore : IDataStore<Wallet>
    {
        public List<Wallet> items;

        public WalletDataStore()
        {
            items = new List<Wallet>
            {
                new Wallet
                {
                    Id = 0,
                    Name = "Сбер",
                    Sum = 20000
                },
                new Wallet
                {
                    Id = 0,
                    Name = "Наличные",
                    Sum = 1100
                }
            };

            App.Database.database.InsertAllAsync(items);
        }

        public async Task<int> AddItemAsync(Wallet item)
        {
            //items.Add(item);

            //return await Task.FromResult(true);
            return await App.Database.database.InsertAsync(item);
        }

        public async Task<int> UpdateItemAsync(Wallet item)
        {
            //var oldItem = items.Where((Wallet arg) => arg.Id == item.Id).FirstOrDefault();
            //items.Remove(oldItem);
            //items.Add(item);

            //return await Task.FromResult(true);
            return await App.Database.database.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync(Wallet item)
        {
            //var oldItem = items.Where((Wallet arg) => arg.Id == id).FirstOrDefault();
            //items.Remove(oldItem);

            //return await Task.FromResult(true);
            return await App.Database.database.DeleteAsync(item);
        }

        public async Task<Wallet> GetItemAsync(int id)
        {
            //return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
            return await App.Database.database.Table<Wallet>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Wallet>> GetItemsAsync(bool forceRefresh = false)
        {
            //return await Task.FromResult(items);
            return await App.Database.database.Table<Wallet>().ToListAsync();
        }
    }

}
