using OnlineShop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas
{
    public class Order_OrderDetail
    {
        public List<Order> lstOrder { get; set; }
        public List<OrderDetail> lstOrderDetail { get; set; } 
        public OrderDetail objOrderDetail { get; set; }
        public Order objOrder { get; set; }
        public List<Product> lstProduct { get; set; }
    }
}