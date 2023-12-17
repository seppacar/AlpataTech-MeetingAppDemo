import { Component } from '@angular/core';
import { UserService } from '../../core/services/user/user.service';

@Component({
  selector: 'app-demo',
  templateUrl: './demo.component.html',
  styleUrl: './demo.component.scss'
})
export class DemoComponent {

  constructor(private userService: UserService){}

  getProfilePicture(){
    const profilePicture = this.userService.getProfilePicture(1)
    console.log(profilePicture)
  }
}
