using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Interfaces;
using MediatR;

namespace FashionStore.Application.Features.Categories.Queries;

public record GetCategoryListQuery(string Search = null) : IRequest<ActionResponse<List<CategoryDto>>>;

public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, ActionResponse<List<CategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryListQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<List<CategoryDto>>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.CategoryRepository.GetAllHierarchyAsync(request.Search);
        return ActionResponse<List<CategoryDto>>.Success(result);
    }
}
