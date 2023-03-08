using OnlineShop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class ProductDetailModel
    {
        public Category objCategory { get; set; }
        public Product objProduct { get; set; }
        public List<Category> ListCategory {get;set;}
        public List<Product> ListProduct { get; set; }
    }
}