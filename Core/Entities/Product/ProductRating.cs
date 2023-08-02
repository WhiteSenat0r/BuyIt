using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Product.Common.Interfaces;

namespace Core.Entities.Product;

public class ProductRating : IProductRating
{
    private double? _score;

    public ProductRating() { }
    
    public ProductRating
        (Product product, int score)
    {
        Product = product;
        Score = Convert.ToDouble(score);
    }
    
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column(TypeName = "decimal(2, 1)")]
    public double? Score // Product's score: calculates with update of the value
    {
        get => _score ?? Math.Round((double)_score!, 1);
        set
        {
            if (value is < 1 or > 5)
                throw new ArgumentException
                    ("Provided value can not be lesser than 1 and greater than 5!");
            
            if (Score is null)
            {
                _score = value;
                return;
            }

            _score = (Score + value) / 2;
        }
    }

    public Product Product { get; set; } = null!;
}