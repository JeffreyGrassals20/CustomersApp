using System;
using System.ComponentModel.DataAnnotations;

namespace CustomersApp.Models
{
    public class Customers
    {
        public Customers()
        {
        }

        [Required (ErrorMessage ="Customer Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Customer Last Name Is Required")]
        public string LastName { get; set; }

        [Required (ErrorMessage="Customer ID number is Required")]
        [StringLength(11)]
        public string DocumentID { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
