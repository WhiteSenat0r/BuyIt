using System.ComponentModel.DataAnnotations.Schema;
using Domain.Contracts.ProductRelated;

namespace Domain.Entities;

public sealed class ProductRating : IProductRating
{
    private double? _score;

    public ProductRating() { } // Required by EF Core for object's initialization from database
    
    public ProductRating(int? score) =>
        Score = score; // Typically used in non-database initialization

    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Product Product { get; set; }
    
    [Column(TypeName = "decimal(2, 1)")]
    public double? Score // Product's score: calculates every time after the value is updated
    {
        get => _score is null ? null : Math.Round((double)_score!, 1);
        set
        {
            if (Score is null && value is null)
            {
                _score = value;
                return;
            }

            if (value!.GetType() == typeof(double)! && (value is < 1 or > 5))
                throw new ArgumentException
                    ("Provided value can not be lesser than 1 and greater than 5!");

            _score = _score is null ? _score = value : (Score + value) / 2;
        }
    }
}