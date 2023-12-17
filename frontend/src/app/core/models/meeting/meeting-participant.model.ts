export class MeetingParticipant {
  id: string;
  userId: string;
  firstName: string;
  lastName: string;

  constructor(data: any) {
    this.id = data.id;
    this.userId = data.userId
    this.firstName = data.firstName;
    this.lastName = data.lastName;
  }
}