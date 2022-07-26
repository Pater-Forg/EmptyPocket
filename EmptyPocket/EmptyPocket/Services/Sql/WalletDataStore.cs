using EmptyPocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyPocket.Services 
{
    public class WalletDataStore : IWalletsStore
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

        public async Task UpsertAsync(Wallet wallet)
        {
            await App.Database.database.InsertOrReplaceAsync(wallet);
        }

        //public async Task<int> AddItemAsync(Wallet item)
        //{
        //    return await App.Database.database.InsertAsync(item);
        //}

        //public async Task<int> UpdateItemAsync(Wallet item)
        //{
        //    return await App.Database.database.UpdateAsync(item);
        //}

        public async Task DeleteAsync(int id)
        {
            await App.Database.database.DeleteAsync<Wallet>(id);
        }

        public async Task<Wallet> GetAsync(int id)
        {
            return await App.Database.database.Table<Wallet>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Wallet> GetByNameAsync(string name)
        {
            return await App.Database.database.Table<Wallet>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Wallet>> GetAsync(bool forceRefresh = false)
        {
            return await App.Database.database.Table<Wallet>().ToListAsync();
        }
    }

}
