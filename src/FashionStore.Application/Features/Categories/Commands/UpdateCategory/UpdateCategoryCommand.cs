using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using Mapster;
using System.Text.Json.Serialization;

namespace FashionStore.Application.Features.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand : ICommand
{
    [JsonIgnore]
    public int CategoryId { get; set; }
    public string CategoryName { get; init; }
    public int? ParentId { get; init; }
}

public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null || category.IsDeleted)
        {
            return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "Category not found.");
        }

        var existingCategory = await _unitOfWork.CategoryRepository
            .GetFirstOrDefaultAsync(c => c.CategoryName == request.CategoryName && c.CategoryId != request.CategoryId && !c.IsDeleted);

        if (existingCategory != null)
        {
            return ActionResponse.CreateResponse(ResponseCode.BadRequest, message: "Category name already exists.");
        }

        if (request.ParentId.HasValue)
        {
            if (request.ParentId == request.CategoryId)
            {
                return ActionResponse.CreateResponse(ResponseCode.BadRequest, message: "A category cannot be its own parent.");
            }

            var parent = await _unitOfWork.CategoryRepository.GetByIdAsync(request.ParentId.Value);
            if (parent == null || parent.IsDeleted)
            {
                return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "Parent category not found.");
            }
        }

        request.Adapt(category);

        _unitOfWork.CategoryRepository.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActionResponse.Success();
    }
}
