using FashionStore.Application.Features.Files.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.API.Controllers;
[ApiController]
public class FileController : StoreBaseController
{
    private readonly IMediator _mediator;

    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("presigned-upload")]
    public async Task<IActionResult> GenerateUploadUrl(GenerateUploadUrlCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("complete-upload")]
    public async Task<IActionResult> CompleteUpload(CompleteUploadCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
