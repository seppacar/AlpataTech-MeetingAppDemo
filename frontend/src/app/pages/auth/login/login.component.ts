import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth/auth.service';
import { StorageService } from '../../../core/services/storage/storage.service';
import { UserAuth } from '../../../core/models/user/user-auth.model';
import { Router } from '@angular/router';
import { PageService } from '../../../core/services/page/page.service';
import { UIService } from '../../../core/services/ui/ui.service';
import { finalize } from 'rxjs';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  isLoading = false;
  userAuthObject = new UserAuth({});

  constructor(private authService: AuthService, private storageService: StorageService, private pageService: PageService, private uiService: UIService, private router: Router) { }

  ngOnInit(): void {
    this.pageService.setPageInfo('Sign in', 'Please enter login details to continue')
  }

  onSubmit() {
    this.uiService.showSpinner()
    
    this.authService.login(this.userAuthObject)
      .pipe(
        finalize(() => {this.uiService.hideSpinner()})
      )
      .subscribe({
        next: () => {
          console.log("Succesfully logged in")
        },
        error: (error) => {
          this.uiService.hideSpinner()
          console.error("Error logging in:", error.error.message);
        },
        complete: () => {
          this.router.navigateByUrl('/dashboard')
        }
      })
  }
}
