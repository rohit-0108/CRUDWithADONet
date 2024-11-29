﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDWithADONet.Models
{
    public class EmployeeModel
    {

        [Key]
        public int  Id  { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        [DisplayName("Date of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("E-mail")]
        [Required]
        public string Email { get; set; }
        [Required]
        public double Salary { get; set; }
        [NotMapped]
        public string FullName { 

            get{ return FirstName + " " + LastName; }
        }
        
        

    }
}
