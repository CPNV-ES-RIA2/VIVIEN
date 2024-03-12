namespace gateway.Models
{
    public class ImageTag
    {
        public string Name { get; set; }
        public double Confidence { get; set; }
        public ImageTag(string name, double confidence)
        {
            Name = name;
            Confidence = confidence;
        }
    }
}
