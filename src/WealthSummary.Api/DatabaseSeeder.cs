using Microsoft.VisualBasic;
using WealthSummary.Domain.Model;
using WealthSummary.Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WealthSummary.Api;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(WealthDbContext dbContext)
    {
       if (!dbContext.Clients.Any())
        {
            // **** Add Clients ****
            dbContext.Clients.AddRange(
                new Client { ClientId = 10001, FullName = "John Doe", MaritalStatus = MaritalStatus.Divorced, DateOfBirth = new DateTime(1972, 5, 15, 0, 0, 0, DateTimeKind.Local) },
                new Client { ClientId = 10002, FullName = "Jane Smith", MaritalStatus = MaritalStatus.Married, DateOfBirth = new DateTime(1985, 10, 25, 0, 0, 0, DateTimeKind.Local) },
                new Client { ClientId = 10003, FullName = "Alice Johnson", MaritalStatus = MaritalStatus.Married, DateOfBirth = new DateTime(1978, 3, 30, 0, 0, 0, DateTimeKind.Local) },
                new Client { ClientId = 10004, FullName = "David Roberts", MaritalStatus = MaritalStatus.Single, DateOfBirth = new DateTime(2000, 10, 12, 0, 0, 0, DateTimeKind.Local) }
            );

            await dbContext.SaveChangesAsync();

            // **** Add Assets ****
            dbContext.Assets.AddRange(
                new Asset { ClientId = 10001, AssetType = AssetType.RealEstate, Description = "Main Residence", Value = 1500000m },
                new Asset { ClientId = 10001, AssetType = AssetType.RealEstate, Description = "French Chateau", Value = 750000m },
                new Asset { ClientId = 10001, AssetType = AssetType.Stocks, Description = "Investment Portfolio", Value = 2000000m }, 
                new Asset { ClientId = 10001, AssetType = AssetType.Possessions, Description = "Ferrari GTB 280", Value = 265000m },
                new Asset { ClientId = 10001, AssetType = AssetType.Possessions, Description = "Paintings by famous artists", Value = 1100000m },

                new Asset { ClientId = 10002, AssetType = AssetType.RealEstate, Description = "Main Residence", Value = 650000m },
                new Asset { ClientId = 10002, AssetType = AssetType.Possessions, Description = "Car", Value = 21000m },
                new Asset { ClientId = 10002, AssetType = AssetType.Stocks, Description = "Investment Fund", Value = 1300m },
                new Asset { ClientId = 10002, AssetType = AssetType.Cash, Description = "Savings", Value = 24500m },

                new Asset { ClientId = 10003, AssetType = AssetType.RealEstate, Description = "Family Home", Value = 1200000m },
                new Asset { ClientId = 10003, AssetType = AssetType.Possessions, Description = "Car", Value = 65000m },
                new Asset { ClientId = 10003, AssetType = AssetType.Possessions, Description = "Car", Value = 16000m },
                new Asset { ClientId = 10003, AssetType = AssetType.RealEstate, Description = "Holiday Home", Value = 120000m },
                new Asset { ClientId = 10002, AssetType = AssetType.Possessions, Description = "Swiss Watch", Value = 3000m },
                new Asset { ClientId = 10003, AssetType = AssetType.Cash, Description = "Savings", Value = 12750m },

                new Asset { ClientId = 10004, AssetType = AssetType.Cash, Description = "Savings", Value = 15000m },
                new Asset { ClientId = 10004, AssetType = AssetType.Stocks, Description = "Investment Portfolio", Value = 45000m }
            );

            await dbContext.SaveChangesAsync();

            // **** Add Liabilities ****
            dbContext.Liabilities.AddRange(
                new Liability { ClientId = 10001, LiabilityType = LiabilityType.Mortgage, Description = "Mortgage on Main Residence", Value = 300000m },
                new Liability { ClientId = 10001, LiabilityType = LiabilityType.CreditCard, Description = "Credit Card Debt", Value = 15000m },
                new Liability { ClientId = 10001, LiabilityType = LiabilityType.PersonalLoan, Description = "Loan from Friend", Value = 50000m },

                new Liability { ClientId = 10002, LiabilityType = LiabilityType.Mortgage, Description = "Mortgage on Main Residence", Value = 150000m },
                new Liability { ClientId = 10002, LiabilityType = LiabilityType.CreditCard, Description = "Credit Card Debt", Value = 5000m },

                new Liability { ClientId = 10003, LiabilityType = LiabilityType.Mortgage, Description = "Mortgage on Family Home", Value = 125000m },
                new Liability { ClientId = 10003, LiabilityType = LiabilityType.Mortgage, Description = "Mortgage on Holiday Home", Value = 3000m },
                new Liability { ClientId = 10003, LiabilityType = LiabilityType.PersonalLoan, Description = "Loan from Bank", Value = 7500m },
                new Liability { ClientId = 10003, LiabilityType = LiabilityType.StudentLoan, Description = "University Debt", Value = 1500m },
                new Liability { ClientId = 10003, LiabilityType = LiabilityType.CreditCard, Description = "Credit Card Debt", Value = 1000m },

                new Liability { ClientId = 10004, LiabilityType = LiabilityType.CreditCard, Description = "Credit Card Debt", Value = 2400m }

            );

            await dbContext.SaveChangesAsync();

            // **** Add Pensions ****
            dbContext.Pensions.AddRange(
                new Pension {  ClientId = 10001, Description = "Self-Managed Pension", Value = 1200000m },
                new Pension { ClientId = 10002, Description = "Defined Contribution Company Pension A", Value = 60000m },
                new Pension { ClientId = 10002, Description = "Defined Contribution Company Pension B", Value = 200000m },
                new Pension { ClientId = 10003, Description = "Defined Contribution Company Pension", Value = 550000m }
            );

            await dbContext.SaveChangesAsync();
            
            // **** Add Financial Status ****
            dbContext.FinancialStatuses.AddRange(
                new FinancialStatus { ClientId = 10001, RiskAppetite = RiskAppetite.VeryHigh, AnnualIncome = 3000000m, AnnualExpenses = 750000m },
                new FinancialStatus { ClientId = 10002, RiskAppetite = RiskAppetite.Low, AnnualIncome = 45000, AnnualExpenses = 3000m },
                new FinancialStatus { ClientId = 10003, RiskAppetite = RiskAppetite.Moderate, AnnualIncome = 135000, AnnualExpenses = 15000 },
                new FinancialStatus { ClientId = 10004, RiskAppetite = RiskAppetite.High, AnnualIncome = 55000, AnnualExpenses = 2000 }
            );

            await dbContext.SaveChangesAsync();

            // **** Add Financial Goals ****
            dbContext.FinancialGoals.AddRange(
                new FinancialGoal {  ClientId = 10001, TargetDate = new DateTime(2030, 12, 31), Description = "To run a successful business empire." },
                new FinancialGoal { ClientId = 10001, TargetDate = new DateTime(2035, 12, 31), Description = "Earn enough money to continue with high-addrenaline sport activities." },
                new FinancialGoal { ClientId = 10001, TargetDate = new DateTime(2030, 12, 31), Description = "Expand business operations internationally." },
                new FinancialGoal { ClientId = 10001, TargetDate = new DateTime(2037, 12, 31), Description = "Retire with an exceptional lifestyle." },
                new FinancialGoal { ClientId = 10002, TargetDate = new DateTime(2040, 12, 31), Description = "Build a comfortable pension for retirement." },
                new FinancialGoal { ClientId = 10003, TargetDate = new DateTime(2042, 12, 31), Description = "Achieve financial independence." },
                new FinancialGoal { ClientId = 10003, TargetDate = new DateTime(2042, 12, 31), Description = "Support Children through University." },
                new FinancialGoal { ClientId = 10003, TargetDate = new DateTime(2028, 12, 31), Description = "Pay off student debt." },
                new FinancialGoal { ClientId = 10004, TargetDate = new DateTime(2027, 12, 31), Description = "Buy a property in London" },
                new FinancialGoal { ClientId = 10004, TargetDate = new DateTime(2055, 12, 31), Description = "Retire early and travel the world." }
            );
            await dbContext.SaveChangesAsync();

            // **** Add Meeting Notes ****
            dbContext.MeetingNotes.AddRange(
                new MeetingNote
                {
                    ClientId = 10001,
                    Author = "Bob Andrews",
                    MeetingDate = new DateTime(2025, 03, 14, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Discussed investment opportunities in emerging markets."
                },
                new MeetingNote
                {
                    ClientId = 10001,
                    Author = "Bob Andrews",
                    MeetingDate = new DateTime(2025, 03, 14, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Reviewed portfolio performance and rebalanced assets." 
                },
                new MeetingNote
                {
                    ClientId = 10001,
                    Author = "Bob Andrews",
                    MeetingDate = new DateTime(2025, 03, 14, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Concerned about global economies."
                },
                new MeetingNote
                {
                    ClientId = 10001,
                    Author = "Bob Andrews",
                    MeetingDate = new DateTime(2025, 03, 14, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Concerned about import tariffs affecting business in some countries."
                },
                new MeetingNote
                {
                    ClientId = 10001,
                    Author = "Bob Andrews",
                    MeetingDate = new DateTime(2025, 03, 14, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Recommended a follow-up meeting to discuss reducing tax burden."
                },
                new MeetingNote { 
                    ClientId = 10002, 
                    Author = "Susan Lee",
                    MeetingDate = new DateTime(2025, 06, 11, 0, 0, 0, DateTimeKind.Local), 
                    Notes = "Talked about pension options and retirement planning." 
                },
                new MeetingNote
                {
                    ClientId = 10002,
                    Author = "Susan Lee",
                    MeetingDate = new DateTime(2025, 06, 11, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Looked at options for investing in a stocks and shares ISA."
                },
                new MeetingNote
                {
                    ClientId = 10002,
                    Author = "Susan Lee",
                    MeetingDate = new DateTime(2025, 06, 11, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Recommended a follow-up meeting on how to pay off mortgage early."
                },
                new MeetingNote { 
                    ClientId = 10003, 
                    Author = "Michael Brown",
                    MeetingDate = new DateTime(2025, 05, 20, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Explored real estate investment strategies. " 
                },
                new MeetingNote
                {
                    ClientId = 10003,
                    Author = "Michael Brown",
                    MeetingDate = new DateTime(2025, 05, 20, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Updated financial goals and risk tolerance."
                },
                new MeetingNote
                {
                    ClientId = 10003,
                    Author = "Michael Brown",
                    MeetingDate = new DateTime(2025, 05, 20, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Discussed investment strategies for children university fund"
                },
                new MeetingNote
                {
                    ClientId = 10003,
                    Author = "Michael Brown",
                    MeetingDate = new DateTime(2025, 05, 20, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Client is interested in socially responsible investments."
                },
                new MeetingNote
                {
                    ClientId = 10003,
                    Author = "Michael Brown",
                    MeetingDate = new DateTime(2025, 05, 20, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Client would like to pay off student loan as soon as possible."
                },
                new MeetingNote
                {
                    ClientId = 10003,
                    Author = "Michael Brown",
                    MeetingDate = new DateTime(2025, 05, 20, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Client is concerned about rising interest rates."
                },
                new MeetingNote
                {
                    ClientId = 10004,
                    Author = "Bob Andrews",
                    MeetingDate = new DateTime(2025, 07, 22, 0, 0, 0, DateTimeKind.Local),
                    Notes = "First face-to-face meeting with client. Discussed mortgage options."
                },
                new MeetingNote
                {
                    ClientId = 10004,
                    Author = "Bob Andrews",
                    MeetingDate = new DateTime(2025, 07, 22, 0, 0, 0, DateTimeKind.Local),
                    Notes = "Discussed options on growing wealth as they progress through their career."
                }
            );
            await dbContext.SaveChangesAsync();

        }

    }
}
