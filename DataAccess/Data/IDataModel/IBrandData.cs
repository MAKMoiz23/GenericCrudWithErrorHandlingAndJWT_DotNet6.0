using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IBrandData
    {
        Task DeleteData(int Id);
        Task<IEnumerable<Brand>> GetAll();
        Task<Brand?> GetById(int Id);
        Task<Brand?> GetDataforAuth(string email, string password);
        Task SaveData(Brand Brand);
        Task UpdateData(Brand Brand);
    }
}