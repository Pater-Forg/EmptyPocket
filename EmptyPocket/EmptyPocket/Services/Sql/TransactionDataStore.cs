using EmptyPocket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyPocket.Services
{
    public class TransactionDataStore : ITransactionStore
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


        public async Task UpsertAsync(Transaction transaction)
        {
            await App.Database.database.InsertOrReplaceAsync(transaction);
        }

        public async Task DeleteAsync(int id)
        {
            await App.Database.database.DeleteAsync<Transaction>(id);
        }

        public async Task<Transaction> GetAsync(int id)
        {
            return await App.Database.database.Table<Transaction>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAsync(bool forceRefresh = false)
        {
            return await App.Database.database.Table<Transaction>().ToListAsync();
        }
    }
}