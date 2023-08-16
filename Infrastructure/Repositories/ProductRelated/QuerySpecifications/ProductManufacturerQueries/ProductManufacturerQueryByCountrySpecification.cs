using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;

public class ProductManufacturerQueryByCountrySpecification : QuerySpecification<ProductManufacturer>
{
    public ProductManufacturerQueryByCountrySpecification(string countryName)
        : base(criteria => 
            criteria.RegistrationCountry.ToLower().Equals(countryName.ToLower())) =>
        AddOrderByAscending(m => m.Name);
}