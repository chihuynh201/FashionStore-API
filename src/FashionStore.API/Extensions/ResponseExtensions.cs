using FashionStore.Application.Common.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.API.Extensions;

public static class ResponseExtensions
{
    public static ObjectResult AsObjectResult(this ActionResponse response)
    {
        return new ObjectResult(response);
    }

    public static ObjectResult AsObjectResult<T>(this ActionResponse<T> response)
    {
        var rs = ActionResponse.CreateResponse(response.CodeStatus, response.Data, response.Message, summary: response.Summary);
        return rs.AsObjectResult();
    }

    public static ObjectResult AsObjectResult<T>(this ActionResponse<PagedResponse<T>> response)
    {
        PagedResponse<T> pagedData = (PagedResponse<T>)response.Data;

        var rs = ActionResponse.CreateResponse(
            response.CodeStatus,
            pagedData.Items,
            response.Message,
            count: pagedData.TotalCount,
            pages: pagedData.TotalPages,
            summary: response.Summary
        );

        return rs.AsObjectResult();
    }
}
