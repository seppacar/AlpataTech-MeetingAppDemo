import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Meeting } from '../../core/models/meeting/meeting.model';
import { MeetingService } from '../../core/services/meeting/meeting.service';
import { PageService } from '../../core/services/page/page.service';
import { AuthService } from '../../core/services/auth/auth.service';
import { User } from '../../core/models/user/user.model';
import { UIService } from '../../core/services/ui/ui.service';
import { UserService } from '../../core/services/user/user.service';
import { MeetingParticipant } from '../../core/models/meeting/meeting-participant.model';
import { finalize, forkJoin } from 'rxjs';

@Component({
  selector: 'app-meeting-details',
  templateUrl: './meeting-details.component.html',
  styleUrl: './meeting-details.component.scss',
})
export class MeetingDetailsComponent {
  // Here for add participants functionality it would be better if create seperate features for these
  allUsers: User[] = [];
  selectedParticipants = [];
  selectedDocuments: File[] = [];
  isProfileImagesLoaded = false;
  //
  //
  currentUser: User | null = null;
  currentMeeting: Meeting | null = null;
  //
  constructor(
    private route: ActivatedRoute,
    private meetingService: MeetingService,
    private userService: UserService,
    private pageService: PageService,
    private uiService: UIService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authService.currentUser$.subscribe((user) => {
      this.currentUser = user;
    });
    this.pageService.setPageInfo(
      'Meeting Details',
      'Lorem ipsum dolor sit amet'
    );
    this.getMeeting(this.route.snapshot.params['id']);
    this.fetchUsers();
  }

  getMeeting(id: number) {
    this.meetingService.get(id).subscribe({
      next: (meeting) => {
        this.currentMeeting = meeting;
      },
      error: (error) => {
        console.error(error);
      },
      complete: () => {
        this.fetchParticipantProfilePictures();
      },
    });
  }

  getMeetingDate(meetingStart: string): string {
    const startDate = new Date(meetingStart);
    return startDate.toUTCString();
  }

  getMeetingDuration(meetingStart: string, meetingEnd: string): string {
    const startDate = new Date(meetingStart);
    const endDate = new Date(meetingEnd);

    const durationInMinutes =
      (endDate.getTime() - startDate.getTime()) / (1000 * 60);
    const hours = Math.floor(durationInMinutes / 60);
    const minutes = Math.floor(durationInMinutes % 60);

    if (hours > 0) {
      return `${hours} hrs ${minutes} mins`;
    } else {
      return `${minutes} minutes`;
    }
  }

  fetchParticipantProfilePictures() {
    this.currentMeeting?.participants.forEach((participant) => {
      this.userService.getProfilePicture(participant.userId).subscribe({
        next: (profileImageBlob) => {
          participant.profileImageUrl = URL.createObjectURL(profileImageBlob);
        },
        error: (error) => {
          console.error(error);
        },
        complete: () => {
          console.log('yes');
        },
      });
    });
  }

  fetchUsers() {
    this.userService.getAll().subscribe({
      next: (users) => {
        this.allUsers = users;
      },
      error: (error) => {
        console.error(error);
      },
      complete: () => {
        this.fetchUserProfilePictures();
      },
    });
  }

  fetchUserProfilePictures() {
    const observables = this.allUsers.map((user) => {
      return this.userService.getProfilePicture(user.id);
    });

    forkJoin(observables).subscribe({
      next: (profileImages) => {
        profileImages.forEach((blob, index) => {
          this.allUsers[index].profileImageUrl = URL.createObjectURL(blob);
        });
      },
      error: (error) => {
        console.error(error);
      },
      complete: () => {
        this.isProfileImagesLoaded = true;
      },
    });
  }

  isUserInMeeting(userId: number): boolean {
    // Check if user.id is in currentMeeting.participants
    return !!this.currentMeeting?.participants.find(
      (participant) => participant.userId === userId
    );
  }

  submitAddParticipants() {
    if (this.currentMeeting) {
      this.uiService.showSpinner();
      const meetingId = this.currentMeeting.id;
      const addParticipantObservables = this.selectedParticipants.map(
        (participant) => {
          return this.meetingService.addMeetingParticipant(
            meetingId,
            new MeetingParticipant({ userId: participant })
          );
        }
      );
      forkJoin(addParticipantObservables)
        .pipe(
          finalize(() => {
            this.uiService.hideSpinner();
          })
        )
        .subscribe({
          next: () => {
            this.selectedParticipants = [];
            this.getMeeting(meetingId);
          },
          error: (error) => {
            console.error(error);
          },
          complete: () => {
            this.uiService.toastrShowSuccess('Participant added');
          },
        });
    }
  }

