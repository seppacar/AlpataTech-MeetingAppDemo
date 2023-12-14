import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { StorageService } from '../../../../core/services/storage/storage.service';
import { UserAuth } from '../../../../core/models/user/user-auth.model';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  userAuthObject = new UserAuth();

  constructor(private authService: AuthService, private storageService: StorageService) { }

  onSubmit() {
    this.authService.login(this.userAuthObject)
      .subscribe(
        (response) => {
          this.storageService.setToken(response.token);
        },
      )
  }
}
