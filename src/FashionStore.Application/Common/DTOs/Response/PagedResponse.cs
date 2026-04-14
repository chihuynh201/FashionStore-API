using FashionStore.Application.Common.DTOs.Requests;

namespace FashionStore.Application.Common.DTOs.Response;
public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get => PageSize > 0 ? (int)Math.Ceiling((decimal)TotalCount / PageSize) : 0; }
    public static PagedResponse<T> Create(IEnumerable<T> data, int count, Pagination pagination)
    {
        return new PagedResponse<T>
        {
            Items = data,
            TotalCount = count,
            PageSize = pagination.PageSize
        };
    }

    public static PagedResponse<T> CreateEmpty()
    {
        return new PagedResponse<T>
        {
            Items = Enumerable.Empty<T>(),
            TotalCount = 0,
            PageSize = 0
        };
    }
}
