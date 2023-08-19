import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {authGuard} from "./core/guards/auth.guard";
import {SignInComponent} from "./features/authorization/pages/sign-in/sign-in.component";
import {HomeComponent} from "./features/home/pages/home/home.component";

const routes: Routes = [
  { path: 'login', component: SignInComponent },
  { path: '', component: HomeComponent, canActivate:[authGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
