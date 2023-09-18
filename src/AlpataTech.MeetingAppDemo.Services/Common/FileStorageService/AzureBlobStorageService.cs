using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpataTech.MeetingAppDemo.Services.Common.FileStorageService
{
    public class AzureBlobStorageService : IFileStorageService
    {
        public Task<string> UploadFileAsync(byte[] fileData, string fileName, string directory)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public bool isImage(byte[] fileData)
        {
            throw new NotImplementedException();
        }
    }
}
