using Dapper;
using el_sn_marcelo_api_application.Ports.Database;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_infrastructure.Adapter.Database
{
    public class DapperConnection : IDatabasePort
    {
        private SqlConnection conn;
        public DapperConnection()
        {
            if (conn == null)
            {
                conn = new SqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
                conn.Open();
            }
        }

        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                var id = await conn.QueryAsync<int>(sql + "; SELECT CAST(SCOPE_IDENTITY() as int)", param, trans, 60, System.Data.CommandType.Text);
                trans.Commit();
                return id.AsList().Single();
            }

        }


        public void Execute(string sql, object param = null)
        {
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                conn.Execute(sql, param, null, 60, System.Data.CommandType.Text);
                trans.Commit();
            }
        }

        public async Task<dynamic> QueryAsync<T>(string sql, object param = null)
        {
            return await conn.QueryAsync<T>(sql, param, null, 60, System.Data.CommandType.Text);
        }

        public async Task<dynamic> QueryMultipleAsync(string sql, object param = null)
        {
            return await conn.QueryMultipleAsync(sql, param, null, 60, System.Data.CommandType.Text);
        }

        public async Task<dynamic> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, string splitOn = "Id", object param = null)
        {
            // var t = await conn.QueryAsync<T1, T2, TReturn>(sql, map, param, null, true, splitOn, 60, System.Data.CommandType.Text);
            return await conn.QueryAsync<T1, T2, TReturn>(sql, map, param, null, true, splitOn, 60, System.Data.CommandType.Text);
        }

        public async Task<dynamic> QueryAsync(string sql, object param = null)
        {
            return await conn.QueryAsync(sql, param, null, 60, System.Data.CommandType.Text);
        }
    }
}
