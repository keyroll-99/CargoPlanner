import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpContextToken, HttpContext, HttpClient
} from '@angular/common/http';
import { Observable } from 'rxjs';
import {AuthService} from "../services/auth.service";
import AuthModel from "../models/authModel";
import {environment} from "../../../environments/environment";

export const IGNORE_AUTH_TOKEN = new HttpContextToken(() => false);


@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshingInProgress: boolean = false;
  private baseUrl: string

  constructor(private authService: AuthService, private httpClient: HttpClient) {
    this.baseUrl = environment.apiUrl;
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if(request.context.get(IGNORE_AUTH_TOKEN)){
      return next.handle(request)
    }

    const jwt = localStorage.getItem("JWT");
    if(jwt !== null){
      const authModel = this.authService.authModel;
      if(authModel === undefined || authModel.expires < new Date()){
        return  this.refreshToken(request, next);
      }

    }


    return next.handle(request);
  }


  private refreshToken(request: HttpRequest<unknown>, next: HttpHandler){
    //TODO finish refresh 
    if(!this.isRefreshingInProgress) {
      this.isRefreshingInProgress = true
      return this.httpClient.post<AuthModel>(`${this.baseUrl}/Auth/Refresh`, undefined, {
        withCredentials: true,
        context: new HttpContext().set(IGNORE_AUTH_TOKEN, true)
      }).pipe()
    }

  }
}
