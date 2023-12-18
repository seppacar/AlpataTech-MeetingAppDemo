import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Meeting } from '../../core/models/meeting/meeting.model';
import { MeetingService } from '../../core/services/meeting/meeting.service';

@Component({
  selector: 'app-meeting-details',
  templateUrl: './meeting-details.component.html',
  styleUrl: './meeting-details.component.scss'
})
export class MeetingDetailsComponent {
  meeting: Meeting | null = null

  constructor(private route: ActivatedRoute, private meetingService: MeetingService) {}

  ngOnInit(){
    this.getMeeting(this.route.snapshot.params['id'])
  }

  getMeeting(id: number){
    this.meetingService.get(id)
    .subscribe(
      {
        next: (meeting) => {console.log(meeting)},
        error: (error) => {console.error(error)}
      }
    )
  }

}
