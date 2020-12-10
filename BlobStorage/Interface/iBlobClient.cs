using System.Threading.Tasks;

namespace BlobStorage.Interface
{
    public interface iBlobClient
    {
        string UploadFileToBlob(string strFileName, byte[] fileData);
        Task<bool> DeleteBlobDataAsync(string fileUrl);
        Task<string> UploadFileToBlobAsync(string strFileName, byte[] fileData);
        Task<bool> UpdateBlobStorageAccessSettingsArchive(string url);
    }
}

