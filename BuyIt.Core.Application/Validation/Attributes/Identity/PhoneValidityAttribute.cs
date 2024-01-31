using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Validation.Attributes.Identity;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field 
                                          | AttributeTargets.Parameter, AllowMultiple = false)]
public class PhoneValidityAttribute : ValidationAttribute
{
    private const string PhonePattern = 
        @"((\+38)?\(?\d{3}\)?[\s\.-]?(\d{7}|\d{3}[\s\.-]\d{2}[\s\.-]\d{2}|\d{3}-\d{4}))";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not string phone)
            return new ValidationResult("The phone number can not be validated!");

        return !Regex.IsMatch(phone, PhonePattern) 
            ? new ValidationResult(
                "The phone number is not in the correct format! For example: +380971234567") 
            : ValidationResult.Success;
    }
}