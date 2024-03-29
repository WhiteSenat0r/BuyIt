﻿namespace Domain.Entities.ProductListRelated;

public class BasketItem : PricedProductListItem
{
    private int _quantity;
    
    public int Quantity
    {
        get => _quantity;
        set => _quantity = (int)SetValue(value, 0, "Quantity");
    }
}