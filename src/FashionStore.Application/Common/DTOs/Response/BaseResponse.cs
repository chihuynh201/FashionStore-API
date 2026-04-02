using FashionStore.Application.Common.Enums;
using System.Text.Json.Serialization;

namespace FashionStore.Application.Common.DTOs.Response;
public class BaseResponse
{
    public object Data { get; set; }
    public string Message { get; set; }

    public ResponseCode CodeStatus { get; set; }
    public int CodeNumber { get => (int)CodeStatus; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object Summary { get; set; } = null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TotalCount { get; set; } = null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TotalPages { get; set; } = null;
}
