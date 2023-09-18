using AlpataTech.MeetingAppDemo.Services.Common.FileStorageService;

namespace AlpataTech.MeetingAppDemo.Services.Common.LocalFileStorageService
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _baseDirectory = "temp";

        public LocalFileStorageService()
        {
        }

        public async Task<string> UploadFileAsync(byte[] fileData, string fileName, string directory)
        {
            // Ensure the directory exists
            string targetDirectory = Path.Combine(_baseDirectory, directory);
            Directory.CreateDirectory(targetDirectory);

            // Combine the directory and file name to get the file path
            string filePath = Path.Combine(targetDirectory, fileName);

            // Write the file to the specified directory
            await File.WriteAllBytesAsync(filePath, fileData);

            return filePath;
        }

        public async Task DeleteFileAsync(string filePath)
        {
            // Check if the file exists and delete it
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public Task<byte[]> GetFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
