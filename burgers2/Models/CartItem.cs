using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace burgers2.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int MealId { get; set; }

        public string CartId { get; set; }

        public int Count { get; set; }

        public Meal Meal { get; set; }
    }
}