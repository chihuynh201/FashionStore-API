using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using Mapster;

namespace FashionStore.Application.Features.ProductAttributes.Commands.CreateAttribute;

public record CreateProductAttributeCommand : ICommand<int>
{
    public string AttributeName { get; init; }
}

public class CreateProductAttributeCommandHandler : ICommandHandler<CreateProductAttributeCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductAttributeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<int>> Handle(CreateProductAttributeCommand request, CancellationToken cancellationToken)
    {
        var existingAttribute = await _unitOfWork.Repository<ProductAttribute>()
            .GetFirstOrDefaultAsync(attr => attr.AttributeName == request.AttributeName);
        if (existingAttribute != null)
        {
            return ActionResponse<int>.CreateResponse(ResponseCode.BadRequest, message: "Attribute name already exists.");
        }

        var attribute = request.Adapt<ProductAttribute>();

        await _unitOfWork.Repository<ProductAttribute>().AddAsync(attribute);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActionResponse<int>.Success(attribute.ProductAttributeId);
    }
}
