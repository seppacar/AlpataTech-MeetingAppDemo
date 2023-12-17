import { Component } from '@angular/core';
import { User } from '../../models/user/user.model';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';
import { PageService } from '../../services/page/page.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  pageName: string | null = null;
  pageDescription: string | null = null;
  currentUser: User | null = null;

  constructor(private authService: AuthService, private pageService: PageService, private router: Router) {}

  ngOnInit(): void {
    this.pageService.pageName$.subscribe((name) => {this.pageName = name})
    this.pageService.pageDescription$.subscribe((description) => {this.pageDescription = description})
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
  }


  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
