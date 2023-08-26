import {Injectable} from '@angular/core';
import {
  HttpClient,
  HttpContext,
  HttpContextToken,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import {BehaviorSubject, filter, Observable, switchMap, take} from 'rxjs';
import {AuthService} from "../services/auth.service";
import AuthModel from "../models/authModel";
import {environment} from "../../../environments/environment";

export const IGNORE_AUTH_TOKEN = new HttpContextToken(() => false);


@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(
    null
  );
  private isRefreshingInProgress: boolean = false;
  private baseUrl: string

  constructor(private authService: AuthService, private httpClient: HttpClient) {
    this.baseUrl = environment.apiUrl;
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.context.get(IGNORE_AUTH_TOKEN)) {
      return next.handle(request)
    }

    const accessToken = localStorage.getItem(AuthService.accessTokenKey);
    if (accessToken !== null) {
      const authModel = this.authService.authModel;
      if (authModel === undefined || authModel.expires < new Date()) {
        return this.refreshToken(request, next);
      }
      request = this.getRequestWithAuthHeader(request, accessToken);
    }

    return next.handle(request);
  }


  private refreshToken(request: HttpRequest<unknown>, next: HttpHandler) {
    if (!this.isRefreshingInProgress) {
      this.isRefreshingInProgress = true
      return this.httpClient.post<AuthModel>(`${this.baseUrl}Users/Auth/Refresh`, undefined, {
        withCredentials: true,
        context: new HttpContext().set(IGNORE_AUTH_TOKEN, true)
      }).pipe(switchMap((response: AuthModel) => {
        this.authService.setAuth(response);
        this.isRefreshingInProgress = false;
        this.refreshTokenSubject.next(response.accessToken);
        return next.handle(this.getRequestWithAuthHeader(request, response.accessToken))
      }))
    } else {
      return this.refreshTokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap((accessToken) => {
          return next.handle(this.getRequestWithAuthHeader(request, accessToken))
        })
      )
    }
  }

  private getRequestWithAuthHeader(request: HttpRequest<unknown>, accessToken: string): HttpRequest<unknown> {
    return request.clone({setHeaders: {Authorization: `Bearer ${accessToken}`}})
  }
}
