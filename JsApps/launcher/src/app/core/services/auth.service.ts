import {Injectable} from '@angular/core';
import {HttpClient, HttpContext, HttpResponse} from "@angular/common/http";
import SingInForm from "../../features/authorization/models/SingInForm";
import {environment} from "../../../environments/environment";
import AuthModel from "../models/authModel";
import {Observable, retry} from "rxjs";
import {User} from "../models/user";
import {IGNORE_AUTH_TOKEN} from "../interceptors/auth.interceptor";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _authModel: AuthModel | undefined = undefined;
  private _userModel: User | undefined = undefined;
  private isRefreshingInProgress = false;


  private readonly baseUrl;
  private readonly accessTokenKey = "accessToken"


  constructor(private httpClient: HttpClient) {
    this.baseUrl = `${environment.apiUrl}Users`;
  }

  get authModel(): AuthModel | undefined {
    return this._authModel;
  }

  get userModel(): User | undefined {
    return this._userModel;
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem(this.accessTokenKey);

    return token !== null;
  }

  public singIn(form: SingInForm) {
    return this.httpClient.post<AuthModel>(`${this.baseUrl}/Auth/SignIn`, form, {withCredentials: true});
  }

  public setAuth(authModel: AuthModel) {
    localStorage.setItem(this.accessTokenKey, authModel.accessToken);
    this._authModel = authModel;
  }

  public updateUserMode(){
    this.httpClient.get<User>(`${this.baseUrl}/User/Me`).subscribe({
      next: (resp) => {
        this._userModel = resp
      },
      error: (error: HttpResponse<any>) => {
        console.log(error)
      },
    })
  }

}
