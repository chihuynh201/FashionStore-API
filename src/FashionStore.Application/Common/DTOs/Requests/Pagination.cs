namespace FashionStore.Application.Common.DTOs.Requests;

public sealed class Pagination
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public Pagination(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber <= 0 ? 1 : pageNumber;
        PageSize = pageSize <= 0 ? 10 : pageSize;
    }
}
