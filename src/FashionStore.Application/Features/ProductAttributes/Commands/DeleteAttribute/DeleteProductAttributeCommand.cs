using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;

namespace FashionStore.Application.Features.ProductAttributes.Commands.DeleteAttribute;

public record DeleteProductAttributeCommand(int ProductAttributeId) : ICommand;

public class DeleteProductAttributeCommandHandler : ICommandHandler<DeleteProductAttributeCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductAttributeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse> Handle(DeleteProductAttributeCommand request, CancellationToken cancellationToken)
    {
        var attribute = await _unitOfWork.Repository<ProductAttribute>().GetByIdAsync(request.ProductAttributeId);
        if (attribute == null)
        {
            return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "Product attribute not found.");
        }

        _unitOfWork.Repository<ProductAttribute>().Delete(attribute);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActionResponse.Success();
    }
}
