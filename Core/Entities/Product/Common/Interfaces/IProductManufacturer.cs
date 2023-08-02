using Core.Common.Interfaces;

namespace Core.Entities.Product.Common.Interfaces;

public interface IProductManufacturer : IEntity<Guid>
{
    string Name { get; set; }
    
    string RegistrationCountry { get; set; }
    
    Product Product { get; set; }
}