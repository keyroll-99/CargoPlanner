import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {NavigationStart, Router} from "@angular/router";
import {User} from "../../models/user";
import {PermissionEnum} from "../enums/permission-enum";
import {NestedTreeControl} from "@angular/cdk/tree";
import {MatTreeNestedDataSource} from "@angular/material/tree";


@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {


  protected isAuthorized: boolean = false;
  protected user: User | undefined

  constructor(private authService: AuthService, private router: Router) {
  }

  ngOnInit() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.isAuthorized = this.authService.isAuthenticated();
        this.setUser()
      }
    })
  }

  private setUser() {
    if (this.isAuthorized && this.user === undefined) {
      this.user = this.authService.userModel
      if (this.user === undefined) {
        this.authService.updateUserModel().subscribe({
          next: resp => {
            this.user = resp
          }
        })
      }
    }
  }
}
