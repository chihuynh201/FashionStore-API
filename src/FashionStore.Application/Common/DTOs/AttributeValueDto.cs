namespace FashionStore.Application.Common.DTOs;

public record AttributeValueDto
{
    public int AttributeValueId { get; init; }
    public string Value { get; init; }
    public int DisplayOrder { get; init; }
}
