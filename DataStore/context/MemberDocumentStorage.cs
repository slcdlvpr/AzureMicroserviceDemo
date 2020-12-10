using System;

namespace DataStore.context
{
    public partial class MemberDocumentStorage
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string FileUri { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
