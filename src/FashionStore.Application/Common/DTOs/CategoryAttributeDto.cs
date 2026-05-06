namespace FashionStore.Application.Common.DTOs;

public record CategoryAttributeSummaryDto
{
    public int CategoryAttributeId { get; init; }
    public int ProductAttributeId { get; init; }
    public string AttributeName { get; init; }
}

public record CategoryAttributeDto : CategoryAttributeSummaryDto
{
    public List<AttributeValueDto> AttributeValues { get; init; } = [];
}

public record CategoryWithAttributesDto
{
    public int CategoryId { get; init; }
    public string CategoryName { get; init; }
    public List<CategoryAttributeSummaryDto> Attributes { get; init; } = [];
}
