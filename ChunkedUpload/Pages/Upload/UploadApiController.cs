using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChunkedUpload.Pages.Upload
{
    [Route("/api/Upload")]
    [ApiController]
    public class UploadApiController : ControllerBase
    {
        [BindProperty]
        public IFormFile? UploadFile { get; set; }
        [BindProperty]
        public string? FileName { get; set; }
        [BindProperty]
        public int ChunkNumber { get; set; }
        [BindProperty]
        public int TotalChunks { get; set; }

        private readonly ILogger<UploadApiController> _logger;

        public UploadApiController(ILogger<UploadApiController> logger)
        {
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [RequestSizeLimit(90*1024*1024)]
        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation($"Upload of {FileName} chunk {ChunkNumber} started.");
            try
            {
                //Null Checks
                if (UploadFile == null) return new JsonResult(new { error = "No file uploaded" });
                if (string.IsNullOrEmpty(FileName)) return new JsonResult(new { error = "No file name" });

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FileName);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, FileName);
                using (var stream = new FileStream(filePath, ChunkNumber == 0 ? FileMode.Create : FileMode.Append))
                {
                    await UploadFile.CopyToAsync(stream);
                }

                return new JsonResult(new { chunkNumber = ChunkNumber, totalChunks = TotalChunks });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message });
            }
        }
    }
}
