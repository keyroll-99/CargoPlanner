import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {NavigationStart, Router} from "@angular/router";
import {User} from "../../models/user";
import {PermissionEnum} from "../Enums/permission-enum";

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {
  isAuthorized: boolean = false;
  user: User | undefined

  constructor(private authService: AuthService, private router: Router) {
  }

  ngOnInit() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.isAuthorized = this.authService.isAuthenticated();
        if(this.isAuthorized && this.user === undefined){
          this.user = this.authService.userModel;
        }

      }})

  }

  hasAccess(permission: PermissionEnum): boolean{
    if(this.user?.permission === undefined){
      return  false;
    }
    console.log(this.user);
    return (this.user.permission & permission) > 0;
  }


  protected readonly PermissionEnum = PermissionEnum;
}
