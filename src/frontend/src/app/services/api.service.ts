import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ClientResponse, ClientSummaryResponse } from '../models/client.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5008/api'; // Direct API URL for demo

  constructor(private http: HttpClient) { }

  getClient(clientId: number): Observable<ClientResponse> {
    return this.http.get<ClientResponse>(`${this.baseUrl}/Client/${clientId}`);
  }

  getClientSummary(clientId: number): Observable<ClientSummaryResponse> {
    return this.http.get<ClientSummaryResponse>(`${this.baseUrl}/summaries/${clientId}`).pipe(
      catchError((error) => {
        // If Ollama is not available, return a mock response
        console.warn('Ollama service not available, using mock data:', error);
        return of(this.getMockSummary(clientId));
      })
    );
  }

  private getMockSummary(clientId: number): ClientSummaryResponse {
    return {
      client_Summary: {
        financial_Position: {
          assets: `Client ${clientId} has a diversified portfolio including real estate, investment holdings, and personal assets. Total estimated asset value represents a strong financial foundation.`,
          liabilities: `Manageable debt levels including mortgage obligations and minimal credit commitments. Debt-to-asset ratio remains within conservative parameters.`,
          income_Expenditure: `Stable income stream with controlled expenses, maintaining positive cash flow. Annual surplus available for further investment and savings.`,
          pensions: `Well-positioned pension arrangements contributing to long-term retirement security and financial independence goals.`
        },
        progress_Since_Last_Meeting: `Client has made significant progress toward their financial objectives since our last review. Investment performance has remained stable with moderate growth across key holdings.`,
        financial_Goals: `Primary objectives include wealth preservation, capital growth, and preparation for major life events. Timeline-based goal achievement tracking shows positive momentum.`,
        recommendations_And_Next_Steps: `Continue current investment strategy with minor portfolio rebalancing. Schedule quarterly reviews to monitor progress and adjust allocations as needed. Consider tax-efficient savings opportunities.`,
        overall_Summary: `Client maintains a strong financial position with well-diversified assets and manageable liabilities. Current strategy aligns well with stated objectives and risk tolerance.`
      },
      caveats: [
        'AI Summary - Demo Mode',
        'Ollama service unavailable',
        'Mock data for demonstration',
        'Connect Ollama for real AI analysis'
      ]
    };
  }
}