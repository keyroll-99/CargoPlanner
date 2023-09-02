import {Address} from "./address";

export interface Location {
  lat: number,
  lon: number,
  displayName: string,
  osmId: number,
  name: number,
  address: Address
}
