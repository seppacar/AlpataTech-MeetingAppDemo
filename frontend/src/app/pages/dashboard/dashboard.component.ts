import { Component } from '@angular/core';
import { PageService } from '../../core/services/page/page.service';
import { AuthService } from '../../core/services/auth/auth.service';
import { MeetingService } from '../../core/services/meeting/meeting.service';
import { UserService } from '../../core/services/user/user.service';
import { User } from '../../core/models/user/user.model';
import { Meeting } from '../../core/models/meeting/meeting.model';
import { UIService } from '../../core/services/ui/ui.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {
  currentUser: User | null = null;
  participatedMeetings: Meeting[] = [];
  participatedMeetingsCurrentPage = 1;
  itemsPerPage = 5;

  constructor(
    private pageService: PageService,
    private authService: AuthService,
    private meetingService: MeetingService,
    private userService: UserService,
    private uiService: UIService
  ) {}

  ngOnInit(): void {
    this.uiService.showSpinner()

    this.pageService.setPageInfo('Dashboard', 'Lorem ipsum dolor ist amet');
    this.authService.currentUser$.subscribe((user) => {
      this.currentUser = user;
    });

    this.meetingService.getParticipatedMeetings()
    .pipe(
      finalize(() => {
        this.uiService.hideSpinner()
      })
    )
    .subscribe(
      {
        next: (meetings) => {
          this.participatedMeetings = meetings;
          meetings.sort((a, b) => new Date(b.startTime).getTime() - new Date(a.startTime).getTime());
        },
        error: (error) => {console.error(error)}
      }
    )
  }

  fetchUpcompingMeetings() {
    // Get participated meetings
  }

  fetchParticipantProfilePicture(id: number){
    let blobUrl = ''
    this.userService.getProfilePicture(id)
    .subscribe(
      {
        next: (blob) => {blobUrl = URL.createObjectURL(blob)},
      }
    )
    return blobUrl
  }

  get paginatedMeetings(): Meeting[] {
    const startIndex =
      (this.participatedMeetingsCurrentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    return this.participatedMeetings.slice(startIndex, endIndex);
  }

  get totalItems(): number {
    return this.participatedMeetings.length;
  }

  get pages(): number[] {
    const pageCount = Math.ceil(this.totalItems / this.itemsPerPage);
    return Array.from({ length: pageCount }, (_, index) => index + 1);
  }
  setCurrentPage(page: number): void {
    this.participatedMeetingsCurrentPage = page;
  }

  calculateTimeRemaining(endTimeString: string): string {
    // Parse the input string into a Date object
    const endTime = new Date(endTimeString);

    // Get the current time
    const currentTime = new Date();

    // Check if the meeting has ended
    if (endTime < currentTime) {
      return 'Meeting ended';
    }

    // Calculate the time difference in milliseconds
    const timeDifference = endTime.getTime() - currentTime.getTime();

    // Calculate the remaining days and hours
    const remainingDays = Math.floor(timeDifference / (1000 * 60 * 60 * 24));
    const remainingHours = Math.floor(
      (timeDifference % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60)
    );

    // Construct the result string
    let result = '';
    if (remainingDays > 0) {
      result += `${remainingDays} days `;
    }

    result += `${remainingHours} hours left`;

    return result;
  }
}