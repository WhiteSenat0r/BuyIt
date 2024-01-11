namespace Application.DataTransferObjects.IdentityRelated;

public class UserDto
{
    public string DisplayedName { get; set; }
    
    public string Email { get; set; }
    
    public string Token { get; set; }
    
    public IEnumerable<string> Roles { get; set; }
}