using DataAccess.Data.IDataModel;
using DataAccess.Models;
using DataAccess.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.DataModel
{
    public class AuthData : IAuthData
	{
		private readonly IGenericCrudServices _service;

		public AuthData(IGenericCrudServices service)
		{
			_service = service;
		}
		public async Task<User?> GetDataforUserAuth(string email, string password)
		{
			var result = await _service.LoadData<User, dynamic>(
				"[dbo].[sp_Login]",
				new { id = email, pass = password });

			return result.FirstOrDefault();
		}
		public async Task<User?> GetDataforSubUserAuth(string email, string password)
		{
			var result = await _service.LoadData<User, dynamic>(
				"[dbo].[sp_Login_SubUser]",
				new { id = email, pass = password });

			return result.FirstOrDefault();
		}
	}
}
