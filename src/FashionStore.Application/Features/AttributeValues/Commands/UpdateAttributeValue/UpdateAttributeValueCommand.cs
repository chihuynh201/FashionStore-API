using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using System.Text.Json.Serialization;

namespace FashionStore.Application.Features.AttributeValues.Commands.UpdateAttributeValue;

public record UpdateAttributeValueCommand : ICommand
{
    [JsonIgnore]
    public int ProductAttributeId { get; set; }
    public List<AttributeValueUpdateDto> AttributeValues { get; init; }

    public class AttributeValueUpdateDto
    {
        public int AttributeValueId { get; init; }
        public string Value { get; init; }
        public int DisplayOrder { get; init; }
    }
}

public class UpdateAttributeValueCommandHandler : ICommandHandler<UpdateAttributeValueCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAttributeValueCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse> Handle(UpdateAttributeValueCommand request, CancellationToken cancellationToken)
    {
        var existingAttribute = await _unitOfWork.ProductAttributeRepository.GetByIdAsync(request.ProductAttributeId);
        if (existingAttribute == null)
        {
            return ActionResponse.CreateResponse(ResponseCode.NotFound, message: "Product attribute not found.");
        }

        if (request.AttributeValues.GroupBy(x => x.Value.ToLower()).Any(g => g.Count() > 1))
        {
            return ActionResponse.CreateResponse(ResponseCode.BadRequest, message: "Duplicate attribute values.");
        }
        var currentValues = await _unitOfWork.AttributeValueRepository.GetAllAsync(x => x.ProductAttributeId == request.ProductAttributeId, tracking: true);

        var valuesToDelete = currentValues.Where(x => !request.AttributeValues.Any(rv => rv.AttributeValueId == x.AttributeValueId));
        if (valuesToDelete.Any())
        {
            _unitOfWork.AttributeValueRepository.DeleteRange(valuesToDelete);
        }

        foreach (var reqVal in request.AttributeValues)
        {
            var existingVal = currentValues.FirstOrDefault(x => x.AttributeValueId == reqVal.AttributeValueId);
            if (existingVal != null)
            {
                existingVal.Value = reqVal.Value;
                existingVal.DisplayOrder = reqVal.DisplayOrder;
                _unitOfWork.AttributeValueRepository.Update(existingVal);
            }
            else
            {
                var newVal = new AttributeValue
                {
                    Value = reqVal.Value,
                    DisplayOrder = reqVal.DisplayOrder,
                    ProductAttributeId = request.ProductAttributeId
                };
                await _unitOfWork.AttributeValueRepository.AddAsync(newVal);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActionResponse.Success();
    }
}
