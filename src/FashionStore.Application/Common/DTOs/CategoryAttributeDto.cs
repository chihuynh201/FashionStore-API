namespace FashionStore.Application.Common.DTOs;

public record CategoryAttributeDto
{
    public int CategoryAttributeId { get; init; }
    public int ProductAttributeId { get; init; }
    public string AttributeName { get; init; }
}
