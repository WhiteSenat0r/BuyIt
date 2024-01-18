using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Validation.Attributes.Links
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field 
                                              | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ImageLinkValidityAttribute : ValidationAttribute
    {
        private const string ImageLinkPattern = "\\b(?:https?):\\/\\/[^\\s\\/$.?#].[^\\s]*\\.(?:jpg)\\b";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not string email)
                return new ValidationResult("The image link can not be validated!");

            return !Regex.IsMatch(email, ImageLinkPattern) 
                ? new ValidationResult( 
                    "The link you entered is not in the correct format or has unsuitable image format!!") 
                : ValidationResult.Success;
        }
    }
}