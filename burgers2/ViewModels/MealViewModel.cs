using burgers2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace burgers2.ViewModels
{
    public class MealViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="picture is required")]
        public string pictureUrl { get; set; }

        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}