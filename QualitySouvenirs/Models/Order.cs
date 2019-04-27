using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public enum OrderStatus
    {
        waiting, shipped
    }

    public class Order
    {
        public int OrderID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime Date { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
