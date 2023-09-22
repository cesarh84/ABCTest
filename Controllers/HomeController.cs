using Productos.Models;
using System.Configuration;
using System.Web.Mvc;

namespace Productos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //validamos primero si se inicio sesion
            Parametros.Headers headers = new Parametros.Headers();
            Parametros.LoginBody body = new Parametros.LoginBody();
            CallWS callWS = new CallWS();

            //obtenemos la url base del servicio para el logueo
            headers.URL = ConfigurationManager.AppSettings["URLWSLOGIN"];

            //se llena usuario y password para logueo
            body.Username = "admin";
            body.Password = "123456";

            //se hace login y se valida el logueo para obtener el token cifrado el cual se ocupara para el consumo de los servicios (API)
            headers.Token = callWS.Login(headers, body);

            //se guarda objeto de encabezado para reutilizarlo en los llamados a los servicios.
            headers.URL = ConfigurationManager.AppSettings["URLWS"];
            this.HttpContext.Session["seguridad"] = headers;

            return View();
        }

    }
}