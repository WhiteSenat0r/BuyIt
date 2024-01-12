using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Entities;

public sealed class User : IdentityUser<Guid>
{
    private string _firstName;
    private string _middleName;
    private string _lastName;

    public User() => Id = Guid.NewGuid();

    [MaxLength(64)]
    public string FirstName
    {
        get => _firstName;
        set => _firstName = GetCheckedValue(value, "First name");
    }
    
    [MaxLength(64)]
    public string MiddleName
    {
        get => _middleName;
        set => _middleName = GetCheckedValue(value, "Middle name");
    }
    
    [MaxLength(64)]
    public string LastName
    {
        get => _lastName;
        set => _lastName = GetCheckedValue(value, "Last name");
    }
    
    private string GetCheckedValue(string value, string valueName)
    {
        return !value.IsNullOrEmpty() 
            ? value
            : throw new ArgumentNullException($"{valueName} is null or empty!");
    }
}