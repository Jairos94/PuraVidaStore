using System;

namespace PuraVidaStoreBK.Models.DbContex
{
    public class ReporteMovimivientos
    {
        public string Codigo { get; set; }
        public string DescripcionProducto { get; set; }
        public DateTime fecha { get; set; }
        public string Responsable { get; set; }
        public string Bodega { get; set; }
        public string Descripcion { get; set; }
        public int Ingresos { get; set; }
        public int Salidas { get; set; }

    }
}
