using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using Mapster;
using System.Text.Json.Serialization;

namespace FashionStore.Application.Features.ProductAttributes.Commands.UpdateAttribute;

public record UpdateProductAttributeCommand : ICommand
{
    [JsonIgnore]
    public int ProductAttributeId { get; set; }
    public string AttributeName { get; init; }
}

public class UpdateProductAttributeCommandHandler : ICommandHandler<UpdateProductAttributeCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductAttributeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse> Handle(UpdateProductAttributeCommand request, CancellationToken cancellationToken)
    {
        var attribute = await _unitOfWork.Repository<ProductAttribute>().GetByIdAsync(request.ProductAttributeId);
        if (attribute == null)
        {
            return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "Product attribute not found.");
        }
        var existingAttribute = await _unitOfWork.Repository<ProductAttribute>()
            .GetFirstOrDefaultAsync(attr => attr.AttributeName == request.AttributeName && attr.ProductAttributeId != request.ProductAttributeId);
        if (existingAttribute != null)
        {
            return ActionResponse.CreateResponse(ResponseCode.BadRequest, message: "Attribute name already exists.");
        }

        request.Adapt(attribute);

        _unitOfWork.Repository<ProductAttribute>().Update(attribute);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActionResponse.Success();
    }
}
