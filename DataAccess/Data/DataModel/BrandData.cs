using DataAccess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DataAccess.Data.IDataModel;
using System.Text.Json;
using DataAccess.Services.IService;

namespace DataAccess.Data.DataModel
{
    public class BrandData : IBrandData
    {
        private readonly IGenericCrudServices _services;
        public BrandData(IGenericCrudServices services)
        {
            _services = services;
        }

        public Task<IEnumerable<Brand>> GetAll() =>
            _services.LoadData<Brand, dynamic>("dbo.sp_getBrands_api", new { });

        public async Task<Brand?> GetById(int Id)
        {
            var result = await _services.LoadData<Brand, dynamic>(
                "dbo.sp_GetBrandID_Admin",
                new { id = Id });
            return result.FirstOrDefault();
        }
        public async Task<Brand?> GetDataforAuth(string email, string password)
        {
            var result = await _services.LoadData<Brand, dynamic>(
                "dbo.sp_GetBrand_Auth",
                new { email, password });
            return result.FirstOrDefault();
        }
        

        public Task SaveData(Brand Brand)
        {
            return _services.SaveData("dbo.sp_insertBrand_Admin",
                new
                {
                    Brand.Username,
                    Brand.Name,
                    Brand.Image,
                    Brand.Email,
                    Brand.Password,
                    Brand.CompanyURl, 
                    Brand.Address, 
                    Brand.StatusID, 
                    Brand.Currency, 
                    Brand.BusinessKey, 
                    Brand.LastUpdateBy, 
                    Brand.LastUpdatedDate, 
                    Brand.BrandID });
        }

        public Task UpdateData(Brand Brand)
        {
            return _services.SaveData("dbo.sp_updateBrand_Admin",
                new
                {
                    Brand.Username,
                    Brand.Name,
                    Brand.Image,
                    Brand.Email,
                    Brand.Password,
                    Brand.CompanyURl,
                    Brand.Address,
                    Brand.StatusID,
                    Brand.Currency,
                    Brand.BusinessKey,
                    Brand.LastUpdateBy,
                    Brand.LastUpdatedDate,
                    Brand.BrandID
                });
        }

        public Task DeleteData(int Id) =>
            _services.SaveData("dbo.DeleteBrand", new { ParamTable1 = Id });

    }
}
