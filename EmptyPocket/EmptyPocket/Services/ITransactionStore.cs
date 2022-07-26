using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyPocket.Models;

namespace EmptyPocket.Services
{
    public interface ITransactionStore : IDataStore<Transaction>
    {
        //Task<IEnumerable<Transaction>> GetAsync(bool forceRefresh = false);
        Task<Transaction> GetAsync(int id);
        Task UpsertAsync(Transaction transaction);
        Task DeleteAsync(int id);
    }
}
