using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingDocument;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingParticipant;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AlpataTech.MeetingAppDemo.Services.MeetingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AlpataTech.MeetingAppDemo.API.Controllers
{

    [ApiController]
    [Route("api/meetings")]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        /* Admin Authorized Routes */

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllMeetings()
        {
            var meetings = await _meetingService.GetAllMeetingsAsync();
            return Ok(meetings);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMeetingById(int id)
        {
            var meeting = await _meetingService.GetMeetingByIdAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return Ok(meeting);
        }

        [HttpPost]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> CreateMeeting([FromBody] CreateMeetingDto createMeetingDto)
        {
            var meetingDto = await _meetingService.CreateMeetingAsync(createMeetingDto);
            return CreatedAtAction(nameof(GetMeetingById), new { id = meetingDto.Id }, meetingDto);
        }

        [HttpPost("{id}")]
        // TODO: Authorize Organizer and Admin
        public async Task<IActionResult> UpdateMeeting(int id, [FromBody] UpdateMeetingDto updateMeetingDto)
        {
            var meeting = await _meetingService.UpdateMeetingAsync(id, updateMeetingDto);
            return Ok();
        }

        [HttpDelete]
        // TODO: Authorize Organizer and Admin
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            await _meetingService.DeleteMeetingAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/participants")]
        public async Task<IActionResult> AddMeetingParticipant(int id, [FromBody] CreateMeetingParticipantDto createMeetingParticipantDto)
        {
            return Ok(await _meetingService.AddMeetingParticipantAsync(id, createMeetingParticipantDto));
        }

        [HttpPost("{id}/documents")]
        public async Task<IActionResult> AddMeetingDocument(int id, IFormFile meetingDocumentFile)
        {
            string[] permittedExtensions = { ".pdf", ".docx", ".pptx", "webp" };

            var fileExtension = Path.GetExtension(meetingDocumentFile.FileName).ToLower();
            var meetingDocumentFileModel = new FileUploadModel {
                FileName = Path.GetExtension(meetingDocumentFile.FileName).ToLower(),
                FileExtension = fileExtension,
            };

            if (!permittedExtensions.Contains(fileExtension))
            {
                return BadRequest($"Unsupported file extension. Permitted extensions: {permittedExtensions.ToString()}");
            }

            var meetingDocumentUploadObject = new FileUploadModel
            {
                FileName = meetingDocumentFile.FileName,
                FileExtension = fileExtension,
                ContentType = meetingDocumentFile.ContentType,
            };

            // Convert IFormFile to byte array for the profile picture
            using (var ms = new MemoryStream())
            {
                await meetingDocumentFile.CopyToAsync(ms);
                meetingDocumentUploadObject.FileData = ms.ToArray();
            }

            var createdMeetingDocument = await _meetingService.AddMeetingDocumentAsync(id, meetingDocumentUploadObject);

            // TODO: convert to created at action
            return Ok(meetingDocumentUploadObject);
        }

        [HttpGet("{meetingId}/documents/{meetingDocumentId}/download")]
        public async Task<IActionResult> GetMeetingDocumentFile(int meetingId, int meetingDocumentId)
        {
            var meetingDocumentFileBytes = await _meetingService.GetMeetingDocumentFileAsync(meetingId, meetingDocumentId);
            var meetingDocumentObject = await _meetingService.GetMeetingDocumentObjectAsync(meetingId, meetingDocumentId);

            // Set headers for file download
            Response.Headers.Add($"Content-Disposition", $"attachment; filename={meetingDocumentObject.DocumentTitle}");
            // Send the file content as the response
            return File(meetingDocumentFileBytes, "application/octet-stream");
        }

        [HttpDelete("{meetingId}/documents/{meetingDocumentId}")]
        public async Task<IActionResult> RemoveMeetingDocument(int meetingId, int meetingDocumentId)
        {
            await _meetingService.RemoveMeetingDocumentAsync(meetingId, meetingDocumentId);
            return NoContent();
        }

    }
}
