 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace burgers2.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public string NumberPeople { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Venue { get; set; }
        [Required]
        public string Message { get; set; }

        public ApplicationUser Customer { get; set; }
        
    }
}