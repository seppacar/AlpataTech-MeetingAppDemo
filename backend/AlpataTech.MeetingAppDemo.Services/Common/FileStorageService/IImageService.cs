using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpataTech.MeetingAppDemo.Services.Common.FileStorageService
{
    public interface IImageService
    {
        public byte[] CompressAndConvertWebP(byte[] originalImage);
    }
}
