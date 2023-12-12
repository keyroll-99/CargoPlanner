import {Component, Input, OnInit} from '@angular/core';
import {User} from "../../../models/user";
import {PermissionEnum} from "../../enums/permission-enum";
import NAVIGATION_SCHEMA from "./navitaionSchema";



@Component({
  selector: 'app-navigation-elements',
  templateUrl: './navigation-elements.component.html',
  styleUrls: ['./navigation-elements.component.scss']
})
export class NavigationElementsComponent implements OnInit{
  @Input({required: true}) user!: User;
  navigationSchema = NAVIGATION_SCHEMA

  ngOnInit(): void {
  }

  hasAccess(permission: PermissionEnum): boolean {
    if (this.user?.permission === undefined) {
      return false;
    }
    return (this.user.permission & permission) > 0;
  }



}
