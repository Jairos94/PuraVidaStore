using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PuraVidaStoreApi.Model
{
    public class Conexcion
    {
        private string server= "DESKTOP-4FJOI9V";
        private string user= "pvs";
        private string pass= "$psv2022$";
        private string Database= "PuraVidaStore";

        public string CadenaString1 { get; set; } = "Data Source=" & server & ";Initial Catalog=" + Database + ";Persist Security Info=True;User ID=" + user + ";Password=" + pass;

        public string caneda() 
        {
            return CadenaString1;
        }
    }
}
