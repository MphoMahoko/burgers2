using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace burgers2.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int MealId { get; set; }
        public Meal Meal { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}