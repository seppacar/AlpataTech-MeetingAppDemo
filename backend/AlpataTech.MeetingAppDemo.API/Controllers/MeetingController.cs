using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingParticipant;
using AlpataTech.MeetingAppDemo.Services.MeetingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllMeetings()
        {
            var meetings = await _meetingService.GetAllMeetingsAsync();
            return Ok(meetings);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetMeetingById(int id)
        {
            // Extract "sub" from JWT (which is userId) and convert to int
            var userIdentity = User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            // Only allow if request maker user is organizer or Participant or is "Admin"
            if (await _meetingService.IsUserOrganizer(id, userId) || await _meetingService.IsUserParticipant(id, userId) || User.IsInRole("Admin"))
            {
                var meeting = await _meetingService.GetMeetingByIdAsync(id);
                if (meeting == null)
                {
                    return NotFound();
                }
                return Ok(meeting);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateMeeting([FromBody] CreateMeetingDto createMeetingDto)
        {
            // Extract "sub" from JWT (which is userId) and convert to int
            var userIdentity = User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            createMeetingDto.OrganizerId = userId;

            var meetingDto = await _meetingService.CreateMeetingAsync(createMeetingDto);
            return CreatedAtAction(nameof(GetMeetingById), new { id = meetingDto.Id }, meetingDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateMeeting(int id, [FromBody] UpdateMeetingDto updateMeetingDto)
        {
            // Extract "sub" from JWT (which is userId) and convert to int
            var userIdentity = User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            // Only allow if request maker user is organizer or is "Admin"
            if (await _meetingService.IsUserOrganizer(id, userId) || User.IsInRole("Admin"))
            {
                var meeting = await _meetingService.UpdateMeetingAsync(id, updateMeetingDto);
                return Ok(meeting);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            // Extract "sub" from JWT (which is userId) and convert to int
            var userIdentity = User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            // Only allow if request maker user is organizer or is "Admin"
            if (await _meetingService.IsUserOrganizer(id, userId) || User.IsInRole("Admin"))
            {
                await _meetingService.DeleteMeetingAsync(id);
                return NoContent();
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("{id}/participants")]
        [Authorize]
        public async Task<IActionResult> AddMeetingParticipant(int id, [FromBody] CreateMeetingParticipantDto createMeetingParticipantDto)
        {
            // Extract "sub" from JWT (which is userId) and convert to int
            var userIdentity = User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            // Only allow if request maker user is organizer or is "Admin"
            if (await _meetingService.IsUserOrganizer(id, userId) || User.IsInRole("Admin"))
            {
                return Ok(await _meetingService.AddMeetingParticipantAsync(id, createMeetingParticipantDto));
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("{meetingId}/participants/{participantUserId}")]
        [Authorize]
        public async Task<IActionResult> RemoveMeetingParticipant(int meetingId, int participantUserId)
        {
            // Extract "sub" from JWT (which is userId) and convert to int
            var userIdentity = User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            // Only allow if request maker user is organizer or is "Admin"
            if ((await _meetingService.IsUserOrganizer(meetingId, userId) || User.IsInRole("Admin")) && userId != participantUserId)
            {
                await _meetingService.RemoveMeetingParticipantAsync(meetingId, participantUserId);
                return NoContent();
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("{meetingId}/documents")]
        [Authorize]
        public async Task<IActionResult> AddMeetingDocument(int meetingId, IFormFile meetingDocumentFile)
        {
            // Extract "sub" from JWT (which is userId) and convert to int
            var userIdentity = User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            // Only allow if request maker user is organizer or is "Admin"
            if (await _meetingService.IsUserOrganizer(meetingId, userId) || User.IsInRole("Admin"))
            {
                string[] permittedExtensions = { ".pdf", ".docx", ".pptx", "webp" };

                var fileExtension = Path.GetExtension(meetingDocumentFile.FileName).ToLower();
                var meetingDocumentFileModel = new FileUploadModel
                {
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

                var createdMeetingDocument = await _meetingService.AddMeetingDocumentAsync(meetingId, meetingDocumentUploadObject);

                // TODO: convert to created at action
                return Ok(meetingDocumentUploadObject);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpGet("{meetingId}/documents/{meetingDocumentId}/download")]
        [Authorize]
        public async Task<IActionResult> GetMeetingDocumentFile(int meetingId, int meetingDocumentId)
        {
            // Check if request maker is a participant
            // Extract "sub" from JWT (which is userId) and convert to int
            var userIdentity = User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            // Only allow if request maker user is organizer or is "Admin"
            if (await _meetingService.IsUserParticipant(meetingId, userId) || User.IsInRole("Admin"))
            {
                var meetingDocumentFileBytes = await _meetingService.GetMeetingDocumentFileAsync(meetingId, meetingDocumentId);
                var meetingDocumentObject = await _meetingService.GetMeetingDocumentObjectAsync(meetingId, meetingDocumentId);

                // Set headers for file download
                Response.Headers.Add($"Content-Disposition", $"attachment; filename={meetingDocumentObject.DocumentTitle}");
                // Send the file content as the response
                return File(meetingDocumentFileBytes, "application/octet-stream");
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("{meetingId}/documents/{meetingDocumentId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveMeetingDocument(int meetingId, int meetingDocumentId)
        {
            // Extract "sub" from JWT (which is userId) and convert to int
            var userIdentity = User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            // Check if request maker user is organizer or Admin
            if (await _meetingService.IsUserOrganizer(meetingId, userId) || User.IsInRole("Admin"))
            {
                await _meetingService.RemoveMeetingDocumentAsync(meetingId, meetingDocumentId);
                return NoContent();
            }
            else
            {
                return Forbid();
            }
        }
    }
}
