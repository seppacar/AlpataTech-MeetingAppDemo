namespace AlpataTech.MeetingAppDemo.Services.Common.FileStorageService
{
    public class AzureBlobStorageService : IFileStorageService
    {
        public Task<string> UploadFileAsync(byte[] fileData, string fileName)
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

        public Task<byte[]> GetDefaultProfilePicture()
        {
            throw new NotImplementedException();
        }
    }
}
