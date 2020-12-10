using DataStore.context;
using System.Collections.Generic;
using UploadMicroService.Model;

namespace UploadMicroService.Interface
{
    public interface iMemberStorageFactory
    {
        MemberStorageItem Convert(MemberDocumentStorage entity);
        List<MemberStorageItem> Convert(List<MemberDocumentStorage> entityList);
        MemberDocumentStorage Convert(MemberStorageItem entity);
        List<MemberDocumentStorage> Convert(List<MemberStorageItem> entityList);

    }
}
