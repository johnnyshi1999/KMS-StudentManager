using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManager.Models
{
    public class StudentViewModel
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public System.DateTime DateOfBirth { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public double Mathematics { get; set; }

        [Required]
        public double Literatures { get; set; }

        [Required]
        public double English { get; set; }

        public double Average
        {
            get
            {
                return (Mathematics + Literatures + English) / 3;
            }
        }
    }
}