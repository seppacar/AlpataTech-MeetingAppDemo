export class MeetingParticipant {
  id: number;
  userId: number;
  firstName: string;
  lastName: string;
  email: string;
  profileImageUrl: string;

  constructor(data: any) {
    this.id = data.id;
    this.userId = data.userId
    this.firstName = data.firstName;
    this.lastName = data.lastName;
    this.email = data.email;
    this.profileImageUrl = ''
  }
}