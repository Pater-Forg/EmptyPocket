using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyPocket.Models;

namespace EmptyPocket.Services
{
    public interface ICategoriesStore : IDataStore<Category>
    {
        //Task<IEnumerable<Category>> GetAsync(bool forceRefresh = false);
        Task<IEnumerable<Category>> GetByTypeAsync(string type);
        Task<Category> GetAsync(int id);
        Task<Category> GetByNameAsync(string name);
        Task UpsertAsync(Category category);
        Task DeleteAsync(int id);
    }
}
