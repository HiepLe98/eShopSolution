﻿using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.Application.Catalog.Category
{
    public class CategoryService : ICategoryService
    {
        public readonly EShopDbContext _context;
        public CategoryService(EShopDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };
            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.ct.Name
            }).ToListAsync();

        }
    }
}