using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;

namespace FashionStore.Application.Features.CategoryAttributes.Commands;

public record AssignCategoryAttributesCommand(int CategoryId, List<int> AttributeIds) : ICommand;

public class AssignCategoryAttributesCommandHandler : ICommandHandler<AssignCategoryAttributesCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignCategoryAttributesCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse> Handle(AssignCategoryAttributesCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await _unitOfWork.CategoryRepository.AnyAsync(c =>
            c.CategoryId == request.CategoryId && !c.IsDeleted);

        if (!categoryExists)
        {
            return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "Category not found.");
        }

        var requestedAttributeIds = request.AttributeIds?
            .Distinct()
            .ToHashSet() ?? new HashSet<int>();

        if (requestedAttributeIds.Count > 0)
        {
            var existingAttributeIds = (await _unitOfWork.ProductAttributeRepository.GetAllAsync(
                    pa => requestedAttributeIds.Contains(pa.ProductAttributeId)))
                .Select(pa => pa.ProductAttributeId)
                .ToHashSet();

            var missingAttributeId = requestedAttributeIds
                .Except(existingAttributeIds)
                .Cast<int?>()
                .FirstOrDefault();

            if (missingAttributeId is not null)
            {
                return ActionResponse.CreateResponse(ResponseCode.NotFound, message: $"Attribute with ID={missingAttributeId.Value} not found.");
            }
        }

        var categoryAttributeRepo = _unitOfWork.Repository<CategoryAttribute>();
        var existingLinks = (await categoryAttributeRepo.GetAllAsync(
            ca => ca.CategoryId == request.CategoryId)).ToList();

        var linksToRemove = existingLinks
            .Where(link => !requestedAttributeIds.Contains(link.ProductAttributeId))
            .ToList();

        if (linksToRemove.Count > 0)
        {
            categoryAttributeRepo.DeleteRange(linksToRemove);
        }

        var existingAttributeLinkIds = existingLinks
            .Select(link => link.ProductAttributeId)
            .ToHashSet();

        var linksToAdd = requestedAttributeIds
            .Where(attributeId => !existingAttributeLinkIds.Contains(attributeId))
            .Select(attributeId => new CategoryAttribute
            {
                CategoryId = request.CategoryId,
                ProductAttributeId = attributeId
            })
            .ToList();

        if (linksToAdd.Count > 0)
        {
            await categoryAttributeRepo.AddRangeAsync(linksToAdd);
        }

        if (linksToRemove.Count > 0 || linksToAdd.Count > 0)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return ActionResponse.Success();
    }
}
