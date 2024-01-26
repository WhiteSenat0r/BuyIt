using Domain.Contracts.Common;

namespace Domain.Entities.IdentityRelated;

public class RefreshToken : IEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public User User { get; set; }
    
    public Guid UserId { get; set; }
    
    public string Value { get; set; }
    
    public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(14);
    
    public DateTime? RevocationDate { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiryDate;
    
    public bool IsValid => RevocationDate is null && !IsExpired;
}