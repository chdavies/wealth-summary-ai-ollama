export interface ClientResponse {
  clientId: number;
  fullName: string;
  dateOfBirth: string;
  maritalStatus: number;
  assets?: Asset[];
  liabilities?: Liability[];
  meetingNotes?: MeetingNote[];
  financialStatuses?: FinancialStatus[];
  financialGoals?: FinancialGoal[];
  pensions?: Pension[];
}

export interface Asset {
  assetId: number;
  clientId: number;
  assetType: number;
  description: string;
  value: number;
}

export interface Liability {
  liabilityId: number;
  clientId: number;
  liabilityType: number;
  description: string;
  value: number;
}

export interface MeetingNote {
  meetingNoteId: number;
  clientId: number;
  meetingDate: string;
  author: string;
  notes: string;
  createdAt: string;
}

export interface FinancialStatus {
  financialStatusId: number;
  clientId: number;
  annualIncome: number;
  annualExpenses: number;
  riskAppetite: number;
}

export interface FinancialGoal {
  financialGoalId: number;
  clientId: number;
  description: string;
  targetDate: string;
}

export interface Pension {
  pensionId: number;
  clientId: number;
  description: string;
  value: number;
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