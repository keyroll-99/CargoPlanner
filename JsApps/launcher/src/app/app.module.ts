import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatInputModule} from "@angular/material/input";
import {ReactiveFormsModule} from "@angular/forms";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {MatRippleModule} from "@angular/material/core";
import {MatButtonModule} from "@angular/material/button";
import {SignInComponent} from './features/authorization/pages/sign-in/sign-in.component';
import {HomeComponent} from './features/home/pages/home/home.component';
import {NavigationComponent} from './core/components/navigation/navigation.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatCardModule} from "@angular/material/card";
import {NgIconsModule} from "@ng-icons/core";
import {bootstrapArrowRightShort} from '@ng-icons/bootstrap-icons';
import {MatListModule} from "@angular/material/list";
import {AuthInterceptor} from "./core/interceptors/auth.interceptor";
import {LeafletModule} from "@asymmetrik/ngx-leaflet";
import {MapComponent} from './features/location/component/map/map.component';
import {LocationSearchComponent} from './features/location/component/location-search/location-search.component';
import {MainLocationComponent} from './features/location/pages/main/main-location/main-location.component';
import {MatTreeModule} from "@angular/material/tree";
import {MatIconModule} from "@angular/material/icon";
import {MatExpansionModule} from "@angular/material/expansion";
import {
  NavigationElementsComponent
} from "./core/components/navigation/navigation-elements/navigation-elements.component";

export const httpInterceptorProviders = [
  {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
];

@NgModule({
  declarations: [AppComponent, SignInComponent, HomeComponent, NavigationComponent, MapComponent, LocationSearchComponent, MainLocationComponent, NavigationElementsComponent, NavigationElementsComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatInputModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatRippleModule,
    MatButtonModule,
    MatSidenavModule,
    MatCardModule,
    NgIconsModule.withIcons({bootstrapArrowRightShort}),
    MatListModule,
    LeafletModule,
    MatTreeModule,
    MatIconModule,
    MatExpansionModule

  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent],
})
export class AppModule {
}
