using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Entities.IdentityRelated;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications.Identity;

public sealed class RefreshTokenQuerySpecification : QuerySpecification<RefreshToken>
{
    public RefreshTokenQuerySpecification() => 
        Includes.Add(refreshToken => refreshToken.Include(u => u.User));

    public RefreshTokenQuerySpecification(
        Expression<Func<RefreshToken, bool>> criteria) : base(criteria) =>
        Includes.Add(refreshToken => refreshToken.Include(u => u.User));
}