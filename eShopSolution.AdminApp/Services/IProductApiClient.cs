using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductViewModel>> GetPagings(GetManageProductPagingRequest request);
        Task<ApiResult<ProductViewModel>> GetById(int id, string languageId);
        Task<ApiResult<bool>> UpdateProduct(int id, ProductViewModel request);
        Task<bool> CreateProduct(ProductCreateRequest request);
    }
}
