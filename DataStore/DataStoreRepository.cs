using DataStore.context;
using DataStore.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataStore
{
    public class DataStoreRepository : iDataStoreRepository
    {
        private readonly string _connectionString;
        public DataStoreRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<bool> AddCustomerReferenceAsync(MemberDocumentStorage document)
        {
            try
            {
                DocumentstorageContext context = new DocumentstorageContext(_connectionString);
                context.Add(document);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                //add logging
                throw;
            }
        }
        public List<MemberDocumentStorage> GetReferencesByCustomer(int memberId)
        {
            try
            {
                DocumentstorageContext context = new DocumentstorageContext(_connectionString);
                return context.MemberDocumentStorage.Where(x => x.MemberId == memberId).ToList();
            }
            catch
            {
                //add logging
                throw;
            }
        }
        public async Task<bool> DeleteDocumentRecordByURIAsync(int memberId, string uri)
        {
            try
            {
                DocumentstorageContext context = new DocumentstorageContext(_connectionString);
                var entity =
                context.MemberDocumentStorage.FirstOrDefault(x => x.MemberId == memberId && x.FileUri == uri);
                context.MemberDocumentStorage.Remove(entity);
                await context.SaveChangesAsync();
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

