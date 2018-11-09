using System.ComponentModel.DataAnnotations;

namespace LoginRegistration.ViewModels
{
    public class RegistrationViewModel
    {   
        [Required]
        [MinLength(2, ErrorMessage="First Name should be atleast 2 characters.")]
        [Display (Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage="Last Name should be atleast 2 characters.")]
        [Display (Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display (Name = "Email")]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage="Password must be atleast 8 characters")]
        [DataType(DataType.Password)]
        [Display (Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display (Name = "Confirm Password")]
        [Compare("Password")]
        public string PasswordConfirmation { get; set;}
    }
}