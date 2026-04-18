namespace FashionStore.Application.Common.DTOs;

public record ProductAttributeDto
{
    public int ProductAttributeId { get; init; }
    public string AttributeName { get; init; }
    public List<AttributeValueDto> AttributeValues { get; init; } = new();
}
