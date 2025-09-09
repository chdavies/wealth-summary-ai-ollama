# Angular Frontend Development Guide

## Overview
This Angular frontend provides a Material UI interface for the Wealth Summary AI API. It displays AI-generated wealth summaries with professional styling and responsive design.

## Prerequisites
- Node.js 18+ and npm
- .NET 8 SDK
- The wealth summary API running on `https://localhost:7055`

## Development Setup

### 1. Install Dependencies
```bash
cd src/frontend
npm install
```

### 2. Start the .NET API
```bash
cd src
dotnet run --project WealthSummary.Api
```
The API will run on `https://localhost:7055`

### 3. Start Angular Development Server
```bash
cd src/frontend
npm start
```
The Angular app will run on `http://localhost:4200` with API proxy configuration.

## Project Structure

```
src/frontend/
├── src/
│   ├── app/
│   │   ├── components/
│   │   │   └── dashboard.component.ts    # Main UI component
│   │   ├── models/
│   │   │   └── client.model.ts           # TypeScript interfaces
│   │   ├── services/
│   │   │   └── api.service.ts            # HTTP client service
│   │   ├── app.config.ts                 # App configuration
│   │   └── app.ts                        # Root component
│   ├── styles.scss                       # Global styles with Material theme
│   └── index.html                        # App entry point
├── proxy.conf.json                       # Development proxy config
└── package.json                          # Dependencies and scripts
```

## Features

### Material UI Components Used
- **MatToolbar** - Navigation header
- **MatCard** - Content containers
- **MatButton** - Action buttons
- **MatIcon** - Visual icons
- **MatInput** - Form inputs
- **MatProgressSpinner** - Loading indicator
- **MatChips** - Caveat tags

### Responsive Design
- Grid layouts for financial data
- Mobile-friendly Material components
- Professional color scheme

### API Integration
- HTTP client service with error handling
- TypeScript interfaces matching .NET models
- Proxy configuration for development

## Available Endpoints
- `GET /api/client/{clientId}` - Get client details
- `GET /api/summaries/{clientId}` - Get AI wealth summary

## Build Commands

### Development
```bash
npm start          # Start dev server with API proxy
npm run watch      # Build in watch mode
```

### Production
```bash
npm run build      # Create production build in dist/
```

## Testing Client IDs
The seeded database contains sample client data. Try client IDs like 1, 2, 3 to test the summary generation.

## Troubleshooting

### CORS Issues
- Ensure the .NET API is running with CORS configured for `http://localhost:4200`
- Check the proxy configuration in `proxy.conf.json`

### Build Errors
- Run `npm install` to ensure all dependencies are installed
- Check TypeScript compilation errors in the console

### API Connection
- Verify the .NET API is accessible at `https://localhost:7055`
- Check browser network tab for failed API calls