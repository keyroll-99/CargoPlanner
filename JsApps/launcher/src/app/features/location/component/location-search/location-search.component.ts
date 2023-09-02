import {Component, OnInit} from '@angular/core';
import {LocationSearchService} from "../../services/location-search.service";

@Component({
  selector: 'app-location-search',
  templateUrl: './location-search.component.html',
  styleUrls: ['./location-search.component.scss']
})
export class LocationSearchComponent implements OnInit {
  protected query: string | undefined;
  protected locations: Location[] = []


  constructor(private locationService: LocationSearchService) {
  }

  ngOnInit(): void {
  }

  protected search() {
    if (this.query !== undefined) {
      this.locationService.search(this.query).subscribe({
        next: response => {
          this.locations = response
        }
      })
    }
  }


}
