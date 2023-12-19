import { User } from "../user/user.model"
import { MeetingDocument } from "./meeting-document.model"
import { MeetingParticipant } from "./meeting-participant.model"

export class Meeting {
  id: number
  title: string
  description: string
  organizer: User
  participants: MeetingParticipant[]
  documents: MeetingDocument[]
  startTime: string
  endTime: string

  constructor(data: any) {
    this.id = data.id
    this.title = data.title;
    this.description = data.description;
    this.organizer = data.organizer
    this.participants = data.participants
    this.documents = data.documents
    this.startTime = data.startTime;
    this.endTime = data.endTime;
  }
}