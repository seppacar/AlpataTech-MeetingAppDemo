import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private toastr: ToastrService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request).
    pipe(
      catchError((error: HttpErrorResponse) => {
        // Handle error here
        if (error.error.error){
          this.toastr.error(error.error.error)
        }
        else if(error.error.message){
          this.toastr.error(error.error.message)
        }
        else{
          this.toastr.error("Something unexpected happened!")
        }

        throw error; // rethrow the error to continue propagating it
      }),
    )
  }
}