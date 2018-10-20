using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WCF;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/TransmissorMensagem")]
    public class TransmissorMensagemController : Controller
    {

        // POST api/values
        [HttpPost]
        [Route("InserirMensagem")]
        public async Task<System.Net.HttpStatusCode> InserirMensagem([FromBody]Pacientes paciente)
        {
            try
            {

                HttpResponseMessage response = null;
                using (var client = new HttpClient())
                {
                    var uri = new Uri(@"http://localhost/WCFTransmissorMensagem/TransmissorMensagem.svc/InserirMensagem");
                    var json = JsonConvert.SerializeObject(paciente);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(uri, stringContent);
                    return response.StatusCode;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("teste")]
        public IEnumerable<string> teste()
        {
            return new string[] { "value1", "value2" };
        }

    }
}