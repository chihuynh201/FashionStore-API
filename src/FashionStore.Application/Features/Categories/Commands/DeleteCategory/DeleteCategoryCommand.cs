using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;

namespace FashionStore.Application.Features.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(int CategoryId) : ICommand;

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null || category.IsDeleted)
        {
            return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "Category not found.");
        }

        // Check if there are subcategories
        var hasSubcategories = await _unitOfWork.CategoryRepository.AnyAsync(c => c.ParentId == request.CategoryId && !c.IsDeleted);
        if (hasSubcategories)
        {
            return ActionResponse.CreateResponse(ResponseCode.BadRequest, message: "Cannot delete category with subcategories.");
        }

        // Check if there are products
        var hasProducts = await _unitOfWork.ProductRepository.AnyAsync(p => p.CategoryId == request.CategoryId && !p.IsDeleted);
        if (hasProducts)
        {
            return ActionResponse.CreateResponse(ResponseCode.BadRequest, message: "Cannot delete category with products.");
        }

        category.IsDeleted = true;
        _unitOfWork.CategoryRepository.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActionResponse.Success();
    }
}
