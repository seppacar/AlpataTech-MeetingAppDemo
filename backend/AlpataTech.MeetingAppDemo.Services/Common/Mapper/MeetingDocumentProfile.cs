using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingDocument;
using AutoMapper;

namespace AlpataTech.MeetingAppDemo.Services.Common.Mapper
{
    public class MeetingDocumentProfile : Profile
    {
        public MeetingDocumentProfile()
        {
            CreateMap<MeetingDocument, MeetingDocumentDto>();
            CreateMap<MeetingDocumentDto, MeetingDocument>();
        }
    }
}
