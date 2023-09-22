using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Productos.Models
{
    public class Parametros
    {
        public class Headers
        {
            public string URL { get; set; }
            public string Token { get; set; }
        }

        public class LoginBody
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class AgregarExistenciaProducto
        {
            public int IdProducto { get; set; }
            public int Existencia { get; set; }
        }

        public class ActaulizarExistenciaProd
        {
            public int IdProducto { get; set; }
            public int Existencia { get; set; }
        }
    }
}