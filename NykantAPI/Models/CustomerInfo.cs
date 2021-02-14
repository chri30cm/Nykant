﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NykantAPI.Models
{
    public class CustomerInfo
    {
        [Key]
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        [Required]
        [StringLength(20)]
        public string City { get; set; }
        [Required]
        [StringLength(20)]
        public string Country { get; set; }
        [Required]
        [StringLength(20)]
        public string Postal { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }
    }
}
