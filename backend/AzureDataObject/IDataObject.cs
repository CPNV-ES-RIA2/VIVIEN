namespace DataObject
{
    public interface IDataObject
    {
        public Task<bool> DoesExist(Uri remoteFullPath);
        public Task Upload(Uri localFullPath, Uri remoteFullPath);
        public Task<byte[]> Download(Uri remoteFullPath, Uri localFullPath);
        public Task<Uri> Publish(Uri remoteFullPath, int expirationTime = 90);
        public Task Remove(Uri remoteFullPath, bool recursive = false);
    }
}