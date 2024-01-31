using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Validation.Attributes.Identity;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field 
                                          | AttributeTargets.Parameter, AllowMultiple = false)]
public class PasswordValidityAttribute : ValidationAttribute
{
    private const string PasswordPattern = "(?=^.{8,32}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*" +
                                           "[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not string password)
            return new ValidationResult("The password can not be validated!");

        return !Regex.IsMatch(password, PasswordPattern) 
            ? new ValidationResult(
                "The password you entered is does not meet one or more of the next criteria: " +
                "be between 8 and 32 characters in length, " +
                "contain at least one digit, " +
                "contain at least one lowercase letter, " +
                "contain at least one uppercase letter, " +
                "contain at least one special character and " +
                "must not contain any whitespace characters!") 
            : ValidationResult.Success;
    }
}