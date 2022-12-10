using System.Data.SqlClient;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IDataBase
    {
        public SqlConnection GetConnection();
    }
}
