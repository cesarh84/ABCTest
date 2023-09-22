//using System;
//using System.Collections.Generic;
using System.Data.SqlClient;
//using System.Data;
using System.Linq;
//using System.Net;
//using System.Net.Http;
using System.Web.Http;
//using System.Xml.Linq;
using Productos.Models;

namespace Productos.Controllers
{
    /// <summary>
    /// API que concentra los enpoints de las operaciones
    /// </summary>
    [Authorize]
    [RoutePrefix("api/ws")]
    public class WSController : ApiController
    {

        private Model1 db = new Model1();

        //[HttpGet]
        //public IHttpActionResult GetId(int id)
        //{
        //    var customerFake = "customer-fake";
        //    return Ok(customerFake);
        //}

        //[HttpGet]
        //public IHttpActionResult GetAll()
        //{
        //    var customersFake = new string[] { "customer-1", "customer-2", "customer-3" };
        //    return Ok(customersFake);
        //}

        //[HttpPost]
        //[Route("TEST")]
        //public IHttpActionResult TEST(Parametros.AgregarExistenciaProducto body)
        //{
        //    var customersFake = new string[] { "customer-1", "customer-2", "customer-3" };
        //    return Ok(customersFake);
        //}


        /// <summary>
        /// Enpoint para agregar existencias a un producto
        /// </summary>
        /// <param name="body">objeto que contiene los datos necesarios para el cuerpo de la peticion</param>
        [HttpPost]
        [Route("AgregarExistenciaProd")]
        [AllowAnonymous]
        public RespuestasJSON.AgregarExistenciaProducto AgregarExistenciaProd(Parametros.AgregarExistenciaProducto body)
        {
            var result = new RespuestasJSON.AgregarExistenciaProducto();

            var resultValue = db.Database.SqlQuery<RespuestasJSON.AgregarExistenciaProducto>("exec sp_AgregarExistencias @IdProducto, @Existencia",
                new SqlParameter("IdProducto", body.IdProducto),
                new SqlParameter("Existencia", body.Existencia)).ToList<RespuestasJSON.AgregarExistenciaProducto>();

            result.Resultado = resultValue[0].Resultado;

            return result;
        }

        /// <summary>
        /// Enpoint que actualiza las existencias de algun producto especifico
        /// </summary>
        /// <param name="body">objeto que contiene los datos necesarios para el cuerpo de la peticion</param>
        [HttpPost]
        [Route("ActualizarExistencias")]
        [AllowAnonymous] //con este se descarta del authorize y se permite ingresar sin token (se dejo asi como ejemplo para este ejercicio)
        public RespuestasJSON.ActualizarExistenciaProd ActualizarExistencias(Parametros.ActaulizarExistenciaProd body)
        {
            var result = new RespuestasJSON.ActualizarExistenciaProd();

            var resultValue = db.Database.SqlQuery<RespuestasJSON.ActualizarExistenciaProd>("exec sp_ActualizarExistencias @IdProducto, @Existencia",
                new SqlParameter("IdProducto", body.IdProducto),
                new SqlParameter("Existencia", body.Existencia)).ToList<RespuestasJSON.ActualizarExistenciaProd>();

            result.Resultado = resultValue[0].Resultado;

            return result;
        }

        /// <summary>
        /// Enpoint para obtener datos de existencias de un producto en especifico ya seleccionado
        /// </summary>
        /// <param name="idProducto">id del producto seleccionado a visualizar</param>
        [HttpGet]
        [Route("ExistenciasGet")]
        public RespuestasJSON.ObtieneExistenciasEdit ExistenciasGet(int idProducto)
        {
            var result = new RespuestasJSON.ObtieneExistenciasEdit();

            var resultValue = db.Database.SqlQuery<RespuestasJSON.ObtieneExistenciasEdit>("exec sp_ObtieneExistencia @IdProducto",
                new SqlParameter("IdProducto", idProducto)).ToList<RespuestasJSON.ObtieneExistenciasEdit>();

            result.Existencia = resultValue[0].Existencia;
            result.Id_Producto = idProducto;

            return result;
        }

        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}