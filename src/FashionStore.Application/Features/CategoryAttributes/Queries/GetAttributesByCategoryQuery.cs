using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Interfaces;
using MediatR;

namespace FashionStore.Application.Features.CategoryAttributes.Queries;

public record GetAttributesByCategoryQuery(int CategoryId) : IRequest<ActionResponse<List<CategoryAttributeDto>>>;

public class GetAttributesByCategoryQueryHandler : IRequestHandler<GetAttributesByCategoryQuery, ActionResponse<List<CategoryAttributeDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAttributesByCategoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<List<CategoryAttributeDto>>> Handle(GetAttributesByCategoryQuery request, CancellationToken cancellationToken)
    {
        var attributes = await _unitOfWork.CategoryAttributeRepository.GetByCategoryIdAsync(request.CategoryId);
        return ActionResponse<List<CategoryAttributeDto>>.Success(attributes);
    }
}
