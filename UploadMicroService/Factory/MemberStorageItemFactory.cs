using AutoMapper;
using DataStore.context;
using System.Collections.Generic;
using System.Linq;
using UploadMicroService.Interface;
using UploadMicroService.Model;

namespace UploadMicroService.Factory
{
    public class MemberStorageItemFactory : iMemberStorageFactory
    {
        private readonly MapperConfiguration _config;
        public MemberStorageItemFactory()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MemberStorageItem, MemberDocumentStorage>();
                cfg.CreateMap<MemberDocumentStorage, MemberStorageItem>();
            });
        }
        public MemberStorageItem Convert(MemberDocumentStorage entity)
        {
            var mapper = new Mapper(_config);
            return mapper.Map<MemberStorageItem>(entity);
        }
        public List<MemberStorageItem> Convert(List<MemberDocumentStorage> entityList)
        {
            return entityList.Select(item => Convert(item)).ToList();
        }
        public MemberDocumentStorage Convert(MemberStorageItem entity)
        {
            var mapper = new Mapper(_config);
            return mapper.Map<MemberDocumentStorage>(entity);
        }
        public List<MemberDocumentStorage> Convert(List<MemberStorageItem> itemList)
        {
            return itemList.Select(item => Convert(item)).ToList();
        }
    }
}
