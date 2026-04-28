using FashionStore.Application.Common.DTOs.Response;
using FashionStore.Application.Common.Enums;
using FashionStore.Application.Common.Interfaces.IServices;
using FashionStore.Application.Interfaces;
using FashionStore.Domain.Entities;
using FashionStore.Domain.Enums;

namespace FashionStore.Application.Features.Files.Command;

public record CompleteUploadCommand(string BlobName) : ICommand<int>
{
}

public class CompleteUploadCommandHandler : ICommandHandler<CompleteUploadCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;
    public CompleteUploadCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }
    public async Task<ActionResponse<int>> Handle(CompleteUploadCommand request, CancellationToken cancellationToken)
    {
        var fileInfo = await _fileStorageService.GetFileInfoAsync(request.BlobName);
        if (fileInfo == null)
            return ActionResponse<int>.CreateResponse(ResponseCode.BadRequest, 0, "File information could not be retrieved");

        var file = await _unitOfWork.Repository<FileUpload>().GetFirstOrDefaultAsync(f => f.FileName == request.BlobName, true);
        if (file == null)
            return ActionResponse<int>.CreateResponse(ResponseCode.BadRequest, 0, "File not found");

        file.Status = FileStatus.Uploaded.ToString();
        file.Size = fileInfo.Size;
        file.ContentType = fileInfo.ContentType;

        await _unitOfWork.SaveChangesAsync();
        return ActionResponse<int>.Success(file.FileId);
    }

}