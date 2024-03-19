using Microsoft.AspNetCore.Mvc;
using DataObject;
using LabelDetector;

namespace gateway.Controllers
{
    [ApiController]
    [Route("Analyze")]
    public class GatewayController : ControllerBase
    {
        private static ILabelDetector labelDetector = new AzureLabelDetector();
        private static Uri bucketUri = new Uri(Environment.GetEnvironmentVariable("BUCKET_URI"));
        private static IDataObject dataObject = new AzureDataObject(bucketUri);
        public GatewayController() { }

        [HttpPost(Name = "Analyze")]
        public async Task<List<ImageTag>> Analyze(IFormFile file, int maxLabelCount = 10, float minConfidence = 0.9f)
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
                Uri remoteObjectPath = new Uri(bucketUri.AbsoluteUri + $"/{fileName}");
                await dataObject.Upload(new Uri(filePath), remoteObjectPath);
                Uri remoteObjectPathPublish = await dataObject.Publish(remoteObjectPath);
                System.IO.File.Delete(filePath);
                return await labelDetector.Analyze(remoteObjectPathPublish, maxLabelCount, minConfidence);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
