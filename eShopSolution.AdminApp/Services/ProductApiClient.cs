using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, httpContextAccessor)
        {

        }

        public async Task<PagedResult<ProductViewModel>> GetPagings(GetManageProductPagingRequest request)
        {
            return await GetAsync<PagedResult<ProductViewModel>>($"/api/products/paging?" +
                $"pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}" +
                $"&languageId={request.LanguageId}" +
                $"&categoryId={request.CategoryId}"
                );
        }

        public async Task<ApiResult<ProductViewModel>> GetById(int id, string languageId)
        {
            return await GetAsync<ApiResult<ProductViewModel>>($"/api/products/{id}/{languageId}");
        }
        public async Task<ApiResult<bool>> UpdateProduct(int id, ProductViewModel request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var reponse = await client.PutAsync($"/api/products/{id}", httpContent);
            var result = await reponse.Content.ReadAsStringAsync();
            if (reponse.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<bool> CreateProduct(ProductCreateRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageID);

            var requestContent = new MultipartFormDataContent();
            if(request.ThumbnaiImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnaiImage.OpenReadStream()))
                    data = br.ReadBytes((int)request.ThumbnaiImage.OpenReadStream().Length);

                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbnaiImage", request.ThumbnaiImage.FileName);
            }
            requestContent.Add(new StringContent(request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");

            if (!string.IsNullOrEmpty(request.OriginalPrice.ToString()))
                requestContent.Add(new StringContent(request.OriginalPrice.ToString()), "originalPrice");
            if (!string.IsNullOrEmpty(request.Stock.ToString()))
                requestContent.Add(new StringContent(request.Stock.ToString()), "stock");                
            if (!string.IsNullOrEmpty(request.Details.ToString()))
                requestContent.Add(new StringContent(request.Details.ToString()), "details");
            if (!string.IsNullOrEmpty(request.SeoDescription.ToString()))
                requestContent.Add(new StringContent(request.SeoDescription.ToString()), "seoDescription");
            if (!string.IsNullOrEmpty(request.SeoTitle.ToString()))
                requestContent.Add(new StringContent(request.SeoTitle.ToString()), "seoTitle");
            if (!string.IsNullOrEmpty(request.SeoAlias.ToString()))
                requestContent.Add(new StringContent(request.SeoAlias.ToString()), "seoAlias");
            if (!string.IsNullOrEmpty(languageId))
                requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"api/products/",requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}
