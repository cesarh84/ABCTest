using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Productos.Models;

namespace Productos.Controllers
{
    public class ProductosController : Controller
    {
        private Model1 db = new Model1();

        // GET: Productos
        public ActionResult Index()
        {
            //validamos la seguridad (logueo de usuario correcto)
            Parametros.Headers headers = new Parametros.Headers();
            headers = (Parametros.Headers)this.HttpContext.Session["seguridad"];
            if (headers.Token == null)
            {
                ViewBag.ErrorMensaje = "Ocurrio un problema al intentar loguearse al API. Revise usuario y contraseña";
                return View("Error");
            }

            var productos = db.Productos.Include(p => p.CT_Estatus);
            return View(productos.ToList());
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Estatus = new SelectList(db.CT_Estatus, "Id_Estatus", "Descripcion");
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken] //se utiliza para proteccion contra ataques falsificacion de solicitudes
        public ActionResult Create([Bind(Include = "Id_Producto,Nombre,PrecioDetalle,PrecioMayoreo,Posicion,Id_Estatus")] Producto productos)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Estatus = new SelectList(db.CT_Estatus, "Id_Estatus", "Descripcion", productos.Id_Estatus);
            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Estatus = new SelectList(db.CT_Estatus, "Id_Estatus", "Descripcion", productos.Id_Estatus);
            return View(productos);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken] //se utiliza para proteccion contra ataques falsificacion de solicitudes
        public ActionResult Edit([Bind(Include = "Id_Producto,Nombre,PrecioDetalle,PrecioMayoreo,Posicion,Id_Estatus")] Producto productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Estatus = new SelectList(db.CT_Estatus, "Id_Estatus", "Descripcion", productos.Id_Estatus);
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Existencias item = db.Existencias.FirstOrDefault(x => x.Id_Producto == id);
            if (item != null) {
                ViewBag.ErrorMensaje = "El producto tiene existencias, primero eliminelas y despues vuelva para eliminar el producto";
                return View("Error");
            }

            Producto productos = db.Productos.Find(id);
            db.Productos.Remove(productos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
