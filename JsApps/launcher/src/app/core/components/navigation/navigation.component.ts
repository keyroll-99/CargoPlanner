import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {NavigationStart, Router} from "@angular/router";
import {User} from "../../models/user";
import {PermissionEnum} from "../Enums/permission-enum";
import {NestedTreeControl} from "@angular/cdk/tree";
import {MatTreeNestedDataSource} from "@angular/material/tree";

interface NavigationNode {
  name: string
  permission: PermissionEnum,
  routerLink?: string
  children?: NavigationNode[]
}


const NAVIGATION_DATA: NavigationNode[] = [
  {
    name: "Locations",
    permission: PermissionEnum.Locations,
    children: [
      {name: "list", routerLink: "/locations", permission: PermissionEnum.Locations},
      {name: "map", routerLink: "/locations/map", permission: PermissionEnum.Locations}
    ]
  },
  {
    name: "Workers",
    permission: PermissionEnum.Workers
  },
  {
    name: "Cars",
    permission: PermissionEnum.Cars
  },
  {
    name: "Cargoes",
    permission: PermissionEnum.Cargoes
  }
]


@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {
  treeControl = new NestedTreeControl<NavigationNode>(node => node.children)
  dataSource = new MatTreeNestedDataSource<NavigationNode>()

  protected isAuthorized: boolean = false;
  protected user: User | undefined
  protected readonly PermissionEnum = PermissionEnum;

  constructor(private authService: AuthService, private router: Router) {
    this.dataSource.data = NAVIGATION_DATA
  }

  ngOnInit() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.isAuthorized = this.authService.isAuthenticated();
        this.setUser()
      }
    })
  }

  hasChild = (_: number, node: NavigationNode) => !!node.children && node.children.length > 0;

  hasAccess(permission: PermissionEnum): boolean {
    if (this.user?.permission === undefined) {
      return false;
    }
    return (this.user.permission & permission) > 0;
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
