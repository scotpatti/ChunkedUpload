using ChunkedUpload.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChunkedUpload.Pages.Upload
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly JwtTokenService _jwtTokenService;

        public string Token { get; set; }
        public string UploadUrl { get; set; }

        public IndexModel(ILogger<IndexModel> logger, JwtTokenService jwtTokenService)
        {
            _logger = logger;
            _jwtTokenService = jwtTokenService;
            //Token is only set on a GET request. It should be used on a Post
            //request to /Upload/Upload. Token is good for 30 minutes. If you
            //need longer change the value in JwtTokenService.
            Token = "";
            //UploadUrl can only be set in the OnGet method. 
            UploadUrl = "";
        }

        public void OnGet()
        {
            Token = _jwtTokenService.GenerateJwtToken(User);
            //Endpoint to use for API call {https}://{localhost:7012}/Upload/Upload
            UploadUrl = $"{Request.Scheme}://{Request.Host.Value}/api/Upload";
        }
    }
}
