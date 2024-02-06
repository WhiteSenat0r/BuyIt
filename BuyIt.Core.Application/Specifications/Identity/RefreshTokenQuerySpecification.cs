using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Entities.IdentityRelated;

namespace Application.Specifications.Identity;

public sealed class RefreshTokenQuerySpecification : QuerySpecification<RefreshToken>
{
    public RefreshTokenQuerySpecification() => AddInclude("User");

    public RefreshTokenQuerySpecification(
        Expression<Func<RefreshToken, bool>> criteria) : base(criteria) =>
        AddInclude("User");
}