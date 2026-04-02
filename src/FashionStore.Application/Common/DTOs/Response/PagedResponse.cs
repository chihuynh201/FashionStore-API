using FashionStore.Application.Common.Enums;

namespace FashionStore.Application.Common.DTOs.Response;
public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; }
    public string Message { get; set; }
    public ResponseCode CodeStatus { get; set; }
    public int CodeNumber { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get => PageSize > 0 ? (int)Math.Ceiling((decimal)TotalCount / PageSize) : 0; }
    public static PagedResponse<T> CreatePagedResponse(ResponseCode codeStatus, IEnumerable<T> data = null, string message = null, int count = 0, int pageSize = 0)
    {
        return new PagedResponse<T>
        {
            Items = data,
            Message = message,
            CodeStatus = codeStatus,
            TotalCount = count,
            PageSize = pageSize
        };
    }

    public static PagedResponse<T> CreateEmpty()
    {
        return new PagedResponse<T>
        {
            Items = Enumerable.Empty<T>(),
            Message = "No data found",
            TotalCount = 0,
            PageSize = 0
        };
    }
}
