using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;

public sealed class ProductManufacturerQueryByCountrySpecification : BaseProductManufacturerQuerySpecification
{
    public ProductManufacturerQueryByCountrySpecification(string countryName)
        : base(criteria => 
            criteria.RegistrationCountry.ToLower().Equals(countryName.ToLower())) =>
        AddOrderByAscending(m => m.Name);
}