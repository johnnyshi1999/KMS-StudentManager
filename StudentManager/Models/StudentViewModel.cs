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
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dt. Of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DateOfBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone No.")]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(0, 10)]
        public double Mathematics { get; set; }

        [Required]
        [Range(0, 10)]
        public double Literatures { get; set; }

        [Required]
        [Range(0, 10)]
        public double English { get; set; }

        [Display(Name = "Avg. Score")]
        public double Average
        {
            get
            {
                return (Mathematics + Literatures + English) / 3;
            }
        }

    }
}