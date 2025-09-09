import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { ApiService } from '../services/api.service';
import { ClientResponse, ClientSummaryResponse } from '../models/client.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatToolbarModule,
    MatProgressSpinnerModule,
    MatChipsModule
  ],
  template: `
    <mat-toolbar color="primary">
      <span>Wealth Summary AI</span>
    </mat-toolbar>

    <div class="container">
      <mat-card class="search-card">
        <mat-card-header>
          <mat-card-title>Client Wealth Summary</mat-card-title>
          <mat-card-subtitle>Enter a client ID to view their wealth summary</mat-card-subtitle>
        </mat-card-header>
        <mat-card-content>
          <mat-form-field appearance="outline" class="full-width">
            <mat-label>Client ID</mat-label>
            <input matInput 
                   type="number" 
                   [(ngModel)]="clientId" 
                   (keyup.enter)="loadClientSummary()"
                   placeholder="Enter client ID">
            <mat-icon matSuffix>search</mat-icon>
          </mat-form-field>
        </mat-card-content>
        <mat-card-actions>
          <button mat-raised-button 
                  color="primary" 
                  (click)="loadClientSummary()"
                  [disabled]="!clientId || loading">
            <mat-icon>analytics</mat-icon>
            Generate Summary
          </button>
        </mat-card-actions>
      </mat-card>

      <div *ngIf="loading" class="loading-container">
        <mat-spinner></mat-spinner>
        <p>Generating AI-powered wealth summary...</p>
      </div>

      <div *ngIf="error" class="error-container">
        <mat-card color="warn">
          <mat-card-content>
            <mat-icon color="warn">error</mat-icon>
            {{ error }}
          </mat-card-content>
        </mat-card>
      </div>

      <div *ngIf="client && summary" class="results-container">
        <!-- Client Info -->
        <mat-card class="client-card">
          <mat-card-header>
            <mat-card-title>{{ client.name }}</mat-card-title>
            <mat-card-subtitle>Client ID: {{ client.clientId }} | DOB: {{ client.dateOfBirth | date:'shortDate' }}</mat-card-subtitle>
          </mat-card-header>
        </mat-card>

        <!-- Financial Position -->
        <mat-card class="summary-card">
          <mat-card-header>
            <mat-card-title>
              <mat-icon>account_balance</mat-icon>
              Financial Position
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="financial-grid">
              <div *ngIf="summary.client_Summary.financial_Position.assets" class="financial-item">
                <h4>Assets</h4>
                <p>{{ summary.client_Summary.financial_Position.assets }}</p>
              </div>
              <div *ngIf="summary.client_Summary.financial_Position.liabilities" class="financial-item">
                <h4>Liabilities</h4>
                <p>{{ summary.client_Summary.financial_Position.liabilities }}</p>
              </div>
              <div *ngIf="summary.client_Summary.financial_Position.income_Expenditure" class="financial-item">
                <h4>Income & Expenditure</h4>
                <p>{{ summary.client_Summary.financial_Position.income_Expenditure }}</p>
              </div>
              <div *ngIf="summary.client_Summary.financial_Position.pensions" class="financial-item">
                <h4>Pensions</h4>
                <p>{{ summary.client_Summary.financial_Position.pensions }}</p>
              </div>
            </div>
          </mat-card-content>
        </mat-card>

        <!-- Goals and Progress -->
        <mat-card class="summary-card" *ngIf="summary.client_Summary.financial_Goals">
          <mat-card-header>
            <mat-card-title>
              <mat-icon>flag</mat-icon>
              Financial Goals
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>{{ summary.client_Summary.financial_Goals }}</p>
          </mat-card-content>
        </mat-card>

        <mat-card class="summary-card" *ngIf="summary.client_Summary.progress_Since_Last_Meeting">
          <mat-card-header>
            <mat-card-title>
              <mat-icon>trending_up</mat-icon>
              Progress Since Last Meeting
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>{{ summary.client_Summary.progress_Since_Last_Meeting }}</p>
          </mat-card-content>
        </mat-card>

        <!-- Recommendations -->
        <mat-card class="summary-card" *ngIf="summary.client_Summary.recommendations_And_Next_Steps">
          <mat-card-header>
            <mat-card-title>
              <mat-icon>lightbulb</mat-icon>
              Recommendations & Next Steps
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>{{ summary.client_Summary.recommendations_And_Next_Steps }}</p>
          </mat-card-content>
        </mat-card>

        <!-- Overall Summary -->
        <mat-card class="summary-card" *ngIf="summary.client_Summary.overall_Summary">
          <mat-card-header>
            <mat-card-title>
              <mat-icon>summarize</mat-icon>
              Overall Summary
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>{{ summary.client_Summary.overall_Summary }}</p>
          </mat-card-content>
        </mat-card>

        <!-- Caveats -->
        <mat-card class="summary-card" *ngIf="summary.caveats && summary.caveats.length > 0">
          <mat-card-header>
            <mat-card-title>
              <mat-icon>warning</mat-icon>
              Important Notes
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <mat-chip-set>
              <mat-chip *ngFor="let caveat of summary.caveats">{{ caveat }}</mat-chip>
            </mat-chip-set>
          </mat-card-content>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .container {
      max-width: 1200px;
      margin: 0 auto;
      padding: 20px;
    }

    .search-card {
      margin-bottom: 20px;
    }

    .full-width {
      width: 100%;
    }

    .loading-container {
      text-align: center;
      padding: 40px;
    }

    .loading-container mat-spinner {
      margin: 0 auto 20px auto;
    }

    .error-container {
      margin: 20px 0;
    }

    .results-container {
      margin-top: 20px;
    }

    .client-card, .summary-card {
      margin-bottom: 20px;
    }

    .financial-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 20px;
      margin-top: 10px;
    }

    .financial-item h4 {
      margin: 0 0 10px 0;
      color: #3f51b5;
      font-weight: 500;
    }

    .financial-item p {
      margin: 0;
      line-height: 1.5;
    }

    mat-card-header mat-card-title {
      display: flex;
      align-items: center;
      gap: 8px;
    }

    mat-icon {
      vertical-align: middle;
    }

    mat-chip-set {
      margin-top: 10px;
    }

    mat-chip {
      margin: 4px;
    }
  `]
})
export class DashboardComponent {
  clientId: number | null = null;
  client: ClientResponse | null = null;
  summary: ClientSummaryResponse | null = null;
  loading = false;
  error: string | null = null;

  constructor(private apiService: ApiService) {}

  loadClientSummary(): void {
    if (!this.clientId) {
      this.error = 'Please enter a client ID';
      return;
    }

    this.loading = true;
    this.error = null;
    this.client = null;
    this.summary = null;

    // Load client info and summary in parallel
    this.apiService.getClient(this.clientId).subscribe({
      next: (client) => {
        this.client = client;
        this.loadSummary();
      },
      error: (err) => {
        this.loading = false;
        this.error = `Failed to load client information: ${err.error?.message || err.message || 'Unknown error'}`;
      }
    });
  }

  private loadSummary(): void {
    if (!this.clientId) return;

    this.apiService.getClientSummary(this.clientId).subscribe({
      next: (summary) => {
        this.summary = summary;
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.error = `Failed to generate summary: ${err.error?.message || err.message || 'Unknown error'}`;
      }
    });
  }
}