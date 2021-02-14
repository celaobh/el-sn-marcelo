using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace el_sn_marcelo_web.Services
{
    public class BrandAPI
    {
        private HttpClient _client;
        private IHttpContextAccessor _httpContextAccessor;
        public BrandAPI(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _client.BaseAddress = new System.Uri(Environment.GetEnvironmentVariable("URL_API"));
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<dynamic> PostMarcaAsync(object obj)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new System.Uri(_client.BaseAddress + "marca"),
                    Content = new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, MediaTypeNames.Application.Json)
                };
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.User.Claims.ToList().Where(c => c.Type == "token").Single().Value);
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

        public async Task<dynamic> GetMarcasAsync()
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new System.Uri(_client.BaseAddress + "marca"),
                };
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Authorization =
          new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.User.Claims.ToList().Where(c => c.Type == "token").Single().Value);
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
