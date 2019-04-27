using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstMidName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        [Display(Name = "Home Phone")]
        public string HomePhoneNumber { get; set; }

        [Display(Name = "Work Phone")]
        public string WorkPhoneNumber { get; set; }

        [Required]
        [Display(Name = "Mobile Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage ="Use numbers only please")]
        public string MobilePhoneNumber { get; set; }

        public string Address { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        public string FullName
        {
            get
            {
                return FirstMidName + ", " + LastName; 
            }
        }


        public ICollection<Order> Orders { get; set; }
    }
}
