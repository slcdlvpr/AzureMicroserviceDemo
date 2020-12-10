using System;

namespace UploadMicroService.Model
{
    public class MemberStorageItem
    {
        public int? Id { get; set; }
        public int MemberId { get; set; }
        public string FileUri { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}