using Productos.Models;
using System.Net;
using System.Threading;
using System.Web.Http;

namespace Productos.Controllers
{
    /// <summary>
    /// login controller clase para autenticar a los usuarios (TEST ACTION)
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        /// <summary>
        /// se ocupa para validar que el controlador responde correctamente (utileria par usar con codigo o postman)
        /// </summary>
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        /// <summary>
        /// se ocupa para validar si hay algun usaurio autenticado (utileria para user con codigo o postman)
        /// </summary>
        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        /// <summary>
        /// se ocupa para validar el usuario
        /// </summary>
        /// <param name="login">objeto que contien los datos de la cuenta (usuario y password)</param>
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //valida que las credenciales sean correctas (usuario para demostracion)
            bool isCredentialValid = (login.Password == "123456" && login.Username == "admin");
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
