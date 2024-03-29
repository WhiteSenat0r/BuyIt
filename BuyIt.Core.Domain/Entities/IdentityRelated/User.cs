﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Entities.IdentityRelated;

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

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    
    public Guid? BasketId { get; set; }
    
    public Guid? WishListId { get; set; }
    
    public Guid? ComparisonListId { get; set; }
    
    private string GetCheckedValue(string value, string valueName)
    {
        return !value.IsNullOrEmpty() 
            ? value
            : throw new ArgumentNullException($"{valueName} is null or empty!");
    }
}