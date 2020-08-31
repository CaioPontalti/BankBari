
using Bari.Consumer.Domain;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bari.API.Service
{
    public class MensagemService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Timer t = new Timer(TimerCallbackAsync, null, 0, 5000);

            await Task.CompletedTask;
        }

        private static void TimerCallbackAsync(Object o)
        {
            var mensagen = new Mensagem();

            var content = new StringContent(mensagen.ToString(), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44370");

            httpClient.PostAsync("/api/Mensagem", content);

        }
    }
}
