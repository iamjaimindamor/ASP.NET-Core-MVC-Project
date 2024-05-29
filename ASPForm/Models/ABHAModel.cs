using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPForm.Models
{
    public class ABHAModel
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="ABHA Number is Must")]
        public required string ABHA_Number { get; set; }
        [Required(ErrorMessage = "ABHA ID is Must")]
        public string? ABHA_ID { get; set; }
        [Required(ErrorMessage = "Full Name is Must")]
        public required string Full_Name { get; set; }
        [Required(ErrorMessage = "Gender is Must")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Date Of Birth is Must")]
        public DateOnly DateOfBirth { get; set; }
        [Required(ErrorMessage = "Phone Number is Must")]
        public required string Phone_Number { get; set; }
    }
}