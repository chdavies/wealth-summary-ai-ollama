// API response types based on the .NET API

export interface Client {
  clientId: number;
  name: string;
  dateOfBirth: string;
  // Additional fields that might be included in the full client object
  assets?: Asset[];
  liabilities?: Liability[];
  financialStatuses?: FinancialStatus[];
  financialGoals?: FinancialGoal[];
  meetingNotes?: MeetingNote[];
  pensions?: Pension[];
}

export interface Asset {
  assetId: number;
  clientId: number;
  assetType: string;
  description: string;
  value: number;
  dateValued: string;
}

export interface Liability {
  liabilityId: number;
  clientId: number;
  liabilityType: string;
  description: string;
  amount: number;
  interestRate?: number;
  monthlyPayment?: number;
}

export interface FinancialStatus {
  financialStatusId: number;
  clientId: number;
  annualIncome: number;
  monthlyExpenses: number;
  riskAppetite: string;
  statusDate: string;
}

export interface FinancialGoal {
  financialGoalId: number;
  clientId: number;
  description: string;
  targetDate: string;
  targetAmount?: number;
}

export interface MeetingNote {
  meetingNoteId: number;
  clientId: number;
  meetingDate: string;
  author?: string;
  notes?: string;
}

export interface Pension {
  pensionId: number;
  clientId: number;
  providerName: string;
  currentValue: number;
  contributionAmount?: number;
  contributionFrequency?: string;
}

// Wealth Summary response from AI
export interface WealthSummaryResponse {
  content?: string; // Raw JSON content
}

export interface ClientSummary {
  client_summary: {
    financial_position: {
      assets: string;
      liabilities: string;
      income_expenditure: string;
      pensions: string;
    };
    progress_since_last_meeting: string;
    financial_goals: string;
    recommendations_and_next_steps: string;
    overall_summary: string;
  };
}