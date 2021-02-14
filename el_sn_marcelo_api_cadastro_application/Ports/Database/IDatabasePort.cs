using System;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_cadastro_application.Ports.Database
{

    public interface IDatabasePort
    {
        Task<dynamic> QueryAsync<T>(string sql, object param = null);
        Task<dynamic> QueryAsync(string sql, object param = null);
        Task<dynamic> QueryAsync<T1, T2, TReturn>(string sql,  Func<T1, T2, TReturn> map, string splitOn = "Id", object param = null);
        Task<int> ExecuteAsync(string sql, object param = null);
        void Execute(string sql, object param = null);
        Task<dynamic> QueryMultipleAsync(string sql, object param = null);


    }
}
