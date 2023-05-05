using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IAuthData
    {
        Task<User?> GetDataforUserAuth(string email, string password);
        Task<User?> GetDataforSubUserAuth(string email, string password);
    }
}