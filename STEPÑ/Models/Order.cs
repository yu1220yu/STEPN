using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace STEPÑ.Models
{
    public class Order
    {
        [DisplayFormat(DataFormatString = "{0:000000}")]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

        public decimal SubTotal { get; set; }
        public List<OrderItem> OrderItem { get; set; }
        public bool isPaid { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }

    public class CartItem : OrderItem
    {
        public CartItem() { }
        public CartItem(OrderItem order)
        {
            this.OrderId = order.OrderId;
            this.ProductId = order.ProductId;
            this.Price = order.Price;
        }
        public Product Product { get; set; }
    }
}
