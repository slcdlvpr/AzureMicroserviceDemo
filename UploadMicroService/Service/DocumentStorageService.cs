using BlobStorage.Interface;
using DataStore.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using UploadMicroService.Interface;
using UploadMicroService.Model;

namespace UploadMicroService.Service
{
    public class DocumentStorageService : iDocumentStorageService
    {
        private readonly iDataStoreRepository _dataStoreRepository;
        private readonly iBlobClient _blobClient;
        private readonly iMemberStorageFactory _factory;
        public DocumentStorageService(iDataStoreRepository dataStoreRepository, iBlobClient blobClient,  iMemberStorageFactory factory)
        {
            _dataStoreRepository = dataStoreRepository;
            _blobClient = blobClient;
            _factory = factory;
        }
        public async Task<bool> AddCustomerReference(MemberStorageItem item, string fileName, byte[] fileData)
        {
            var uploadTask = await _blobClient.UploadFileToBlobAsync(fileName, fileData);
            item.FileUri = uploadTask;
            await _dataStoreRepository.AddCustomerReferenceAsync(_factory.Convert(item));
            return true;
        }
        public List<MemberStorageItem> GetReferencesByCustomer(int memberId)
        {
            return _factory.Convert(_dataStoreRepository.GetReferencesByCustomer(memberId));
        }
        public async Task<bool> DeleteBlobFromStorage(int memberId, string uri)
        {
            _blobClient.DeleteBlobDataAsync(uri);
            await _dataStoreRepository.DeleteDocumentRecordByURIAsync(memberId, uri);
            return true;
        }
        public async Task<bool> MoveDocumentToArchiveStoragePlan(string uri)
        {
            await _blobClient.UpdateBlobStorageAccessSettingsArchive(uri);
            return true;
        }
    }
}
