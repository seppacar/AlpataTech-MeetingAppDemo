import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class UIService {

  constructor(private spinner: NgxSpinnerService, private toastr: ToastrService) { }

  showSpinner() {
    this.spinner.show(undefined, {
      type: 'ball-scale-multiple',
    });
  }
  hideSpinner(){
    this.spinner.hide()
  }

  toastrShowWarning(message: string){
    this.toastr.warning(message)
  }

  toastrShowSuccess(message: string){
    this.toastr.success(message)
  }
  toastrShowError(message: string){
    this.toastr.error(message)
  }
}