  removeParticipant(participantUserId: number) {
    // Remove participant from meeting
    this.uiService.showSpinner();
    if (this.currentMeeting) {
      const meetingId = this.currentMeeting.id;
      this.meetingService
        .removeMeetingParticipant(meetingId, participantUserId)
        .subscribe({
          next: () => {
            this.getMeeting(meetingId);
          },
          error: (error) => {
            console.error(error);
          },
          complete: () => {
            this.uiService.hideSpinner();
            this.uiService.toastrShowWarning(
              'Participant removed from meeting!'
            );
          },
        });
    }
  }

  addSelectedDocuments() {
    // Show spinner while documents are being added
    this.uiService.showSpinner();

    // Initialize a counter for the number of documents to add
    let numberOfDocumentsToAdd = 0;

    // Iterate through selected documents
    this.selectedDocuments.forEach((document) => {
      // Check if there is a current meeting
      if (this.currentMeeting) {
        // Increment the counter for each document
        numberOfDocumentsToAdd += 1;

        // Add the document to the current meeting using the meeting service
        this.meetingService
          .addMeetingDocument(this.currentMeeting.id, document)
          .subscribe({
            next: () => {
              // Decrement the counter when a document is successfully added
              numberOfDocumentsToAdd -= 1;
            },
            complete: () => {
              // Check if all documents have been processed
              if (numberOfDocumentsToAdd === 0) {
                // Hide the spinner and display a success message
                this.uiService.hideSpinner();
                this.uiService.toastrShowSuccess('Files added successfully');
                // Empty selected documents
                this.selectedDocuments = [];
                // Reload the meeting
                this.getMeeting(this.currentMeeting?.id ?? 0);
              }
            },
          });
      }
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length) {
      // Convert FileList to array using Array.from
      const files: File[] = Array.from(input.files);

      // Use forEach for readability
      files.forEach((file) => {
        this.selectedDocuments.push(file);
      });
    }
  }

  removeSelectedDocument(file: File): void {
    this.selectedDocuments = this.selectedDocuments.filter((f) => f !== file);
  }

  // Remove document from meeting
  removeDocument(meetingDocumentId: number) {
    this.uiService.showSpinner();
    if (this.currentMeeting) {
      const meetingId = this.currentMeeting.id;
      this.meetingService
        .removeMeetingDocument(meetingId, meetingDocumentId)
        .subscribe({
          next: () => {
            this.getMeeting(meetingId);
          },
          error: (error) => {
            console.error(error);
          },
          complete: () => {
            this.uiService.hideSpinner();
            this.uiService.toastrShowWarning('Document removed from meeting!');
          },
        });
    }
  }

  downloadDocument(meetingDocumentId: number) {
    // Download document
    if (this.currentMeeting) {
      const meetingId = this.currentMeeting.id;
      const fileName = this.currentMeeting?.documents.find(
        (document) => document.id === meetingDocumentId
      )?.documentTitle;
      this.meetingService
        .getMeetingDocument(meetingId, meetingDocumentId)
        .subscribe({
          next: (file) => {
            const blob = new Blob([file], { type: 'application/octet-stream' });
            const url = window.URL.createObjectURL(blob);

            // Create a link element and trigger a click to start the download
            const link = document.createElement('a');
            link.href = url;
            if (fileName) {
              link.download = fileName; // Replace with the desired file name
            }
            link.click();
            // Clean up
            window.URL.revokeObjectURL(url);
          },
        });
    }
  }

  updateMeetingDetalis() {
    // Update meeting details such as title, description, time and duration
    console.log('implement');
  }

  destroyMeeting() {
    if (this.currentMeeting) {
      this.uiService.showSpinner();
      this.meetingService.delete(this.currentMeeting.id).subscribe({
        next: () => {
          this.router.navigateByUrl("dashboard");
        },
        complete: () => {
          this.uiService.hideSpinner()
          this.uiService.toastrShowWarning("Meeting deleted successfully")
        }
      });
    }
    // destroy meeting
    console.log('implement');
  }
}
