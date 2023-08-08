import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import SingInForm from "./contracts/SingInForm";
import {Router} from "@angular/router";
import {Environment} from "@angular/cli/lib/config/workspace-schema";
import {environment} from "../../../environments/environment";
import {catchError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient, private router: Router) { }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem("jwt");

    return token !== null;
  }

  public singIn(form: SingInForm){
    const baseUrl = environment.apiUrl
    return  this.httpClient.post<{accessToken: string}>(`${baseUrl}/Users/Auth/SignIn`, form);
  }

}
