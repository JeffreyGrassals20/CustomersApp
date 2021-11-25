using System;
using System.ComponentModel.DataAnnotations;

namespace CustomersApp.Models
{
    public class Address
    {
        public Address()
        {
        }

        public int IDAddress { get; set; }

        public string CustomerName { get; set; }

        public string CustomerLastName { get; set; }

        [Required (ErrorMessage ="Customer ID Is Required")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Address Is Required")]
        public string Addresses { get; set; }
    }
}
