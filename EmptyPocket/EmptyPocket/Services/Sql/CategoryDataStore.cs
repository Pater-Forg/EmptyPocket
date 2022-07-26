using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyPocket.Models;

namespace EmptyPocket.Services
{
    public class CategoryDataStore : ICategoriesStore
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

        public async Task UpsertAsync(Category category)
        {
            await App.Database.database.InsertOrReplaceAsync(category);
        }

        //public async Task<int> AddItemAsync(Category item)
        //{
        //    return await App.Database.database.InsertAsync(item);
        //}

        //public async Task<int> UpdateItemAsync(Category item)
        //{
        //    return await App.Database.database.UpdateAsync(item);
        //}

        public async Task DeleteAsync(int id)
        {
            await App.Database.database.DeleteAsync<Category>(id);
        }

        public async Task<Category> GetAsync(int id)
        {
            return await App.Database.database.Table<Category>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetAsync(bool forceRefresh = false)
        {
            return await App.Database.database.Table<Category>().ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetByTypeAsync(string type)
        {
            return await App.Database.database.Table<Category>().Where(x => x.Type == type).ToListAsync();
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await App.Database.database.Table<Category>().Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }

}
