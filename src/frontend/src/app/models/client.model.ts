export interface ClientResponse {
  clientId: number;
  name: string;
  dateOfBirth: string;
}

export interface ClientSummaryResponse {
  client_Summary: ClientSummary;
  caveats?: string[];
}

export interface ClientSummary {
  financial_Position: FinancialPositionSummary;
  progress_Since_Last_Meeting?: string;
  financial_Goals?: string;
  recommendations_And_Next_Steps?: string;
  overall_Summary?: string;
}

export interface FinancialPositionSummary {
  assets?: string;
  liabilities?: string;
  income_Expenditure?: string;
  pensions?: string;
}