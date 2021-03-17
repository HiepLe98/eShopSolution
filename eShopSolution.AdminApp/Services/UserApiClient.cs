﻿using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class UserApiClient : BaseApiClient, IUserApiClient
    {
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            :base(httpClientFactory, configuration, httpContextAccessor)
        {

        }

        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var reponse = await client.PostAsync("/api/users/authenticate", httpContent);
            if (reponse.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await reponse.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await reponse.Content.ReadAsStringAsync());
            
        }

        public async Task<ApiResult<bool>> DeleteUser(Guid id)
        {
            //var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            //var client = _httpClientFactory.CreateClient();
            //client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            //var reponse = await client.DeleteAsync($"/api/users/{id}");

            //var body = await reponse.Content.ReadAsStringAsync();
            //if (reponse.IsSuccessStatusCode)
            //    return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);
            //return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
            return await DeletetAsync<ApiResult<bool>>($"/api/users/{id}");
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            //var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            //var client = _httpClientFactory.CreateClient();
            //client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            //var reponse = await client.GetAsync($"/api/users/{id}");

            //var body = await reponse.Content.ReadAsStringAsync();
            //if(reponse.IsSuccessStatusCode)
            //    return JsonConvert.DeserializeObject<ApiSuccessResult<UserViewModel>>(body);
            //return JsonConvert.DeserializeObject<ApiErrorResult<UserViewModel>>(body);
            return await GetAsync<ApiResult<UserViewModel>>($"/api/users/{id}");
        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUserPagings(GetUserPagingRequest request)
        {
            //var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            //var client = _httpClientFactory.CreateClient();
            //client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            //var reponse = await client.GetAsync($"/api/users/paging?" +
            //    $"pageIndex={request.PageIndex}" +
            //    $"&pageSize={request.PageSize}" +
            //    $"&keyword={request.Keyword}");

            //var body = await reponse.Content.ReadAsStringAsync();
            //var result = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<UserViewModel>>>(body);
            //return result;
            return await GetAsync<ApiResult<PagedResult<UserViewModel>>>($"/api/users/paging?" +
                $"pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}");
        }

        public async Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(registerRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var reponse = await client.PostAsync("/api/users/register", httpContent);
            var result = await reponse.Content.ReadAsStringAsync();
            if(reponse.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await PutAsync<ApiResult<bool>>($"/api/users/{id}/roles", httpContent);

            //var client = _httpClientFactory.CreateClient();
            //client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            //var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            //var reponse = await client.PutAsync($"/api/users/{id}/roles", httpContent);
            //var result = await reponse.Content.ReadAsStringAsync();
            //if (reponse.IsSuccessStatusCode)
            //    return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            //return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var reponse = await client.PutAsync($"/api/users/{id}", httpContent);
            var result = await reponse.Content.ReadAsStringAsync();
            if (reponse.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
