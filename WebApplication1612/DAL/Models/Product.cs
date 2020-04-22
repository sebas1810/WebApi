using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; } = 0.0;
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
