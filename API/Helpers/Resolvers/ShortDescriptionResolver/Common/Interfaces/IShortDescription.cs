using Core.Entities.Product.Common.Interfaces;

namespace API.Helpers.Resolvers.ShortDescriptionResolver.Common.Interfaces;

internal interface IShortDescription
{
    string GetShortDescription(IProduct product);
}