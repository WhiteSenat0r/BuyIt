using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Common;

public interface IEntity<TKey> // Interface that represents a general template of entities with primary key
    where TKey : struct
{
    [Key] public TKey Id { get; set; }
}