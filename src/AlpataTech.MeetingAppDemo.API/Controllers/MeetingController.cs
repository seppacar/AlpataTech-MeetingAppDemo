using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AlpataTech.MeetingAppDemo.Services.MeetingService;
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
        public async Task<IActionResult> GetAllMeetings()
        {
            var meetings = await _meetingService.GetAllMeetingsAsync();
            return Ok(meetings);
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> CreateMeeting([FromBody] CreateMeetingDto createMeetingDto)
        {
            var meetingDto = await _meetingService.CreateMeetingAsync(createMeetingDto);
            return CreatedAtAction(nameof(GetMeetingById), new { id = meetingDto.Id }, meetingDto);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateMeeting(int id, [FromBody] UpdateMeetingDto updateMeetingDto)
        {
            var meeting = await _meetingService.UpdateMeetingAsync(id, updateMeetingDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            await _meetingService.DeleteMeetingAsync(id);
            return NoContent();
        }
    }
}
