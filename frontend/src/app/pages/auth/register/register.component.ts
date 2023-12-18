import { Component } from '@angular/core';
import { UserRegistration } from '../../../core/models/user/user-registration.model';
import { AuthService } from '../../../core/services/auth/auth.service';
import { Router } from '@angular/router';
import { PageService } from '../../../core/services/page/page.service';
import { UIService } from '../../../core/services/ui/ui.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  /* TODO: Set default profile image */
  profilePictureURL = null
  userRegistrationObject = new UserRegistration({});

  constructor(private authService: AuthService, private uiService: UIService, private pageService: PageService, private router: Router) {

  }

  ngOnInit(): void {
    this.pageService.setPageInfo('Sign up', 'Lorem ipsum dolor sit amet')
  }

  handleProfilePictureChange(event: Event) {
    const fileReader = new FileReader()
    const element = event.currentTarget as HTMLInputElement;
    const selectedFiles: FileList | null = element.files;
    if (selectedFiles && selectedFiles.length > 0) {
      const currentPicture = selectedFiles[0]

      fileReader.onload = (e: any) => {
        // Set preview
        this.profilePictureURL = e.target.result
        // Set payload
      };
      fileReader.readAsDataURL(currentPicture);

      this.userRegistrationObject.profilePicture = currentPicture;
    }
  }

  onSubmit() {
    this.uiService.showSpinner()
    this.authService.register(this.userRegistrationObject)
      .pipe(finalize(() => {
        this.uiService.hideSpinner()
      }))
      .subscribe(
        {
          complete: () => {
            this.router.navigateByUrl('/dashboard')
          }
        }
      )
  }
}
