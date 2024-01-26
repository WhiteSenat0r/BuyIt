namespace Application.DataTransferObjects.IdentityRelated;

public class AuthStatusDto
{
    public bool ContainsAccessToken  { get; set; }
    
    public bool ContainsRefreshToken { get; set; }
}