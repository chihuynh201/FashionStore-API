using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using Mapster;
using MediatR;

namespace FashionStore.Application.Features.ProductAttributes.Queries;

public record GetProductAttributeByIdQuery(int ProductAttributeId) : IRequest<ActionResponse<ProductAttributeDto>>;

public class GetProductAttributeByIdQueryHandler : IRequestHandler<GetProductAttributeByIdQuery, ActionResponse<ProductAttributeDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductAttributeByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<ProductAttributeDto>> Handle(GetProductAttributeByIdQuery request, CancellationToken cancellationToken)
    {
        var attribute = await _unitOfWork.ProductAttributeRepository.GetByIdWithValuesAsync(request.ProductAttributeId);
        if (attribute == null)
        {
            return ActionResponse<ProductAttributeDto>.CreateResponse(ResponseCode.NotFound, message: "Product attribute not found.");
        }

        return ActionResponse<ProductAttributeDto>.Success(attribute.Adapt<ProductAttributeDto>());
    }
}
