using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
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
        public IActionResult GetAllMeetings()
        {
            var meetings = _meetingService.GetAll();
            return Ok(meetings);
        }

        [HttpGet("{id}")]
        public IActionResult GetMeetingById(int id)
        {
            var meeting = _meetingService.GetById(id);
            return Ok(meeting);
        }

        [HttpPost]
        public IActionResult CreateMeeting([FromBody] CreateMeetingDto createMeetingDto)
        {
            _meetingService.CreateMeeting(createMeetingDto);
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateMeeting([FromBody] UpdateMeetingDto updateMeetingDto)
        {
            _meetingService.UpdateMeeting(updateMeetingDto);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteMeeting(int id)
        {
            _meetingService.DeleteMeeting(id);
            return Ok();
        }
    }
}
