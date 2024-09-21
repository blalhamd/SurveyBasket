using Survey.API.custom;
using System.ComponentModel.DataAnnotations;

namespace Survey.API
{
    public class Student 
    {
        [Required]
        public int Id { get; set; } 
        [Required]
        public string FName { get; set; } = null!;
        [Required]
        public string MiddleName { get; set; } = null!;
        [Required]
        public string LName { get; set; } = null!;
        [Required]
        [ValidateAge(AgeAllowed = 18), Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

    }
}
