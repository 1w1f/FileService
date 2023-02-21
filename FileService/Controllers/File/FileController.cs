using System.ComponentModel.DataAnnotations;
using DataModel.File;
using FileServiceApi.Service.File;
using FileServiceApi.Service.OssFileService.Interface;
using FileServiceRepsitory.Repository.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace FileService.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    protected IFileStoreService _fileService;
    private ILogger<FileController> _logger;
    public FileController(IFileStoreService fileService, ILogger<FileController> logger)
    {
        _fileService = fileService;
        _logger = logger;
    }

    [HttpGet("GetFileInfo")]
    public async Task<IActionResult> GetFileInfo([FromServices] IFileOperation fileOperation, string fileName)
    {
        await fileOperation.GetFileInfoAsync(fileName);
        return Ok();
    }

    /// <summary>
    /// 方法一：使用 asp.net core的模型绑定
    /// 缓冲方案上传文件 适用于小文件上传场景(该方案占用服务器内存)
    /// </summary>
    /// <param name="formFiles"></param>
    /// <returns></returns>
    [HttpPost("SaveFile")]
    [DisableRequestSizeLimit]
    public async Task<ActionResult> SaveFileAsync(IFormFile formFiles, [FromServices] IFileStoreService fileStoreService, [FromServices] IFileRepository fileRepository)
    {
        var fileInfo = await fileStoreService.SaveFileToOssServiceAsync(formFiles);
        var fileEntity = new FileEntity(formFiles.FileName, fileInfo.FileName, DateTime.Now, "/" + fileInfo.Bucket + "/" + fileInfo.FileName, fileInfo.FileSize);
        if (fileInfo is not null)
        {
            await fileRepository.CreateAsync(fileEntity);
            return Ok(fileInfo);
        }
        else
        {
            return StatusCode(500, "服务器错误");
        }
    }



    [HttpPost(nameof(SaveFilesStream))]
    [DisableRequestSizeLimit]
    public async Task<IActionResult> SaveFilesStream()
    {
        var request = HttpContext.Request;
        if (!request.HasFormContentType ||
                      !MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader) ||
                      string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
        {
            return new UnsupportedMediaTypeResult();
        }

        var fileInfoModel = await _fileService.GetFileInfoFromRequest(request, mediaTypeHeader);


        // If the code runs to this location, it means that no files have been saved
        return BadRequest("No files data in the request.");
    }

    public class SizeRange
    {
        [Required]
        public int Min { get; set; }
        public int Max { get; set; }

        [Required]
        public string name { get; set; }
    }



    [HttpPost(nameof(GetFileWithRangeSize))]
    public async Task<IActionResult> GetFileWithRangeSize([FromServices] IFileRepository fileRepository, SizeRange sizeRange)
    {
        var files = await fileRepository.FindAllAsync();
        var list = files.Where(item => item.FileSize > sizeRange.Min && item.FileSize < sizeRange.Max).ToList();
        return Ok();
    }
}