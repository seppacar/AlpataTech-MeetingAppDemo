import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/auth/login/login/login.component';
import { RegisterComponent } from './pages/auth/register/register/register.component';
import { CreateMeetingComponent } from './pages/create-meeting/create-meeting.component';

const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
  },
  {
    path: "sign-in",
    component: LoginComponent,
  },
  {
    path: "sign-up",
    component: RegisterComponent,
  },
  {
    // Protect test this route
    path: "organize-meeting",
    component: CreateMeetingComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
