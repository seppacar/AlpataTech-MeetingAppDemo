using AlpataTech.MeetingAppDemo.Entities;
using AutoMapper;

namespace AlpataTech.MeetingAppDemo.Services.Common.Mapper
{
    public class FileUploadModelProfile : Profile
    {
        public FileUploadModelProfile()
        {
            // Map FileUplaodModel to MeetingDocument
            CreateMap<FileUploadModel, MeetingDocument>()
                .ForMember(dest => dest.DocumentTitle, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.ContentType));
        }
    }
}
