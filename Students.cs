using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace studentmanagement
{
    public class Students
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "First name cannot be longer than  255 characters.")]
        public string FirstName { get; set; }


        [MaxLength(25, ErrorMessage = "Last name cannot be longer than  25 characters.")]
        public string LastName { get; set; }


        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public string DateOfBirth { get; set; }


        [MaxLength(2000, ErrorMessage = "First name cannot be longer than  2000 characters.")]
        public string Address { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "First name cannot be longer than  10 characters.")]
        public string PhoneNumber { get; set; }



        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public string EnrolmentDate { get; set; }

        [Required]
        public string CourseName { get; set; }
    }
}
