using DataStore.context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataStore.Interface
{
    public interface iDataStoreRepository
    {
        Task<bool> AddCustomerReferenceAsync(MemberDocumentStorage model);
        List<MemberDocumentStorage> GetReferencesByCustomer(int memberId);
        Task<bool> DeleteDocumentRecordByURIAsync(int memberId, string uri);

    }
}
