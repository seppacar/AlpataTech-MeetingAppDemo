import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class UIService {

  constructor(private spinner: NgxSpinnerService) { }

  showSpinner() {
    this.spinner.show(undefined, {
      type: 'ball-scale-multiple',
    });
  }
  hideSpinner(){
    this.spinner.hide()
  }
}
