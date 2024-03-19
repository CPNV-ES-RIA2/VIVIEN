using Azure;
using Azure.AI.Vision.Common;
using Azure.AI.Vision.ImageAnalysis;

namespace LabelDetector
{
    public class AzureLabelDetector : ILabelDetector
    {
        private VisionServiceOptions options;
        public AzureLabelDetector()
        {
            this.options = new VisionServiceOptions(Environment.GetEnvironmentVariable("VISION_ENDPOINT"),
                new AzureKeyCredential(Environment.GetEnvironmentVariable("VISION_KEY")));
        }

        public async Task<List<ImageTag>> Analyze(Uri uri, int maxLabelCount = 10, float precision = 0.9f)
        {
            if (Uri.IsWellFormedUriString(uri.OriginalString, UriKind.Absolute))
            {
                return await AnalyzeImage(VisionSource.FromUrl(uri), maxLabelCount, precision);
            }
            else
            {
                return await AnalyzeImage(VisionSource.FromFile(uri.LocalPath), maxLabelCount, precision);
            }
        }
        private async Task<List<ImageTag>> AnalyzeImage(VisionSource imageSource, int maxLabelCount = 10, float precision = 0.9f)
        {
            ImageAnalysisOptions imageAnalysisOptions = new ImageAnalysisOptions()
            {
                Features = ImageAnalysisFeature.Tags,
                Language = "en"
            };

            using ImageAnalyzer analyzer = new ImageAnalyzer(options, imageSource, imageAnalysisOptions);

            ImageAnalysisResult result = analyzer.Analyze();

            List<ImageTag> tags = new List<ImageTag>();

            if (result.Reason == ImageAnalysisResultReason.Analyzed)
            {
                if (result.Tags != null)
                {
                    tags = result.Tags.Select(tag => new ImageTag(tag.Name, tag.Confidence)).ToList();
                    if (precision > 0)
                    {
                        tags = tags.FindAll((tag) => tag.Confidence >= precision);
                    }
                    if (maxLabelCount > 0)
                    {
                        tags = tags.Take(maxLabelCount).ToList();
                    }
                }
            }
            return tags;
        }
    }
}