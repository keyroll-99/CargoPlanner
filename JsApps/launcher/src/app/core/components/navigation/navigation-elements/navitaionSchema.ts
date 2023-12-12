import {PermissionEnum} from "../../enums/permission-enum";

interface NavigationElement{
  name: string
  permission: PermissionEnum
  links: NavigationLink[]
}

interface NavigationLink {
  name: string
  routerLink?: string
}


const NAVIGATION_SCHEMA: NavigationElement[] = [
  {
    name: "Workers",
    permission: PermissionEnum.Workers,
    links: [
      {name: "Workers", routerLink: "/locations"},
    ],
  },
  {
    name: "Locations",
    permission: PermissionEnum.Locations,
    links: [
      {name: "list", routerLink: "/locations"},
      {name: "map", routerLink: "/locations/map"}
    ],
  },
]


export default NAVIGATION_SCHEMA
