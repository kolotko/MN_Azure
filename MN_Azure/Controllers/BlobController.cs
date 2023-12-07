using Microsoft.AspNetCore.Mvc;
using MN_Azure.Abstractions;
using MN_Azure.Models;

namespace MN_Azure.Controllers;

[ApiController]
[Route("[controller]")]
public class BlobController : ControllerBase
{
    private readonly IBlobServices _blobServices;

    public BlobController(IBlobServices blobServices)
    {
        _blobServices = blobServices;
    }
    
    
    [HttpGet("{blobName}")]
    public async Task<IActionResult> GetBlob(string blobName)
    {
        var data = await _blobServices.GetBlobAsync(blobName);
        return File(data.Content, data.ContentType);
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> ListBlobs()
    {
        var response = await _blobServices.ListBlobAsync();
        return Ok(response);
    }
    
    [HttpPost("uploadfile")]
    public async Task<IActionResult> UploadBlobs(UploadFileRequest request)
    {
        await _blobServices.UploadFileBlobAsync(request.FilePath, request.FileName);
        return Ok();
    }
    
    [HttpPost("uploadcontent")]
    public async Task<IActionResult> UploadContent(UploadContentRequest request)
    {
        await _blobServices.UploadContentBlobAsync(request.Content, request.FileName);
        return Ok();
    }
    
    [HttpDelete("blobName")]
    public async Task<IActionResult> DeleteFile(string blobName)
    {
        await _blobServices.DeleteBlobAsync(blobName);
        return Ok();
    }
}