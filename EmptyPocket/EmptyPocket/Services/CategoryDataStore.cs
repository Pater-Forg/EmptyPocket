using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyPocket.Models;

namespace EmptyPocket.Services
{
    public class CategoryDataStore : IDataStore<Category>
    {
        public List<Category> items;

        public CategoryDataStore()
        {
            items = new List<Category>
            {
                new Category
                {
                    Id = 0,
                    Name = "Продукты",
                    Type = "Расход",
                    Sum = 10000,
                    Number = 8
                },
                new Category
                {
                    Id = 0,
                    Name = "Транспорт",
                    Type = "Расход",
                    Sum = 0,
                    Number = 0
                },
                new Category
                {
                    Id = 0,
                    Name = "Здоровье",
                    Type = "Расход",
                    Sum = 0,
                    Number = 0
                },
                new Category
                {
                    Id = 0,
                    Name = "Зарплата",
                    Type = "Доход",
                    Sum = 30000,
                    Number = 1
                },
            };

            App.Database.database.InsertAllAsync(items);
        }


        public async Task<int> AddItemAsync(Category item)
        {
            //items.Add(item);

            //return await Task.FromResult(true);
            return await App.Database.database.InsertAsync(item);
        }

        public async Task<int> UpdateItemAsync(Category item)
        {
            //var oldItem = items.Where((Category arg) => arg.Id == item.Id).FirstOrDefault();
            //items.Remove(oldItem);
            //items.Add(item);

            //return await Task.FromResult(true);
            return await App.Database.database.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync(Category item)
        {
            //var oldItem = items.Where((Category arg) => arg.Id == id).FirstOrDefault();
            //items.Remove(oldItem);

            //return await Task.FromResult(true);
            return await App.Database.database.DeleteAsync(item);
        }

        public async Task<Category> GetItemAsync(int id)
        {
            // return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
            return await App.Database.database.Table<Category>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Category>> GetItemsAsync(bool forceRefresh = false)
        {
            // return await Task.FromResult(items);
            return await App.Database.database.Table<Category>().ToListAsync();
        }
    }

}
