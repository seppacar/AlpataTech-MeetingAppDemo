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
        if (error.status == 500){
          this.toastr.error("Internal server error")
        }
        else if (error.status == 403){
          this.toastr.error("You don't have rights to view this content")
        }
        else if(error.status == 401){
          this.toastr.error("You must authorize to view this content")
        }
        else if (error.error.error){
          this.toastr.error(error.error.error)
        }
        else if(error.error && !error.error.errors){
          this.toastr.error(error.error)
        }
        else if(error.error.message){
          this.toastr.error(error.error.message)
        }
        else{
          console.log(error)
          this.toastr.error("Something unexpected happened!")
        }

        throw error; // rethrow the error to continue propagating it
      }),
    )
  }
}