using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Souvenir
    {
        public int SouvenirID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
    }
}
