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
        

        // GET: UsuarioController
        [HttpGet("GetUsuario")]
        public async Task<IActionResult> GetUsuario(string user,string password)
        //public ActionResult  GetUsuario()
        {
            UsuarioModel Usu = new UsuarioModel();
            UsuariosQuerys Ejecuta = new UsuariosQuerys();
            Usu = Ejecuta.GetUsuario(user, password);
            if (Usu.Usuario != null) 
            { 
                return  Ok(Usu); 
            }

            else
            {
                return BadRequest("Error de usuario");
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
