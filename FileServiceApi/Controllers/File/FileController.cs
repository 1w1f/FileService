using FileServiceApi.Common;
using FileServiceApi.Service.File;
using FileServiceApi.Service.OssFileService;
using FileServiceApi.Service.OssFileService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Minio;

namespace FileServiceApi.Controllers
{
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


        /// <summary>
        /// 方法一：使用 asp.net core的模型绑定
        /// formOptions中设置了默认的请求大小限制为128M
        /// 缓冲方案上传文件 适用于小文件上传场景(该方案占用服务器内存)
        /// </summary>
        /// <param name="formFiles">表单文件</param>
        /// <returns></returns>
        [HttpPost("SaveFile")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult> SaveFileAsync([FromForm] IFormFile formFiles, [FromServices] IFileOperation fileOperation)
        {
            await fileOperation.UploadFormFileAsync(formFiles);
            return Ok();
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


    }
}