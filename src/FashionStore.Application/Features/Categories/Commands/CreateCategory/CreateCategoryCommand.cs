using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using Mapster;

namespace FashionStore.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand : ICommand<int>
{
    public string CategoryName { get; init; }
    public int? ParentId { get; init; }
}

public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _unitOfWork.CategoryRepository
            .GetFirstOrDefaultAsync(c => c.CategoryName == request.CategoryName && !c.IsDeleted);

        if (existingCategory != null)
        {
            return ActionResponse<int>.CreateResponse(ResponseCode.BadRequest, message: "Category name already exists.");
        }

        if (request.ParentId.HasValue)
        {
            var parent = await _unitOfWork.CategoryRepository.GetByIdAsync(request.ParentId.Value);
            if (parent == null || parent.IsDeleted)
            {
                return ActionResponse<int>.CreateResponse(ResponseCode.NotFound, message: "Parent category not found.");
            }
        }

        var category = request.Adapt<Category>();

        await _unitOfWork.CategoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActionResponse<int>.Success(category.CategoryId);
    }
}
