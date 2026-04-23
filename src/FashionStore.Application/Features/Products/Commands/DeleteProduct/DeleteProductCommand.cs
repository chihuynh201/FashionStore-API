using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using MediatR;

namespace FashionStore.Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int ProductId) : ICommand;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
        if (product == null || product.IsDeleted)
        {
            return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "Product not found.");
        }

        product.IsDeleted = true;
        _unitOfWork.ProductRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActionResponse.Success();
    }
}
