using System.ComponentModel.DataAnnotations;
using Application.Validation.Attributes.Identity;

namespace Application.DataTransferObjects.IdentityRelated;

public class RegistrationDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(64)]
    public string FirstName { get; set; }
    
    [Required]
    [MinLength(1)]
    [MaxLength(64)]
    public string MiddleName { get; set; }
    
    [Required]
    [MinLength(1)]
    [MaxLength(64)]
    public string LastName { get; set; }
    
    [Required]
    [EmailValidity]
    public string Email { get; set; }
    
    [Required]
    [PhoneValidity]
    public string PhoneNumber { get; set; }
    
    [Required]
    [PasswordValidity]
    public string Password { get; set; }
}