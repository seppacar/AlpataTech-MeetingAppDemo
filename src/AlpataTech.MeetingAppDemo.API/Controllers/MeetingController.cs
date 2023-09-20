using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AlpataTech.MeetingAppDemo.Entities.DTO.MeetingParticipant;
using AlpataTech.MeetingAppDemo.Services.MeetingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddMeetingParticipant(int id, [FromBody] MeetingParticipantDto meetingParticipantDto)
        {
            return Ok(await _meetingService.AddMeetingParticipantAsync(id, meetingParticipantDto));
        }
    }
}
