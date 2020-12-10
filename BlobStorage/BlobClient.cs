using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using BlobStorage.Interface;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlobStorage
{

    public class BlobClient : iBlobClient
    {
        private readonly string _accessKey;
        private readonly string _containerName;
        public BlobClient(string accesskey, string containerName)
        {
            _accessKey = accesskey;
            _containerName = containerName;
        }
        public string UploadFileToBlob(string strFileName, byte[] fileData)
        {
            try
            {
                var _task = Task.Run(() => this.UploadFileToBlobAsync(strFileName, fileData));
                _task.Wait();
                string fileUrl = _task.Result;
                return fileUrl;
            }
            catch
            {
                //add logging
                throw;
            }
        }
        public async Task<bool> DeleteBlobDataAsync(string fileUrl)
        {
            try
            {
                Uri uriObj = new Uri(fileUrl);
                string BlobName = Path.GetFileName(uriObj.LocalPath);

                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_accessKey);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_containerName);

                string pathPrefix = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/";
                CloudBlobDirectory blobDirectory = cloudBlobContainer.GetDirectoryReference(pathPrefix);
                CloudBlockBlob blockBlob = blobDirectory.GetBlockBlobReference(BlobName);

                await blockBlob.DeleteAsync();
                return true;
            }
            catch
            {
                //add logging
                throw;
            }
        }
        private string GenerateFileName(string fileName)
        {
            string strFileName = string.Empty;
            string[] strName = fileName.Split('.');
            strFileName = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }
        public async Task<string> UploadFileToBlobAsync(string strFileName, byte[] fileData)
        {
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_accessKey);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_containerName);
                string fileName = this.GenerateFileName(strFileName);
                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                }
                if (fileName != null && fileData != null)
                {
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
                    return cloudBlockBlob.Uri.AbsoluteUri;
                }
                throw new InvalidDataException();
            }
            catch
            {
                //add logging
                throw;
            }
        }
        public async Task<bool> UpdateBlobStorageAccessSettingsArchive(string url)
        {
            try
            {
                BlobServiceClient cloudBlobServiceClient = new BlobServiceClient(_accessKey);
                // Set the access tier 
                BlobBatchClient batch = cloudBlobServiceClient.GetBlobBatchClient();
                await batch.SetBlobsAccessTierAsync(new Uri[] { new Uri(url)
                }, AccessTier.Archive);
                return true;
            }
            catch
            {
                //add logging
                throw;
            }
        }
    }
}






