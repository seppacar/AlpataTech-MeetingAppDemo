import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserAuth } from '../../models/user/user-auth.model';
import { Observable, catchError, throwError } from 'rxjs';
import { UserRegistration } from '../../models/user/user-registration.model';
import { StorageService } from '../storage/storage.service';

const baseUrl = 'http://localhost:5228/api/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private storageService: StorageService) { }

  login(userAuthObject : UserAuth): Observable<any> {
    return this.http.post(`${baseUrl}/login`, userAuthObject)
  }

  register(userRegisterObject : UserRegistration) : Observable<any> {
    const formData = new FormData();

    // Append each field to the FormData object
    formData.append('profilePhoto', userRegisterObject.profilePicture || ''); // Use default value to prevent 'undefined'
    formData.append('FirstName', userRegisterObject.firstName);
    formData.append('LastName', userRegisterObject.lastName);
    formData.append('Email', userRegisterObject.email);
    formData.append('PhoneNumber', userRegisterObject.phoneNumber);
    formData.append('Password', userRegisterObject.password);
    return this.http.post(`${baseUrl}/register`, formData)
  }

  getAuthToken(){
    return this.storageService.getToken();
  }
}
