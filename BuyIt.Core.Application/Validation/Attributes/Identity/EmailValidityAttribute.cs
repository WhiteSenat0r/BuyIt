using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Validation.Attributes.Identity;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field 
                                          | AttributeTargets.Parameter, AllowMultiple = false)]
public class EmailValidityAttribute : ValidationAttribute
{
    private const string EmailPattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{1,}$";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not string email)
            return new ValidationResult("The email can not be validated!");

        return !Regex.IsMatch(email, EmailPattern) 
            ? new ValidationResult( "The email you entered is not in the correct format!") 
            : ValidationResult.Success;
    }
}