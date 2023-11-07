using Domain.Contracts.ProductRelated;

namespace Application.Contracts;

internal interface IShortDescription
{
    string GetShortDescription(IProduct product);
}