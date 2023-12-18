import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-demo',
  templateUrl: './demo.component.html',
  styleUrl: './demo.component.scss'
})
export class DemoComponent {

  constructor(private toastr: ToastrService){}
  ngOnInit(){
    this.toastr.error('Hello world!', 'Toastr fun!');
  }
}
