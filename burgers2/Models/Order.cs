using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace burgers2.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        [Display(Name ="Postal Code")]
     
        public string Postalcode { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

        public ApplicationUser Customer { get; set; }
        public string CustomerId { get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; }
        public string Status { get; set; }



        public List<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}