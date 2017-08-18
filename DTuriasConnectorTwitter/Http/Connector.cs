using DTuriasCore.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DTuriasConnectorTwitter.Http
{
    class Connector
    {
        HttpClient client;

        public Connector(String url)
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpResponseMessage CreateItem(TodoItem todoItem)
        {
            return Create("/api/todo", todoItem);
        }

        public HttpResponseMessage Create(String path, Object toCreate)
        {
            var content = new StringContent(JsonConvert.SerializeObject(toCreate), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(path, content).Result;
            //_logger.Info(response.StatusCode);
            //_logger.Info(response.Headers.Location);
            return response;
        }
    }
}
