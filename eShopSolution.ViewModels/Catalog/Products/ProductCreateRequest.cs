using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Products
{
    public class ProductCreateRequest //: PagingRequestBase
    {
        [Required(ErrorMessage ="Giá sản phẩm không thể để trống")]
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }

        [Required(ErrorMessage ="Tên sản phẩm không thể để trống")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Mô tả không thể để trống")]
        public string Description { get; set; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public IFormFile ThumbnaiImage { get; set; }
    }
}
