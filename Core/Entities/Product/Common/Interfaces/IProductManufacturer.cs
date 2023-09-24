﻿using Core.Common.Interfaces;

namespace Core.Entities.Product.Common.Interfaces;

public interface IProductManufacturer : IEntity<Guid>
{
    string Name { get; }
    
    ICollection<Product> Products { get; set; }
}