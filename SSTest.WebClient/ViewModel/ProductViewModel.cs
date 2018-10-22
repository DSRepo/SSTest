using SSTest.WebClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSTest.WebClient.ViewModel
{
    public class ProductViewModel
    {
        //public Product Product
        //{
        //    get;
        //    set;

        //}

        [Display(Name = "id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Photo")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public byte[] Photo { get; set; }

        [Display(Name = "PhotoName")]
        public string PhotoName { get; set; }

        [Display(Name = "Price")]
        [Required]
        public double Price { get; set; }

        [Display(Name = "LastUpdated")]
        public DateTime LastUpdated { get; set; }
    }
}