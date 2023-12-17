import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Meeting } from '../../models/meeting/meeting.model';
import { MeetingParticipant } from '../../models/meeting/meeting-participant.model';

const baseUrl = 'http://localhost:5228/api/meetings';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Meeting[]> {
    return this.http.get<Meeting[]>(baseUrl);
  }

  get(id: number): Observable<Meeting> {
    return this.http.get<Meeting>(`${baseUrl}/${id}`);
  }

  create(data: any): Observable<any> {
    return this.http.post(baseUrl, data);
  }

  update(id: number, data: any): Observable<any> {
    return this.http.put(`${baseUrl}/${id}`, data);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${baseUrl}/${id}`);
  }

  addMeetingParticipant(meetingId: number, meetingParticipant: MeetingParticipant){
    return this.http.post<Meeting>(`${baseUrl}/${meetingId}/participants`, meetingParticipant);
  }

  addMeetingDocument(meetingId: number, meetingDocumentFile: File){
    const formData = new FormData();
    formData.append('meetingDocumentFile', meetingDocumentFile)
    return this.http.post<Meeting>(`${baseUrl}/${meetingId}/documents`, formData);
  }

  getMeetingDocument(meetingId: number, documentId: number){
    return this.http.get<Meeting>(`${baseUrl}/${meetingId}/documents/${documentId}`);
  }
}
