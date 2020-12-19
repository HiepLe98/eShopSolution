using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set;}
        public string Caption { get; set; }
        public long FileSize { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int SortOrder { get; set; }

        public Product Product { get; set; }
    }
}
