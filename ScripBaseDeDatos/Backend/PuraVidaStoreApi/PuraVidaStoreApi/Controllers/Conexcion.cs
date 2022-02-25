using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PuraVidaStoreApi.Model
{
    public class Conexcion
    {
        private string server= "DESKTOP-4FJOI9V";
        private string Database= "PuraVidaStore";
        private string user= "pvs";
        private string pass= "$psv2022$";
        

        public string caneda() 
        {
            string CadenaString = "Data Source=" + server + ";Initial Catalog=" + Database + ";Persist Security Info=True;User ID=" + user + ";Password=" + pass;
            return CadenaString;
        }
    }
}
