using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyPocket.Services
{
    public interface IDataStore<Type>
    {
        Task<IEnumerable<Type>> GetAsync(bool forceRefresh = false);
    }
}
