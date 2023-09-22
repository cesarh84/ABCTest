using Productos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Productos.Controllers
{
    /// <summary>
    /// customer controller class for testing security token
    /// </summary>
    [Authorize]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {

        private Model1 db = new Model1();

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            var customerFake = "customer-fake";
            return Ok(customerFake);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var customersFake = new string[] { "customer-1", "customer-2", "customer-3" };
            return Ok(customersFake);
        }

        [HttpGet]
        [Route("Test")]
        [AllowAnonymous]
        public IHttpActionResult Test()
        {
            var resultValue = db.Database.SqlQuery<RespuestasJSON.AgregarExistenciaProducto>("exec sp_AgregarExistencias @IdProducto, @Existencia",
               new SqlParameter("IdProducto", 1),
               new SqlParameter("Existencia", 10)).ToList();

            var customersFake = new string[] { "customer-4", "customer-5", "customer-6" };
            return Ok(customersFake);
        }
    }
}
