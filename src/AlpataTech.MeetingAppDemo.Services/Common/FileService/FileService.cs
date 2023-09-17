namespace AlpataTech.MeetingAppDemo.Services.Common.FileService
{
    public class FileService : IFileService
    {
        private readonly string _baseDirectory;

        public FileService(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
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

        public bool isImage(byte[] fileData)
        {
            throw new NotImplementedException();
        }
    }
}
