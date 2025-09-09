import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ClientResponse, ClientSummaryResponse } from '../models/client.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = '/api'; // This will be proxied to the backend

  constructor(private http: HttpClient) { }

  getClient(clientId: number): Observable<ClientResponse> {
    return this.http.get<ClientResponse>(`${this.baseUrl}/client/${clientId}`);
  }

  getClientSummary(clientId: number): Observable<ClientSummaryResponse> {
    return this.http.get<ClientSummaryResponse>(`${this.baseUrl}/summaries/${clientId}`);
  }
}