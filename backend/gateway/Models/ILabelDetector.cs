namespace gateway.Models
{
    public interface ILabelDetector
    {
        public Task<List<ImageTag>> Analyze(Uri uri, int labelCount = 10, float precision = 0.9f);
    }
}
