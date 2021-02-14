using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace el_sn_marcelo_web.Services
{
    public class AuthenticateAPI
    {
        private HttpClient _client;
        private IHttpContextAccessor _httpContextAccessor;
        public AuthenticateAPI(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _client.BaseAddress = new System.Uri(Environment.GetEnvironmentVariable("URL_API"));
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<dynamic> AuthenticateAsync(string cpf, string senha, string matricula)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new System.Uri(_client.BaseAddress + $"authenticate?cpf={cpf}&senha={senha}&matricula={matricula}")
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
    }
}
