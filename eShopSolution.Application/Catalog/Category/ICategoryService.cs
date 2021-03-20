using eShopSolution.ViewModels.Catalog.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Category
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll(string languageId);
    }
}
