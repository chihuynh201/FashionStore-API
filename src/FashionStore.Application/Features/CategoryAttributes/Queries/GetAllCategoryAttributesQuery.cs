using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Interfaces;
using MediatR;

namespace FashionStore.Application.Features.CategoryAttributes.Queries;
public record GetAllCategoryAttributesQuery(string Search) : IRequest<ActionResponse<List<CategoryWithAttributesDto>>>
{
}
public class GetAllCategoryAttributesQueryHandler : IRequestHandler<GetAllCategoryAttributesQuery, ActionResponse<List<CategoryWithAttributesDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllCategoryAttributesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ActionResponse<List<CategoryWithAttributesDto>>> Handle(GetAllCategoryAttributesQuery request, CancellationToken cancellationToken)
    {
        var attributes = await _unitOfWork.CategoryAttributeRepository.GetAllCategoryAttributes(request.Search);
        return ActionResponse<List<CategoryWithAttributesDto>>.Success(attributes);
    }
}
