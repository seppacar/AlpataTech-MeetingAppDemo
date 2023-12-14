import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StorageService } from '../services/storage/storage.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private storageService: StorageService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Do something with the request here
    console.log("intercepted", this.storageService.getToken())
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.storageService.getToken()}`
      }
    });

    return next.handle(request);
  }
}