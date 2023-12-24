import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Meeting } from '../../models/meeting/meeting.model';
import { MeetingParticipant } from '../../models/meeting/meeting-participant.model';
import { environment } from '../../../../environments/environment.development';

const baseUrl = environment.apiUrl + '/meetings';

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

  getParticipatedMeetings(): Observable<Meeting[]> {
    return this.http.get<Meeting[]>(`${baseUrl}/getParticipatedMeetings`);
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

  removeMeetingParticipant(meetingId: number, meetingParticipantUserId: number){
    return this.http.delete(`${baseUrl}/${meetingId}/participants/${meetingParticipantUserId}`);
  }

  addMeetingDocument(meetingId: number, meetingDocumentFile: File){
    const formData = new FormData();
    formData.append('meetingDocumentFile', meetingDocumentFile)
    return this.http.post<Meeting>(`${baseUrl}/${meetingId}/documents`, formData);
  }

  removeMeetingDocument(meetingId: number, meetingDocumentId: number){
    return this.http.delete(`${baseUrl}/${meetingId}/documents/${meetingDocumentId}`);
  }

  getMeetingDocument(meetingId: number, meetingDocumentId: number): Observable<Blob>{
    return this.http.get(`${baseUrl}/${meetingId}/documents/${meetingDocumentId}/download`, { responseType: 'blob' });
  }
}
