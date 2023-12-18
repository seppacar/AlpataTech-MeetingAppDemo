import { Component } from '@angular/core';
import { UserService } from '../../core/services/user/user.service';
import { UIService } from '../../core/services/ui/ui.service';

@Component({
  selector: 'app-demo',
  templateUrl: './demo.component.html',
  styleUrl: './demo.component.scss'
})
export class DemoComponent {

  constructor(private uiService: UIService){}
  ngOnInit(){
    this.uiService.showSpinner()
  }
}
