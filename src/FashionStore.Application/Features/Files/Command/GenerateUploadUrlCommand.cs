using FashionStore.Application.Common.DTOs;
using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Interfaces.IServices;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using FashionStore.Domain.Enums;

namespace FashionStore.Application.Features.Files.Command;
public record GenerateUploadUrlCommand(string FileName, string ContentType) : ICommand<PresignedUploadUrlDto>
{

}

public class GenerateUploadUrlCommandHandler : ICommandHandler<GenerateUploadUrlCommand, PresignedUploadUrlDto>
{
    private readonly IFileStorageService _fileService;
    private readonly IUnitOfWork _unitOfWork;
    public GenerateUploadUrlCommandHandler(IFileStorageService fileService, IUnitOfWork unitOfWork)
    {
        _fileService = fileService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResponse<PresignedUploadUrlDto>> Handle(GenerateUploadUrlCommand request, CancellationToken cancellationToken)
    {
        var result = _fileService.GenerateUploadUrlAsync(request.FileName, request.ContentType);
        var file = new FileUpload
        {
            FileName = result.BlobName,
            Url = result.FileUrl,
            Status = FileStatus.Uploading.ToString(),
        };

        await _unitOfWork.Repository<FileUpload>().AddAsync(file);
        await _unitOfWork.SaveChangesAsync();

        return ActionResponse<PresignedUploadUrlDto>.Success(result);
    }

}