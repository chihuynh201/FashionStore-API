using FashionStore.Application.Common.Enums;

namespace FashionStore.Application.Common.DTOs.Response;
public class ActionResponse : BaseResponse
{
    public ActionResponse(ResponseCode codeStatus)
    {
        CodeStatus = codeStatus;
        Message = GetMessage(codeStatus);
    }

    public static ActionResponse CreateResponse(ResponseCode codeStatus, object data = null, string message = null, int? count = null, int? pages = null, object summary = null)
    {
        var response = new ActionResponse(codeStatus)
        {
            Data = data,
            CodeStatus = codeStatus,
            TotalCount = count,
            TotalPages = pages,
            Summary = summary
        };

        if (!string.IsNullOrEmpty(message))
        {
            response.Message = message;
        }
        return response;
    }

    public static ActionResponse Success(object data = null, string message = null, int? count = null, int? pages = null, object summary = null)
    {
        return CreateResponse(ResponseCode.Success, data, message, count, pages, summary);
    }

    private string GetMessage(ResponseCode responseCode)
    {
        return responseCode switch
        {
            ResponseCode.Success => "Success",
            ResponseCode.InvalidData => "Your information is invalid. Please check and try again",
            ResponseCode.NotFound => "Resource not found",
            ResponseCode.BadRequest => "Bad request",
            ResponseCode.Error => "Something went wrong",
            ResponseCode.Unauthorized => "You are not authorized to access this resource",
            ResponseCode.Forbidden => "You are forbidden to access this resource",
            _ => "Sucess"
        };
    }
}

public class ActionResponse<T> : BaseResponse
{
    public static ActionResponse<T> CreateResponse(ResponseCode codeStatus, T data = default(T), string message = null, int? count = null, int? pages = null, object summary = null)
    {
        return new ActionResponse<T>
        {
            Data = data,
            Message = message,
            CodeStatus = codeStatus,
            TotalCount = count,
            TotalPages = pages,
            Summary = summary
        };
    }

    public static ActionResponse<T> Success(T data, string message = null, int? count = null, int? pages = null, object summary = null)
    {
        return CreateResponse(ResponseCode.Success, data, message, count, pages, summary);
    }
}
