using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Productos.Models
{
    public class RespuestasJSON
    {
        public class AgregarExistenciaProducto
        {
            public int Resultado { get; set; }
        }

        public class ActualizarExistenciaProd
        {
            public int Resultado { get; set; }
        }

        public class ObtieneExistenciasEdit
        {
            public int Id_Producto { get; set; }
            public int Existencia { get; set; }
        }

    }
}