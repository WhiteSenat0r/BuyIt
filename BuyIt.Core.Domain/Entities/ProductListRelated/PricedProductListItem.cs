using Domain.Common;

namespace Domain.Entities.ProductListRelated;

public class PricedProductListItem : ProductListItem
{
    private decimal _price;
    
    public decimal Price
    {
        get => _price;
        set => _price = SetValue(value, 0, "Price");
    }

    protected decimal SetValue(decimal value, int predicateValue, string valueName) =>
        value < predicateValue ? throw new ArgumentException(
            $"{valueName} value can not be less than {predicateValue}!") : value;
}