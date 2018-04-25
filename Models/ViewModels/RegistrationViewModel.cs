using System.ComponentModel.DataAnnotations;

namespace BeltExamCSharp2.Models {
    public class RegistrationViewModel {
 
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        public string Alias { get; set; }
 
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z\s]*$")]
        public string Name { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string PasswordConfirmation { get; set; }
    }
}