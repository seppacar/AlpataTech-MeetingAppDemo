import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../../models/user/user.model';
import { environment } from '../../../../environments/environment.development';


const baseUrl = environment.apiUrl + '/users';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(baseUrl);
  }

  get(id: number): Observable<User> {
    return this.http.get<User>(`${baseUrl}/${id}`);
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

  getProfilePicture(id: number): Observable<Blob> {
    return this.http.get(`${baseUrl}/${id}/profilePicture`, { responseType: 'blob' });
  }

  getUserDetailsWithToken(): Observable<User>{
    console.log("USERSERV")
    return this.http.get<User>(`${baseUrl}/getUserDetails`)
  }
}
