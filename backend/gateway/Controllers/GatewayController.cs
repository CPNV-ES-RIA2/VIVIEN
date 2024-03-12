using gateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace gateway.Controllers
{
    [ApiController]
    [Route("Analyze")]
    public class GatewayController : ControllerBase
    {
        private static ILabelDetector labelDetector = new LabelDetector();
        public GatewayController() { }

        [HttpPost(Name = "Analyze")]
        public async Task<List<ImageTag>> Analyze(IFormFile formFile)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(formFile.FileName);
                string fileName = formFile.FileName + "_" + DateTime.Now.Ticks.ToString() + fileInfo.Extension;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
                return await labelDetector.Analyze(new Uri(filePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
