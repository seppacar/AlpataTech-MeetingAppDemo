using AlpataTech.MeetingAppDemo.DAL.Repository;
using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingDocument;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingParticipant;
using AlpataTech.MeetingAppDemo.Services.Common.FileStorageService;
using AutoMapper;
using System.Linq.Expressions;

namespace AlpataTech.MeetingAppDemo.Services.MeetingService
{
    public class MeetingService : IMeetingService
    {
        private readonly MeetingRepository _meetingRepository;
        private readonly MeetingDocumentRepository _meetingDocumentRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public MeetingService(MeetingRepository meetingRepository, MeetingDocumentRepository meetingDocumentRepository, IFileStorageService fileStorageService, IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _meetingDocumentRepository = meetingDocumentRepository;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
        }
        public async Task<MeetingDto> CreateMeetingAsync(CreateMeetingDto createMeetingDto)
        {
            var meeting = _mapper.Map<Meeting>(createMeetingDto);
            await _meetingRepository.AddAsync(meeting);

            return _mapper.Map<MeetingDto>(meeting);
        }

        public async Task<IEnumerable<MeetingDto>> GetAllMeetingsAsync()
        {
            var meetings = await _meetingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MeetingDto>>(meetings);
        }

        public async Task<MeetingDto> GetMeetingByIdAsync(int id)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);
            return _mapper.Map<MeetingDto>(meeting);
        }

        public async Task<IEnumerable<MeetingDto>> FindMeetingsAsync(Expression<Func<Meeting, bool>> predicate)
        {
            var meetings = await _meetingRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<MeetingDto>>(meetings);
        }

        public async Task<MeetingDto> UpdateMeetingAsync(int id, UpdateMeetingDto updateMeetingDto)
        {
            var meetingToUpdate = await _meetingRepository.GetByIdAsync(id);

            if (meetingToUpdate == null)
            {
                return null;
            }

            // Update the user properties with the values from updateUserDto
            _mapper.Map(updateMeetingDto, meetingToUpdate);

            // Update the user in the repository
            _meetingRepository.Update(meetingToUpdate);
            await _meetingRepository.SaveChangesAsync();

            // Map and return the updated user
            return _mapper.Map<MeetingDto>(meetingToUpdate);
        }

        public async Task DeleteMeetingAsync(int id)
        {
            _meetingRepository.Remove(await _meetingRepository.GetByIdAsync(id));
            await _meetingRepository.SaveChangesAsync();
        }

        public async Task<MeetingDto> AddMeetingParticipantAsync(int meetingId, CreateMeetingParticipantDto createMeetingParticipantDto)
        {
            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if(meeting == null)
            {
                throw new Exception("Meeting not found");
            }
            
            var meetingParticipant = _mapper.Map<MeetingParticipant>(createMeetingParticipantDto);
            // Set meetingId for meetingParticipant object
            meetingParticipant.MeetingId = meetingId;

            // Add participant
            meeting.Participants.Add(meetingParticipant);

            await _meetingRepository.SaveChangesAsync();

            return _mapper.Map<MeetingDto>(meeting);
        }

        public async Task<MeetingDocumentDto> AddMeetingDocumentAsync(int meetingId, FileUploadModel meetingDocumentUploadObject)
        {
            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
            {
                throw new Exception("Meeting not found");
            }

            string fileName = "meetingdoc_" + Guid.NewGuid().ToString() + meetingDocumentUploadObject.FileExtension;
            
            await _fileStorageService.UploadFileAsync(meetingDocumentUploadObject.FileData, fileName);

            var meetingDocument = _mapper.Map<MeetingDocument>(meetingDocumentUploadObject);

            // Consider this later
            meetingDocument.DocumentPath = fileName;

            meeting.Documents.Add(meetingDocument);

            await _meetingRepository.SaveChangesAsync();

            return (_mapper.Map<MeetingDocumentDto>(meetingDocument));
        }


        public async Task<MeetingDocumentDto> GetMeetingDocumentObjectAsync(int meetingId, int meetingDocumentId)
        {
            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if(meeting == null)
            {
                throw new Exception("Meeting not found");
            }
            var meetingDocumentObject = await _meetingDocumentRepository.GetByIdAsync(meetingDocumentId);
            if (meetingDocumentObject == null)
            {
                throw new Exception("Meeting document not found");
            }
            return _mapper.Map<MeetingDocumentDto>(meetingDocumentObject);
        }

        public async Task<byte[]> GetMeetingDocumentFileAsync(int meetingId, int meetingDocumentId)
        {
            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
            {
                throw new Exception("Meeting not found");
            }
            var meetingDocumentObject = await _meetingDocumentRepository.GetByIdAsync(meetingDocumentId);
            if (meetingDocumentObject == null)
            {
                throw new Exception("Meeting document not found");
            }
            var meetingDocumentFileBytes = await _fileStorageService.GetFileAsync(meetingDocumentObject.DocumentPath);

            return meetingDocumentFileBytes;
        }

        public async Task RemoveMeetingDocumentAsync(int meetingId, int meetingDocumentId)
        {
            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
            {
                throw new Exception("Meeting not found");
            }
            var meetingDocumentObject = await _meetingDocumentRepository.GetByIdAsync(meetingDocumentId);
            if (meetingDocumentObject == null)
            {
                throw new Exception("Meeting not found");
            }
            _meetingDocumentRepository.Remove(meetingDocumentObject);

            await _meetingDocumentRepository.SaveChangesAsync();
        }

        public async Task<bool> IsUserParticipant(int meetingId, int userId)
        {
            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
            {
                new Exception("Meeting not found");
            }

            return meeting.Participants.Any(participant => participant.Id == userId);
        }

        public async Task<bool> IsUserOrganizer(int meetingId, int userId)
        {
            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
            {
                new Exception("Meeting not found");
            }

            return meeting.OrganizerId == userId;
        }
    }
}
