using FashionStore.Application.Common.Enums;

namespace FashionStore.Application.Common.DTOs.Requests;
public class QueryModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string KeySearch { get; set; }
    public SortOrder SortOrder { get; set; }
    public string SortBy { get; set; }
    public string FilterBy { get; set; }
    public string FilterValue { get; set; }
    public Pagination GetPagination() => new Pagination(PageNumber, PageSize);
}
