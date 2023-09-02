import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class LocationSearchService {

  private readonly baseApiUrl;

  constructor(private httpClient: HttpClient) {
    this.baseApiUrl = `${environment.apiUrl}Location`
  }

  public search(query: string) {
    return this.httpClient.get<Location[]>(`${this.baseApiUrl}/Search`, {params: {query: query}})
  }
}
