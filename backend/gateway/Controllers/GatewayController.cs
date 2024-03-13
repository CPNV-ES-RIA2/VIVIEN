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
        public async Task<List<ImageTag>> Analyze(IFormFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.FileName);
                string fileName = file.FileName + "_" + DateTime.Now.Ticks.ToString() + fileInfo.Extension;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
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
