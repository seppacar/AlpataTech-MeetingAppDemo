namespace AlpataTech.MeetingAppDemo.Services.Common.FileService
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(byte[] fileData, string fileName, string directory);

        Task DeleteFileAsync(string filePath);

        bool isImage(byte[] fileData);
    }
}
