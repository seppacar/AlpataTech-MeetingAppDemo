import { Component } from '@angular/core';
import { PageService } from '../../core/services/page/page.service';
import { AuthService } from '../../core/services/auth/auth.service';
import { MeetingService } from '../../core/services/meeting/meeting.service';
import { UserService } from '../../core/services/user/user.service';
import { User } from '../../core/models/user/user.model';
import { Meeting } from '../../core/models/meeting/meeting.model';

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
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.pageService.setPageInfo('Dashboard', 'Lorem ipsum dolor ist amet');
    this.authService.currentUser$.subscribe((user) => {
      this.currentUser = user;
    });

    //
    // Generate mock data
    for (let i = 1; i <= 20; i++) {
      const mockMeeting = new Meeting({
        id: i,
        title: `Meeting ${i}`,
        description: `Description for Meeting ${i}`,
        organizer: { id: i, name: `Organizer ${i}` },
        participants: [1, 2, 3, 2, 4, 5, 2],
        documents: [],
        startTime: '2023-01-01T10:00:00',
        endTime: '2023-01-01T12:00:00',
      });

      this.participatedMeetings.push(mockMeeting);
    }
  }

  fetchUpcompingMeetings() {
    // Get participated meetings
  }

  fetchMeetingPeople() {
    console.log();
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
