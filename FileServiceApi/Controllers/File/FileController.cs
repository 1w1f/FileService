using FileServiceApi.Common;
using FileServiceApi.Service.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace FileServiceApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        protected IFileStoreService _fileService;
        public FileController(IFileStoreService fileService)
        {
            _fileService = fileService;
        }


        [HttpGet(nameof(Hello))]
        public IActionResult Hello()
        {
            throw new BusinessException(5001, "业务异常");
            return Ok();
        }
        /// <summary>
        /// 方法一：使用 asp.net core的模型绑定
        /// 缓冲方案上传文件 适用于小文件上传场景(该方案占用服务器内存)
        /// </summary>
        /// <param name="formFiles">表单文件</param>
        /// <returns></returns>
        [HttpPost(nameof(SaveFile))]
        [DisableRequestSizeLimit]
        public IActionResult SaveFile(IFormFile formFiles)
        {
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