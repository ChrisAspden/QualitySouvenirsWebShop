using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Souvenir> Souvenirs { get; set; }
    }
}
