import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth/auth.service';
import { StorageService } from '../../../core/services/storage/storage.service';
import { UserAuth } from '../../../core/models/user/user-auth.model';
import { Router } from '@angular/router';
import { PageService } from '../../../core/services/page/page.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  userAuthObject = new UserAuth({});

  constructor(private authService: AuthService, private storageService: StorageService, private pageService: PageService, private router: Router) { }

  ngOnInit() : void {
    this.pageService.setPageInfo('Sign in', 'Please enter login details to continue')
  }

  onSubmit() {
    this.authService.login(this.userAuthObject)
    .subscribe({
      next: () => {
       console.log("Succesfully logged in")
      },
      error: (error) => {
        // TODO: wth I was thinking there IDK but must check backend
        console.error("Error logging in:", error.error.error);
      },
      complete: () => {
        this.router.navigateByUrl('/dashboard')
      }
    })
  }
}
