using AlpataTech.MeetingAppDemo.Services.Common.FileStorageService;

namespace AlpataTech.MeetingAppDemo.Services.Common.LocalFileStorageService
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _storagePath;

        public LocalFileStorageService(string storagePath)
        {
            _storagePath = storagePath;
        }

        public async Task<string> UploadFileAsync(byte[] fileData, string fileName)
        {
            // Ensure the directory exists
            string targetDirectory = Path.Combine(_storagePath);
            Directory.CreateDirectory(targetDirectory);

            // Combine the directory and file name to get the file path
            string filePath = Path.Combine(targetDirectory, fileName);

            // Write the file to the specified directory
            await File.WriteAllBytesAsync(filePath, fileData);

            return filePath;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            try
            {
                // Combine the storage path with the file name to get the full file path
                string filePath = Path.Combine(_storagePath, fileName);

                // Check if the file exists and delete it
                if (File.Exists(filePath))
                {
                    await Task.Run(() => File.Delete(filePath));
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions according to your requirements
                throw new Exception("An error occurred while deleting the file.", ex);
            }
        }

        public Task<byte[]> GetFileAsync(string fileName)
        {
            try
            {
                // Combine the storage path with the file name to get the full file path
                string filePath = Path.Combine(_storagePath, fileName);
                Console.WriteLine("YOUR FİLE İSSS"+ filePath);
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File not found.", filePath);
                }

                // Read the file into a byte array
                return Task.Run(() => File.ReadAllBytes(filePath));
            }
            catch (Exception ex)
            {
                // Handle any exceptions according to your requirements
                throw new Exception("An error occurred while reading the file.", ex);
            }
        }
    }
}
