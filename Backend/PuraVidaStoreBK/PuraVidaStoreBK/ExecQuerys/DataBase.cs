using PuraVidaStoreBK.ExecQuerys.Interfaces;
using System.Data.SqlClient;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class DataBase:IDataBase
    {
        private readonly IConfiguration _configuration;
        private  string conexion = "";

        public DataBase(IConfiguration configuration)
        {
            _configuration = configuration;
            conexion = _configuration["ConnectionStrings:sqlServer"];
        }
        public SqlConnection GetConnection()
        {
            
            return new SqlConnection(conexion);
        }
    }
}
