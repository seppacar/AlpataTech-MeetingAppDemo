using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace AlpataTech.MeetingAppDemo.Services.Common.FileStorageService
{
    public class ImageService : IImageService
    {   
        public byte[] CompressAndConvertWebP(byte[] originalImage)
        {
            using (var image = Image.Load(originalImage))
            {
                // Resize or apply other transformations as needed
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(800, 600),
                    Mode = ResizeMode.Max
                }));

                // Configure WebP encoding options (compression quality)
                var webpEncoder = new WebpEncoder { Quality = 75 };

                // Save the image as WebP format
                var webpStream = new MemoryStream();
                image.Save(webpStream, webpEncoder);

                return webpStream.ToArray();
            }
        }
    }
}
