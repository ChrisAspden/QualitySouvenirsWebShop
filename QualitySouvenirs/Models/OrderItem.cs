using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        [Required]
        public int OrderID { get; set; }
        public Order Orders { get; set; }
        public int SouvenirID { get; set; }
        public Souvenir Souvenir { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalCost { get; set; }
    }
}
