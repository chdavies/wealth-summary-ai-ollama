import { Client, WealthSummaryResponse, ClientSummary } from '../types/api';

const API_BASE_URL = process.env.REACT_APP_API_URL || 'http://localhost:5000/api';

class ApiService {
  private async fetchData<T>(endpoint: string): Promise<T> {
    const response = await fetch(`${API_BASE_URL}${endpoint}`);
    
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    
    return response.json();
  }

  async getClient(clientId: number): Promise<Client> {
    return this.fetchData<Client>(`/client/${clientId}`);
  }

  async getWealthSummary(clientId: number): Promise<WealthSummaryResponse> {
    return this.fetchData<WealthSummaryResponse>(`/summaries/${clientId}`);
  }

  parseWealthSummary(rawResponse: WealthSummaryResponse): ClientSummary | null {
    try {
      if (!rawResponse.content) {
        return null;
      }
      
      // The API returns raw JSON string, so we need to parse it
      const parsed = JSON.parse(rawResponse.content);
      return parsed as ClientSummary;
    } catch (error) {
      console.error('Failed to parse wealth summary:', error);
      return null;
    }
  }
}

export const apiService = new ApiService();