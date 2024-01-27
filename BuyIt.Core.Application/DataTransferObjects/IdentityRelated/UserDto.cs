using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects.IdentityRelated;

public class UserDto
{
    [Required]
    public string DisplayedName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public IEnumerable<string> Roles { get; set; }
    
    [Required]
    public Guid? BasketId { get; set; }
    
    [Required]
    public Guid? WishListId { get; set; }
    
    [Required]
    public Guid? ComparisonListId { get; set; }
}