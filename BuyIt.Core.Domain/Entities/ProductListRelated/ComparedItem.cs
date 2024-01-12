using Domain.Common;

namespace Domain.Entities.ProductListRelated;

public class ComparedItem : ProductListItem
{
    public IDictionary<string, IDictionary<string, string>> Specifications { get; set; }
}