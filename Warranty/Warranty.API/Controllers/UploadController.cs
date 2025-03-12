using Microsoft.AspNetCore.Mvc;
   using Amazon.S3;
    using Amazon.S3.Model;


namespace Warranty.API.Controllers
{
    //[ApiController]
    //[Route("api/upload")]
    //public class UploadController : ControllerBase
    //{
    //    private readonly IAmazonS3 _s3Client;

    //    public UploadController(IAmazonS3 s3Client)
    //    {
    //        _s3Client = s3Client;
    //    }

    //    [HttpGet("presigned-url")]
    //    public async Task<IActionResult> GetPresignedUrl([FromQuery] string fileName)
    //    {
    //        var request = new GetPreSignedUrlRequest
    //        {
    //            BucketName = "files-warranty",
    //            Key = fileName,
    //            Verb = HttpVerb.PUT,
    //            Expires = DateTime.UtcNow.AddMinutes(5),
    //            ContentType = "image/jpeg/pdf/png" // או סוג הקובץ המתאים
    //        };

    //        string url = _s3Client.GetPreSignedURL(request);
    //        return Ok(new { url });
    //    }
    //}
    [ApiController]
    [Route("api/upload")]
    public class UploadController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;
        private readonly HttpClient _httpClient;

        public UploadController(IAmazonS3 s3Client, HttpClient httpClient)
        {
            _s3Client = s3Client;
            _httpClient = httpClient;
        }
       
        // פעולה להחזיר את הקישור המותנה להעלאת קובץ
        [HttpGet("presigned-url")]
        public async Task<IActionResult> GetPresignedUrl([FromQuery] string fileName)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = "files-warranty",
                Key = fileName,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(5),
                ContentType = "application/octet-stream" // סוג הקובץ הכללי
            };

            string url = _s3Client.GetPreSignedURL(request);
            return Ok(new { url });
        }

        // פעולה להעלות את הקובץ ל-S3 ואז לשלוח אותו ל-AI
        [HttpPost("process-file")]
        public async Task<IActionResult> ProcessFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            
            // 2. שליחת הקובץ ל-AI לאחר העלאה ל-S3
            var aiRequest = new MultipartFormDataContent();
            aiRequest.Add(new StringContent(url), "fileUrl");

            var aiResponse = await _httpClient.PostAsync("AI_URL", aiRequest);
            if (!aiResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)aiResponse.StatusCode, "Error processing file with AI.");
            }

            var aiResult = await aiResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject(aiResult);

            return Ok(result);
        }
    }
}
