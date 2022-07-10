using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.Models;
using System.Threading.Tasks;
using PuraVidaStoreBK.ExecQuerys;

namespace PuraVidaStoreBK.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        UsuariosQuerys Ejecuta = new UsuariosQuerys();

        // GET: UsuarioController
        [HttpGet("GetUsuario")]
        public async Task<IActionResult> GetUsuario(string user,string password)
        //public ActionResult  GetUsuario()
        {
            object Usu = new object();
            Usu = Ejecuta.GetUsuario(user, password);
            try
            {
                return Ok((UsuarioModel)Usu);
            }
            catch (Exception)
            {

                return BadRequest(Usu);
            }
           
<<<<<<< HEAD
=======

           
>>>>>>> feature/Cambios

        }

        [HttpPost("IngresarUsuario")]
        public async Task<IActionResult>  IngresarUsuario(UsuarioModel Usario)
        {

            if (Ejecuta.IngresarUsario(Usario))
            {
                return Ok("Ingreso con exito");
            }
            else 
            {
                return BadRequest("Al ingresar");
            }
        }

        [HttpPost("EditarUsuario")]
        public async Task<IActionResult> EditarUsuario(UsuarioModel Usario)
        {

            if (Ejecuta.EditarUsuario(Usario))
            {
                return Ok("Ingreso con exito");
            }
            else
            {
                return BadRequest("Al ingresar");
            }
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
