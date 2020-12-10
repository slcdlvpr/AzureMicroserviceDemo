using BlobStorage.Interface;
using DataStore.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using UploadMicroService.Interface;
using UploadMicroService.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UploadMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentStorageController : ControllerBase
    {
        private readonly iDocumentStorageService _documentStorageService;
        private readonly iDataStoreRepository _dataStoreRepository;
        private readonly iBlobClient _blobClient;
        private readonly iMemberStorageFactory _memberStorageFactory;

        public DocumentStorageController(iDocumentStorageService documentStorageService, iDataStoreRepository dataStoreRepository,
            iBlobClient blobClient,
            iMemberStorageFactory factory)
        {
            _documentStorageService = documentStorageService;
            _dataStoreRepository = dataStoreRepository;
            _blobClient = blobClient;
            _memberStorageFactory = factory;
        }
        [HttpGet("{memberid}")]
        public IActionResult Get(int memberid)
        {
            return Ok(_documentStorageService.GetReferencesByCustomer(memberid));
        }
        [HttpPost]
        // POST api/<DocumentStorage>
        public async Task<IActionResult> PostAsync(IFormFile formFile, [FromForm] MemberStorageItem item)
        {
            if (formFile.Length > 0)
            {
                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            string mimeType = formFile.ContentType;
            byte[] fileData = new byte[formFile.Length];
            await _documentStorageService.AddCustomerReference(item, formFile.FileName, fileData);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDocumentAsync(int memberId, string bloburi)
        {
            await _documentStorageService.DeleteBlobFromStorage(memberId, bloburi);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> PutDocumentInArchiveAsync(string bloburi)
        {
            await _documentStorageService.MoveDocumentToArchiveStoragePlan(bloburi);
            return Ok();
        }
    }
}
