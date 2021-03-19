using eShopSolution.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class BaseApiClient
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public readonly IConfiguration _configuration;
        public readonly IHttpContextAccessor _httpContextAccessor;


        protected BaseApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var reponse = await client.GetAsync(url);

            var body = await reponse.Content.ReadAsStringAsync();
            if (reponse.IsSuccessStatusCode)
            {
                TResponse myDeserializeObj = (TResponse)JsonConvert.DeserializeObject(body, typeof(TResponse));
                return  myDeserializeObj;
            }
            return JsonConvert.DeserializeObject<TResponse>(body);
        }
        protected async Task<TResponse> DeletetAsync<TResponse>(string url)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var reponse = await client.DeleteAsync(url);

            var body = await reponse.Content.ReadAsStringAsync();
            if (reponse.IsSuccessStatusCode)
            {
                TResponse myDeserializeObj = (TResponse)JsonConvert.DeserializeObject(body, typeof(TResponse));
                return myDeserializeObj;
            }
            return JsonConvert.DeserializeObject<TResponse>(body);
        }
        protected async Task<TResponse> PutAsync<TResponse>(string url, HttpContent httpContent)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var reponse = await client.PutAsync(url, httpContent);

            var body = await reponse.Content.ReadAsStringAsync();
            if (reponse.IsSuccessStatusCode)
            {
                TResponse myDeserializeObj = (TResponse)JsonConvert.DeserializeObject(body, typeof(TResponse));
                return myDeserializeObj;
            }
            return JsonConvert.DeserializeObject<TResponse>(body);
        }
    }
}
