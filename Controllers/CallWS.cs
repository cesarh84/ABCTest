using Newtonsoft.Json;
using Productos.Models;
using RestSharp;
using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
using System.Text;
//using System.Text.Json;
//using System.Text.Json.Nodes;
//using System.Threading.Tasks;
//using System.Web;

namespace Productos.Controllers
{
    public class CallWS
    {

        public string Login(Parametros.Headers headers, Parametros.LoginBody body)
        {
            string token = null;

            try
            {
                StringBuilder sbResponse = new StringBuilder();
                var _loginClient = new RestClient(headers.URL);
                var _loginRequest = new RestRequest("authenticate", Method.Post);

                //Headers
                _loginRequest.AddHeader("Content-Type", "application/json");

                //Body
                _loginRequest.AddJsonBody(body);

                //Ejecuta la petición
                RestResponse response = _loginClient.Execute(_loginRequest);

                string statusCode = response.StatusCode.ToString();

                if (statusCode == "OK")
                {
                    var content = response.Content;

                    //Procesa la respuesta
                    token = "Bearer " + JsonConvert.DeserializeObject(content);
                }
                else if (statusCode == "Unauthorized")
                {
                    token = null;
                }
                else
                {
                     token = "Error de comunicación con el servicio de autenticación. Intenta más tarde.";
                }
            }

            catch (Exception ex)
            {
                //codigo para manejar los errores
            }

            return token;
        }

        public RespuestasJSON.AgregarExistenciaProducto AgregarExistenciasWS(Parametros.Headers headers, Parametros.AgregarExistenciaProducto body)
        {
            RespuestasJSON.AgregarExistenciaProducto resultado = null;

            try
            {
                StringBuilder sbResponse = new StringBuilder();
                var _itemClient = new RestClient(headers.URL);
                var _itemRequest = new RestRequest("AgregarExistenciaProd", Method.Post);

                //Headers
                _itemRequest.AddHeader("Content-Type", "application/json");

                //Body
                _itemRequest.AddJsonBody(body);

                //Ejecuta la petición
                RestResponse response = _itemClient.Execute(_itemRequest);

                string statusCode = response.StatusCode.ToString();

                if (statusCode == "OK")
                {
                    var content = response.Content;

                    //Procesa la respuesta
                    resultado = JsonConvert.DeserializeObject<RespuestasJSON.AgregarExistenciaProducto>(content);
                }
                else
                {
                    resultado = null;
                }
            }

            catch (Exception ex)
            {
                //codigo para manejo de errores
            }

            return resultado;

        }

        public RespuestasJSON.ActualizarExistenciaProd ActualizarExistencias(Parametros.Headers headers, Parametros.ActaulizarExistenciaProd body)
        {
            RespuestasJSON.ActualizarExistenciaProd resultado = null;

            try
            {
                StringBuilder sbResponse = new StringBuilder();
                var _itemClient = new RestClient(headers.URL);
                var _itemRequest = new RestRequest("ActualizarExistencias", Method.Post);

                //Headers
                _itemRequest.AddHeader("Content-Type", "application/json");
                _itemRequest.AddHeader("Authorization", headers.Token);

                //Body
                _itemRequest.AddJsonBody(body);

                RestResponse response = _itemClient.Execute(_itemRequest);

                string statusCode = response.StatusCode.ToString();

                if (statusCode == "OK")
                {
                    var content = response.Content;

                    resultado = JsonConvert.DeserializeObject<RespuestasJSON.ActualizarExistenciaProd>(content);
                }
                else
                {
                    resultado = null;
                }
            }

            catch (Exception ex)
            {
                //codigo para manejo de errores.
            }

            return resultado;
        }


        public RespuestasJSON.ObtieneExistenciasEdit ObtenerExistencias(Parametros.Headers headers, int idProducto)
        {
            RespuestasJSON.ObtieneExistenciasEdit resultado = null;

            try
            {
                StringBuilder sbResponse = new StringBuilder();
                var _itemClient = new RestClient(headers.URL);
                var _itemRequest = new RestRequest("ExistenciasGet?idProducto=" + idProducto.ToString());

                //Headers
                _itemRequest.AddHeader("Authorization", headers.Token);

                //ejecucion
                var response = _itemClient.ExecuteGet(_itemRequest);

                string statusCode = response.StatusCode.ToString();

                if (statusCode == "OK")
                {
                    var content = response.Content;
                    //procesa
                    resultado = JsonConvert.DeserializeObject<RespuestasJSON.ObtieneExistenciasEdit>(content);
                }
                else
                {
                    resultado = null;
                }
            }

            catch (Exception ex)
            {
                //codigo para manejo de errores.
            }

            return resultado;

        }
    }
}