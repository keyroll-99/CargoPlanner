import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatInputModule} from "@angular/material/input";
import {ReactiveFormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import {MatRippleModule} from "@angular/material/core";
import {MatButtonModule} from "@angular/material/button";
import { SignInComponent } from './features/authorization/pages/sign-in/sign-in.component';
import { HomeComponent } from './features/home/pages/home/home.component';
import { NavigationComponent } from './core/components/navigation/navigation.component';
import {MatSidenavModule} from "@angular/material/sidenav";

@NgModule({
  declarations: [AppComponent, SignInComponent, HomeComponent, NavigationComponent],
  imports: [BrowserModule, AppRoutingModule, BrowserAnimationsModule, MatInputModule, ReactiveFormsModule, HttpClientModule, MatRippleModule, MatButtonModule, MatSidenavModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
