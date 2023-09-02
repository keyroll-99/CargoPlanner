import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import SingInForm from "../../features/authorization/models/SingInForm";
import {environment} from "../../../environments/environment";
import AuthModel from "../models/authModel";
import {tap} from "rxjs";
import {User} from "../models/user";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public static readonly accessTokenKey = "accessToken"


  private _authModel: AuthModel | undefined = undefined;
  private _userModel: User | undefined = undefined;


  private readonly baseUrl;


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
    const token = localStorage.getItem(AuthService.accessTokenKey);

    return token !== null;
  }

  public singIn(form: SingInForm) {
    return this.httpClient.post<AuthModel>(`${this.baseUrl}/Auth/SignIn`, form, {withCredentials: true});
  }

  public setAuth(authModel: AuthModel) {
    localStorage.setItem(AuthService.accessTokenKey, authModel.accessToken);
    this._authModel = authModel;
  }

  public updateUserModel() {
    return this.httpClient.get<User>(`${this.baseUrl}/User/Me`).pipe(
      tap({
        next: resp => {
          this._userModel = resp
        },
        error: (error: string) => {
          // todo logout?
          console.log("error from tap")
          console.log(error)
        }
      })
    )
  }

  //TODO: Request to backend to remove cookie
  public logout() {
    localStorage.removeItem(AuthService.accessTokenKey);
  }

}
