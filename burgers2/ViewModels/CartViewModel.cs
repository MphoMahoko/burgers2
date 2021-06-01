using burgers2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace burgers2.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public int Total { get; set; }
    }

}