using el_sn_marcelo_web.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace el_sn_marcelo_web
{
    public class APIClient
    {
        private HttpClient _client;
        public APIClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new System.Uri("https://localhost:5002/");
        }

        public async Task<dynamic> GetAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("/usuario");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<dynamic> AuthorizeAsync(string cpf, string senha, string matricula)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new System.Uri(_client.BaseAddress + $"Authenticate?cpf={cpf}&senha={senha}&matricula={matricula}")
                };

                HttpResponseMessage response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStreamAsync();
                else
                    return null;
            }
            catch (System.Exception err)
            {

                throw err;
            }

        }

        public async Task<dynamic> SignUpAsync(object obj)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new System.Uri(_client.BaseAddress + (obj.GetType() == typeof(Cliente) ? "cliente" : "operador")),
                    Content = new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, MediaTypeNames.Application.Json)
                };
                _client.DefaultRequestHeaders.Accept
      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStreamAsync();
                else
                    return null;
            }
            catch (System.Exception err)
            {

                throw err;
            }

        }
    }
}