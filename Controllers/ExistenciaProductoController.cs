using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
//using System.Web;
using System.Web.Mvc;
using Productos.Models;

namespace Productos.Controllers
{
    public class ExistenciaProductoController : Controller
    {
        private Model1 db = new Model1();
        Parametros.Headers headers = new Parametros.Headers();

        // GET: ExistenciaProducto
        public ActionResult Index()
        {

            //validamos la seguridad (logueo de usuario correcto)
            headers = (Parametros.Headers)this.HttpContext.Session["seguridad"];
            if (headers.Token == null)
            {
                ViewBag.ErrorMensaje = "Ocurrio un problema al intentar loguearse al API. Revise usuario y contraseña";
                return View("Error");
            }

            var existencias = db.Existencias.Include(e => e.Productos);
            return View(existencias.ToList());
        }

        // GET: ExistenciaProducto/Create
        public ActionResult Create()
        {
            ViewBag.Id_Producto = new SelectList(db.Productos, "Id_Producto", "Nombre");
            return View();
        }

        // POST: ExistenciaProducto/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // proteccion contra falsificacion de solicitudes
        public ActionResult Create([Bind(Include = "Id_Producto,Existencia")] Existencias existencias)
        {
            if (ModelState.IsValid)
            {
                headers = (Parametros.Headers)this.HttpContext.Session["seguridad"];

                Parametros.AgregarExistenciaProducto body = new Parametros.AgregarExistenciaProducto();
                body.Existencia = existencias.Existencia;
                body.IdProducto = existencias.Id_Producto;

                CallWS callWS = new CallWS();
                
                RespuestasJSON.AgregarExistenciaProducto res = callWS.AgregarExistenciasWS(headers, body);

                if (res == null)
                {
                    return HttpNotFound();
                }

                if (res.Resultado == 0)
                {
                    ViewBag.ErrorMensaje = "Ya hay existencia para este producto";
                    return View("Error");
                }

                if (res.Resultado == 2)
                {
                    ViewBag.ErrorMensaje = "Ocurrio un error al intentar agregar la existencia, revise con el administrador";
                    return View("Error");
                }

                return RedirectToAction("Index");

            }

            ViewBag.Id_Producto = new SelectList(db.Productos, "Id_Producto", "Nombre", existencias.Id_Producto);
            return View(existencias);
        }

        // GET: ExistenciaProducto/Edit/5
        public ActionResult Edit(int? id)
        {
            int idSeleccionado = id.Value;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            headers = (Parametros.Headers)this.HttpContext.Session["seguridad"];
            CallWS callWS = new CallWS();
            RespuestasJSON.ObtieneExistenciasEdit res = callWS.ObtenerExistencias(headers, idSeleccionado);

            if (res == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_Producto = new SelectList(db.Productos, "Id_Producto", "Nombre", res.Id_Producto);

            return View(res);
        }

        // POST: ExistenciaProducto/Edit/5
        [HttpPost]
        public ActionResult Edit(string Id_Producto,string Existencia)
        {
            if (ModelState.IsValid)
            {
                headers = (Parametros.Headers)this.HttpContext.Session["seguridad"];
                CallWS callWS = new CallWS();
                Parametros.ActaulizarExistenciaProd item = new Parametros.ActaulizarExistenciaProd();
                item.IdProducto = Convert.ToInt32(Id_Producto);
                item.Existencia = Convert.ToInt32(Existencia);

                RespuestasJSON.ActualizarExistenciaProd res = callWS.ActualizarExistencias(headers, item);

                if (res == null)
                {
                    return HttpNotFound();
                }

                if (res.Resultado == 2)
                {
                    ViewBag.ErrorMensaje = "El registro que quiere actualizar no existe";
                    return View("Error");
                }

                if (res.Resultado == 0)
                {
                    ViewBag.ErrorMensaje = "Ocurrio un problema por favor contacte con su administrador";
                    return View("Error");
                }

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        // GET: ExistenciaProducto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Existencias existencias = db.Existencias.FirstOrDefault(x => x.Id_Producto == id);
            if (existencias == null)
            {
                return HttpNotFound();
            }
            return View(existencias);
        }

        // POST: ExistenciaProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //proteccion contra ataques de falsificacion de solicitudes
        public ActionResult DeleteConfirmed(int id)
        {
            Existencias existencias = db.Existencias.FirstOrDefault(x => x.Id_Producto == id);
            db.Existencias.Remove(existencias);
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
