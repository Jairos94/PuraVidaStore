using System.Data.SqlClient;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class DataBase
    {
        //Data Source=DESKTOP-4FJOI9V;Initial Catalog=PuraVidaStore;User ID=pvs;Password=***********
        private const string server = "DESKTOP-4FJOI9V";
        private const string user = "pvs";
        private const string password = "$psv2022$";
        private const string dataBase = "PuraVidaStore";
        private const string conexion = "Data Source=" + server + "; Initial Catalog= " + dataBase + "; User ID=" + user + "; Password=" + password;

        public SqlConnection GetConnection()
        {
            return new SqlConnection(conexion);
        }
    }
}
