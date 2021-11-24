﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CustomersApp.Models
{
    public class Customers
    {
        public Customers()
        {
            this.Name = Name;
            this.LastName = LastName;
            this.DocumentID = DocumentID;
            this.Email = Email;
            this.Phone = Phone;
        }

        [Required (ErrorMessage ="Customer Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Customer Last Name Is Required")]
        public string LastName { get; set; }

        [Required (ErrorMessage="Customer ID number is Required")]
        [StringLength(11)]
        public string DocumentID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
