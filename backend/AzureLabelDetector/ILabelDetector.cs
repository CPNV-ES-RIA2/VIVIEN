namespace LabelDetector
{
    public interface ILabelDetector
    {
        public Task<List<ImageTag>> Analyze(Uri uri, int maxLabelCount = 10, float precision = 0.9f);
    }
}