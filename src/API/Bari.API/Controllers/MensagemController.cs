using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bari.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Bari.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemController : ControllerBase
    {
        private readonly ILogger<MensagemController> _logger;

        public MensagemController(ILogger<MensagemController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddMensagem(Mensagem mensagem)
        {
            try
            {

                #region Fila
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "mensagemQueue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = System.Text.Json.JsonSerializer.Serialize(mensagem);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "mensagemQueue",
                                         basicProperties: null,
                                         body: body);
                }
                #endregion

                return Accepted(mensagem);
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao tentar criar uma mensagem. Erro: {e.Message}");
                return new StatusCodeResult(500);
            }
        }

    }
}