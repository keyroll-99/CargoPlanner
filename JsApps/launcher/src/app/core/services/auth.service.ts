import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import SingInForm from "../../features/authorization/models/SingInForm";
import {environment} from "../../../environments/environment";
import AuthModel from "../models/AuthModel";
import {retry} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authModel: AuthModel | undefined = undefined;
  private readonly accessTokenKey = "accessToken"

  constructor(private httpClient: HttpClient) {
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem(this.accessTokenKey);

    if (token !== null && this.authModel === undefined) {
      this.refreshToken();
    }

    return token !== null;
  }

  public singIn(form: SingInForm) {
    const baseUrl = environment.apiUrl
    return this.httpClient.post<AuthModel>(`${baseUrl}Users/Auth/SignIn`, form, {withCredentials: true});
  }

  public setAuth(authModel: AuthModel) {
    localStorage.setItem(this.accessTokenKey, authModel.accessToken);
    this.authModel = authModel;
  }

  public getAuth(): AuthModel | undefined {
    return this.authModel;
  }

  public refreshToken() {
    const baseUrl = environment.apiUrl
    this.httpClient.post<AuthModel>(`${baseUrl}Users/Auth/Refresh`, undefined, {withCredentials: true}).subscribe({
      next: (resp) => this.setAuth(resp),
      error: resp => localStorage.removeItem(this.accessTokenKey)
    })
  }
}
