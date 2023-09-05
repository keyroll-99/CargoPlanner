import { Component } from '@angular/core';
import * as Leaflet from "leaflet";

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent {
  map!: Leaflet.Map;
  markers: Leaflet.Marker[] = [];
  options = {
    layers: [
      Leaflet.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>',
      })
    ],
    zoom: 5,
    center: { lat: 50.012100, lng: 20.985842 }
  }

  generateMarker(data: any, index: number) {
    return Leaflet.marker(data.position, {icon: new Leaflet.Icon({iconUrl: "/assets/leaflet/marker-icon.png"})})
      .on('click', (event) => this.markerClicked(event, index))
      .on('dragend', (event) => this.markerDragEnd(event, index));
  }

  onMapReady($event: Leaflet.Map) {
    this.map = $event;

    const marker = this.generateMarker({
      position: {
        lat: 50.012100,
        lng: 20.985842,
      },
    },0);

    marker.addTo(this.map).bindPopup("kupa").bindTooltip("dupa");
  }

  mapClicked($event: any) {
    console.log($event.latlng.lat, $event.latlng.lng);
  }

  markerClicked($event: any, index: number) {
    console.log($event.latlng.lat, $event.latlng.lng);
  }

  markerDragEnd($event: any, index: number) {
    console.log($event.target.getLatLng());
  }
}
