import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/auth/login/login/login.component';
import { RegisterComponent } from './pages/auth/register/register.component';
import { CreateMeetingComponent } from './pages/create-meeting/create-meeting.component';
import { authGuard } from './core/guards/auth.guard';
import { DemoComponent } from './pages/demo/demo.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

const routes: Routes = [
  {
    path: "demo",
    component: DemoComponent,
  },
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
    path: "dashboard",
    component: DashboardComponent,
    canActivate: [authGuard], // Apply AuthGuard to protect the route
  },
  {
    path: "organize-meeting",
    component: CreateMeetingComponent,
    canActivate: [authGuard], // Apply AuthGuard to protect the route
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
