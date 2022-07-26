using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyPocket.Models;

namespace EmptyPocket.Services
{
    public interface IWalletsStore : IDataStore<Wallet>
    {
        //Task<IEnumerable<Wallet>> GetAsync(bool forceRefresh = false);
        Task<Wallet> GetAsync(int id);
        Task<Wallet> GetByNameAsync(string name);
        Task UpsertAsync(Wallet wallet);
        Task DeleteAsync(int id);
    }
}
