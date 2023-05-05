using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services.IService
{
    public interface IGenericCrudServices
    {
        Task<IEnumerable<T>> LoadData<T, U>(string SP, U parameters);

        Task SaveData<T>(string SP, T parameters);
    }
}
