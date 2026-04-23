using FashionStore.Application.Common.DTOs.Requests;

namespace FashionStore.Application.Common.DTOs.QueryModels;
public class ProductQueryModel : QueryModel
{
    public int? CategoryId { get; set; }
}
