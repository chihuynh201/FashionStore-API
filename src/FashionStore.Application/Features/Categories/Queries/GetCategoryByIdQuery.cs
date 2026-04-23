using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using Mapster;
using MediatR;

namespace FashionStore.Application.Features.Categories.Queries;

public record GetCategoryByIdQuery(int CategoryId) : IRequest<ActionResponse<CategoryDto>>;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ActionResponse<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null || category.IsDeleted)
        {
            return ActionResponse<CategoryDto>.CreateResponse(ResponseCode.NotFound, message: "Category not found.");
        }

        return ActionResponse<CategoryDto>.Success(category.Adapt<CategoryDto>());
    }
}
