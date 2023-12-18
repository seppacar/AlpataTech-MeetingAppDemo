import { Component } from '@angular/core';
import { PageService } from '../../core/services/page/page.service';
import { UserService } from '../../core/services/user/user.service';
import { User } from '../../core/models/user/user.model';
import { forkJoin, finalize } from 'rxjs';
import { AuthService } from '../../core/services/auth/auth.service';
import { Meeting } from '../../core/models/meeting/meeting.model';
import { MeetingParticipant } from '../../core/models/meeting/meeting-participant.model';
import { MeetingService } from '../../core/services/meeting/meeting.service';
import { UIService } from '../../core/services/ui/ui.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-meeting',
  templateUrl: './create-meeting.component.html',
  styleUrls: ['./create-meeting.component.scss']
})
export class CreateMeetingComponent {
  currentUser: User | null = null;
  isLoading = false
  isProfileImagesLoaded = false;
  users: User[] = [];
  // Form template fields
  maxDurationInMinutes = 360
  durationInterval = 30
  meetingDurations = Array.from({ length: this.maxDurationInMinutes / this.durationInterval }, (_, index) => (index + 1) * this.durationInterval);
  newMeeting = new Meeting({})
  meetingDurationMinutes: number = this.durationInterval
  selectedDocuments: File[] = [];
  selectedParticipants = [];

  constructor(private pageService: PageService, private uiService: UIService, private router: Router, private userService: UserService, private authService: AuthService, private meetingService: MeetingService) { }

  ngOnInit(): void {
    this.fetchUsers()
    this.pageService.setPageInfo('Organize Meeting', 'Lorem ipsum dolor ist amet')

    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
  }

  fetchUsers() {
    this.userService.getAll()
      .subscribe(
        {
          next: (users) => {
            this.users = users
          },
          error: (error) => { console.error(error) },
          complete: () => { this.fetchUserProfilePictures() }
        }
      )
  }

  fetchUserProfilePictures() {
    const observables = this.users.map((user) => {
      return this.userService.getProfilePicture(user.id);
    });

    forkJoin(observables).subscribe(
      {
        next: (profileImages) => {
          profileImages.forEach((blob, index) => {
            this.users[index].profileImageUrl = URL.createObjectURL(blob);
          });
        },
        error: (error) => { console.error(error) },
        complete: () => { this.isProfileImagesLoaded = true }
      }
    );
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      for (let i = 0; i < input.files.length; i++) {
        this.selectedDocuments.push(input.files[i]);
      }
    }
  }

  removeFile(file: File): void {
    this.selectedDocuments = this.selectedDocuments.filter(f => f !== file);
  }

  // TODO: Replace this mess later (use something elegant like moment.js)
  getISOStringWithLocalTimezone(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); // Months are zero-based
    const day = date.getDate().toString().padStart(2, '0');
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }

  createMeeting() {
    this.uiService.showSpinner()

    let createdMeetingId = -1
    const startTimeString = this.newMeeting.startTime;
    const startTime = new Date(startTimeString);
    const endTime = this.getISOStringWithLocalTimezone(new Date(startTime.getTime() + this.meetingDurationMinutes * 60000))
    this.newMeeting.endTime = endTime

    // Set loading
    this.isLoading = true

    // Create meeting
    this.meetingService.create(this.newMeeting)
      .subscribe({
        next: (meeting) => { createdMeetingId = meeting.id },
        error: (error) => {
          console.error(error)
          this.uiService.hideSpinner()
        },
        complete: () => {
          const addParticipantObservables = this.selectedParticipants.map((participant) => {
            return this.meetingService.addMeetingParticipant(createdMeetingId, new MeetingParticipant({ userId: participant }));
          });

          const addDocumentObservables = this.selectedDocuments.map((document) => {
            return this.meetingService.addMeetingDocument(createdMeetingId, document);
          });

          // Combine all observables using forkJoin
          forkJoin([...addParticipantObservables, ...addDocumentObservables])
            .pipe(finalize(() => {
              this.isLoading = false;
              this.uiService.hideSpinner()
              this.router.navigateByUrl(`/meeting-details/${createdMeetingId}`)
            }))
            .subscribe({
              error: (error) => {
                // TODO: Handle error here, destroy meeting if adding participants or documents failed?
                console.error(error);
              },
            });
        }
      });
  }
}
