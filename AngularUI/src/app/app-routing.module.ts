import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthGuard } from './helpers/auth.guard';
import { MainComponent } from './main/main.component';
import { DetailsComponent } from './user/components/details/details.component';
import { LoginComponent } from './user/components/login/login.component';
import { SignupComponent } from './user/components/signup/signup.component';

const routes: Routes = [
  {path: '', component: MainComponent},
  {path: 'login', component: LoginComponent},
  {path: 'signup', component: SignupComponent},
  {path: 'details', component: DetailsComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
