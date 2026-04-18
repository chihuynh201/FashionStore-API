using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using Mapster;
using MediatR;

namespace FashionStore.Application.Features.AttributeValues.Queries;

public record GetAttributeValueListQuery(int ProductAttributeId) : IRequest<ActionResponse<List<AttributeValueDto>>>;

public class GetAttributeValueListQueryHandler : IRequestHandler<GetAttributeValueListQuery, ActionResponse<List<AttributeValueDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAttributeValueListQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<List<AttributeValueDto>>> Handle(GetAttributeValueListQuery request, CancellationToken cancellationToken)
    {
        var values = await _unitOfWork.Repository<AttributeValue>().GetAllAsync(x => x.ProductAttributeId == request.ProductAttributeId);

        return ActionResponse<List<AttributeValueDto>>.Success(values.Adapt<List<AttributeValueDto>>());
    }
}
