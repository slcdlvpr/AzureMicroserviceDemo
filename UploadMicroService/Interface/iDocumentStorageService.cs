using System.Collections.Generic;
using System.Threading.Tasks;
using UploadMicroService.Model;

namespace UploadMicroService.Interface
{
    public interface iDocumentStorageService
    {
        Task<bool> AddCustomerReference(MemberStorageItem model, string fileName, byte[] fileData);
        List<MemberStorageItem> GetReferencesByCustomer(int memberId);
        Task<bool> MoveDocumentToArchiveStoragePlan(string url);
        Task<bool> DeleteBlobFromStorage(int memberId, string uri);

    }
}
