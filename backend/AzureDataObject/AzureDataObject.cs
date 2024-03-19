using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace DataObject
{
    public class AzureDataObject : IDataObject
    {
        private BlobContainerClient blobContainerClient;
        private Uri bucketName;
        private string objectName;

        public AzureDataObject(Uri bucketName, string connectionStringEnvrionmentVariable = "AZURE_DATA_STORAGE_CONNECTION_STRING")
        {

            blobContainerClient = new BlobContainerClient(Environment.GetEnvironmentVariable(connectionStringEnvrionmentVariable), bucketName.Host.Split('.')[0]);
            this.bucketName = bucketName;
        }
        public async Task<bool> DoesExist(Uri remoteFullPath)
        {
            if (IsBucket(remoteFullPath))
            {
                return blobContainerClient.Exists();
            }
            return blobContainerClient.GetBlobClient(remoteFullPath.AbsolutePath).Exists();
        }

        public async Task<byte[]> Download(Uri remoteFullPath, Uri localFullPath)
        {
            if (await DoesExist(remoteFullPath))
            {
                if (File.Exists(localFullPath.AbsolutePath))
                {
                    File.Delete(localFullPath.AbsolutePath);
                }
                using (FileStream file = File.Create(localFullPath.AbsolutePath))
                {
                    await blobContainerClient.GetBlobClient(remoteFullPath.AbsolutePath).DownloadToAsync(file);
                    using (MemoryStream returnStream = new MemoryStream())
                    {
                        file.CopyTo(returnStream);
                        return returnStream.ToArray();
                    }
                }
            }
            else
            {
                throw new ObjectNotFoundException(remoteFullPath.ToString());
            }
        }

        public async Task<Uri> Publish(Uri remoteFullPath, int expirationTime = 90)
        {
            if (await DoesExist(remoteFullPath))
            {
                return blobContainerClient.GetBlobClient(remoteFullPath.AbsolutePath).GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddSeconds(expirationTime));
            }
            else
            {
                throw new ObjectNotFoundException(remoteFullPath.ToString());
            }
        }

        public async Task Remove(Uri remoteFullPath, bool recursive = false)
        {
            if (recursive)
            {
                foreach (BlobItem blobItem in blobContainerClient.GetBlobs(prefix: remoteFullPath.AbsolutePath.Remove(0, 1)))
                {
                    blobContainerClient.GetBlobClient(blobItem.Name).Delete();
                }
            }
            else
            {
                blobContainerClient.GetBlobClient(remoteFullPath.AbsolutePath).Delete();
            }
        }

        public async Task Upload(Uri localFullPath, Uri remoteFullPath)
        {
            if (!await DoesExist(remoteFullPath) && File.Exists(localFullPath.LocalPath))
            {
                CreateObject(File.ReadAllBytes(localFullPath.LocalPath), remoteFullPath);
            }
            else
            {
                throw new ObjectAlreadyExistsException(remoteFullPath.ToString());
            }
        }

        private async void CreateObject(byte[] data, Uri remoteFullPath)
        {
            blobContainerClient.GetBlobClient(remoteFullPath.AbsolutePath).Upload(new BinaryData(data));
        }

        private bool IsBucket(Uri uri)
        {
            return Uri.Compare(blobContainerClient.Uri, uri, UriComponents.Host | UriComponents.PathAndQuery, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }

    public class DataObjectException : Exception
    {
        public DataObjectException()
        {
        }

        public DataObjectException(string message) : base(message)
        {
        }
    }

    public class ObjectAlreadyExistsException : DataObjectException
    {
        public ObjectAlreadyExistsException(string message) : base(message)
        {
        }
    }

    public class ObjectNotFoundException : DataObjectException
    {
        public ObjectNotFoundException(string message) : base(message)
        {
        }
    }

    public class NotEmptyObjectException : DataObjectException
    {
        public NotEmptyObjectException()
        {
        }
    }
}