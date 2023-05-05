using DataAccess.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DataAccess.Services.IService;

namespace DataAccess.Services.Service
{
    public class GenericCrudServices : IGenericCrudServices
    {

        private readonly IConfiguration _config;
        public GenericCrudServices(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(
            string SP,
            U parameters)
        {
            using (IDbConnection con = new SqlConnection(_config.GetConnectionString("Default")))
            {
                if (con.State != ConnectionState.Open) con.Open();

                return await con.QueryAsync<T>(
                    SP, parameters, commandType: CommandType.StoredProcedure);
            }
        } // GENERIC GET ALL

        public async Task SaveData<T>(
            string SP,
            T parameters)
        {
            using (IDbConnection con = new SqlConnection(_config.GetConnectionString("Default")))
            {
                if (con.State != ConnectionState.Open) con.Open();

                await con.ExecuteAsync(
                    SP, parameters, commandType: CommandType.StoredProcedure);
            }
        } // GENERIC SAVE UPDATE AND DELETE 
    }
}
