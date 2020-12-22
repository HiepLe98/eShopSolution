using eShopSolution.ViewModels.Catalog.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Products
{
    public class GetPubicProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
        //public string LanguageId { set; get; }
    }
}
