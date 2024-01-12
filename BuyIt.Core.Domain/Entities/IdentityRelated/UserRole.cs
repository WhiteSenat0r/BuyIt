using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityRelated;

public sealed class UserRole : IdentityRole<Guid>
{
    public UserRole() { }

    public UserRole(string roleName) : base(roleName)
    {
        Id = Guid.NewGuid();
    }
}