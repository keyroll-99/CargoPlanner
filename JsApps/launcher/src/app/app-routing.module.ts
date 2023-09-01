import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {authGuard} from "./core/guards/auth.guard";
import {SignInComponent} from "./features/authorization/pages/sign-in/sign-in.component";
import {HomeComponent} from "./features/home/pages/home/home.component";
import {loginPageGuard} from "./core/guards/login-page.guard";
import {LocationComponent} from "./features/location/pages/location.component";

const routes: Routes = [
  { path: 'login', component: SignInComponent, canActivate:[loginPageGuard]},
  {path: "locations", component: LocationComponent, canActivate: [authGuard]},
  { path: '', component: HomeComponent, canActivate:[authGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
