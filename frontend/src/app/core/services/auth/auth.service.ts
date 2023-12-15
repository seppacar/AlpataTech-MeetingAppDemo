import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserAuth } from '../../models/user/user-auth.model';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { UserRegistration } from '../../models/user/user-registration.model';
import { StorageService } from '../storage/storage.service';
import { User } from '../../models/user/user.model';
import { UserService } from '../user/user.service';

const baseUrl = 'http://localhost:5228/api/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<User | null> = new BehaviorSubject<User | null>(null);
  currentUser$: Observable<User | null> = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient, private storageService: StorageService, private userService: UserService) {
    // Fetch and set user if token is already set
    const token = storageService.getToken()

    if (token) {
      // Subscribe to the observable to initiate the network request
      this.userService.getUserDetailsWithToken().subscribe({
        next: (user) => {
          this.currentUserSubject.next(user)
          console.log("AuthService construct user set")
        },
        error: (error) => {
          console.error("Error fetching user details:", error);
        },
        complete: () => {
          // Handle completion cases
        }
      })
    }
  }

  login(userAuthObject: UserAuth): Observable<any> {
    // TODO: Handle errors
    return this.http.post(`${baseUrl}/login`, userAuthObject)
      .pipe(
        tap((response: any) => {
          // Successfull login set JWT in localstorage
          this.storageService.setToken(response.token)
          // Subscribe to the observable to initiate the network request
          this.userService.getUserDetailsWithToken().subscribe(
            user => {
              // TODO: Set
              this.currentUserSubject.next(user)
            },
            error => {
              console.error("Error fetching user details:", error);
            })
          // TODO: Centralised notifications show success here
        }),
      )
  }

  logout(): void {
    // Clear authentication token
    this.storageService.removeToken();

    // Set current user to null
    this.currentUserSubject.next(null);

    // TODO: invalidate token on the server
  }

  register(userRegisterObject: UserRegistration): Observable<any> {
    const formData = new FormData();

    // Append each field to the FormData object
    formData.append('profilePhoto', userRegisterObject.profilePicture || ''); // Use default value to prevent 'undefined'
    formData.append('FirstName', userRegisterObject.firstName);
    formData.append('LastName', userRegisterObject.lastName);
    formData.append('Email', userRegisterObject.email);
    formData.append('PhoneNumber', userRegisterObject.phoneNumber);
    formData.append('Password', userRegisterObject.password);
    return this.http.post(`${baseUrl}/register`, formData)
      .pipe(
        tap((response: any) => {
          // Successfull login set JWT in localstorage
          this.storageService.setToken(response.token)
          // Subscribe to the observable to initiate the network request
          this.userService.getUserDetailsWithToken().subscribe(
            user => {
              // TODO: Set
              this.currentUserSubject.next(user)
            },
            error => {
              console.error("Error fetching user details:", error);
            })
          // TODO: Centralised notifications show success here
        })
      )
  }

  getAuthToken() {
    return this.storageService.getToken();
  }

  isAuthenticated(): boolean {
    return !!this.storageService.getToken()
  }
}
