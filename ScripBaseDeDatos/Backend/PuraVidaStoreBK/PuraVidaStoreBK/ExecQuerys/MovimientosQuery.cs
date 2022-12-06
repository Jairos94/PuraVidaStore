using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class MovimientosQuery : IMovimientosQuery
    {
        public async Task<Movimiento> IngresoDeProductosPorCompra(Movimiento movimiento)
        {

            using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
            {
                var MotivosMovimiento = await db.MotivosMovimientos
                    .Where(x => x.MtmDescripcion.Contains("Ingreso por compra"))
                    .FirstOrDefaultAsync();

                movimiento.MvmFecha = DateTime.Now;
                movimiento.MvmIdMotivoMovimiento = MotivosMovimiento.MtmId;

                return await ingresarMovimmiento(movimiento);
            }
        }

        public Task<List<Inventarios>> ListaInventarios()
        {
            throw new NotImplementedException();
        }



        private async Task<Movimiento> ingresarMovimmiento(Movimiento movimiento) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {

                    db.Movimientos.Add(movimiento);
                    await db.SaveChangesAsync();
                    return movimiento;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
                return new Movimiento();
            }
        }
    }
}
