namespace AlpataTech.MeetingAppDemo.Services.Common.FileStorageService
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(byte[] fileData, string fileName);
        Task<byte[]> GetFileAsync(string fileName);
        Task DeleteFileAsync(string fileName);
    }
}
