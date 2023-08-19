import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import SingInForm from "../../features/authorization/models/SingInForm";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem("jwt");

    return token !== null;
  }

  public singIn(form: SingInForm){
    const baseUrl = environment.apiUrl
    return  this.httpClient.post<{accessToken: string}>(`${baseUrl}Users/Auth/SignIn`, form);
  }

  public setJwt(jwt: string){
    localStorage.setItem("jwt", jwt);
  }
}
