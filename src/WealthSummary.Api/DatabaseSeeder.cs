using WealthSummary.Infrastructure.Data;
using WealthSummary.Domain.Model;

namespace WealthSummary.Api;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(WealthDbContext dbContext)
    {
       if (!dbContext.Clients.Any())
        {
            dbContext.Clients.AddRange(
                new Client { ClientId = 10001, FullName = "John Doe", DateOfBirth = new DateTime(1960, 5, 15, 0, 0, 0, DateTimeKind.Local) },
                new Client { ClientId = 10002, FullName = "Jane Smith", DateOfBirth = new DateTime(1985, 10, 25, 0, 0, 0, DateTimeKind.Local) },
                new Client { ClientId = 10003, FullName = "Alice Johnson", DateOfBirth = new DateTime(1975, 3, 30, 0, 0, 0, DateTimeKind.Local) }
            );

            await dbContext.SaveChangesAsync();

            dbContext.Assets.AddRange(
                new Asset { ClientId = 10001, AssetType = AssetType.RealEstate, Description = "Main Residence", Value = 1500000m },
                new Asset { ClientId = 10001, AssetType = AssetType.RealEstate, Description = "French Chateau", Value = 750000m },
                new Asset { ClientId = 10001, AssetType = AssetType.Stocks, Description = "Investment Portfolio", Value = 2000000m }, 
                new Asset { ClientId = 10001, AssetType = AssetType.Possessions, Description = "Ferrari GTB 280", Value = 265000m },
                new Asset { ClientId = 10001, AssetType = AssetType.Possessions, Description = "Paintings by famous artists", Value = 1100000m },

                new Asset { ClientId = 10002, AssetType = AssetType.RealEstate, Description = "Main Residence", Value = 650000m },
                new Asset { ClientId = 10002, AssetType = AssetType.Possessions, Description = "Car", Value = 21000m },
                new Asset { ClientId = 10002, AssetType = AssetType.Possessions, Description = "Swiss Watch", Value = 3000m },
                new Asset { ClientId = 10002, AssetType = AssetType.Stocks, Description = "Investment Fund", Value = 1300m },
                new Asset { ClientId = 10002, AssetType = AssetType.Cash, Description = "Savings", Value = 24500m },

                new Asset { ClientId = 10003, AssetType = AssetType.RealEstate, Description = "Family Home", Value = 1200000m },
                new Asset { ClientId = 10003, AssetType = AssetType.Possessions, Description = "Car", Value = 65000m },
                new Asset { ClientId = 10003, AssetType = AssetType.Possessions, Description = "Car", Value = 16000m },
                new Asset { ClientId = 10003, AssetType = AssetType.RealEstate, Description = "Holiday Home", Value = 120000m },
                new Asset { ClientId = 10003, AssetType = AssetType.Cash, Description = "Savings", Value = 12750m }
            );

            await dbContext.SaveChangesAsync();

            dbContext.Liabilities.AddRange(
                new Liability { ClientId = 10001, LiabilityType = LiabilityType.Mortgage, Description = "Mortgage on Main Residence", Value = 300000m },
                new Liability { ClientId = 10001, LiabilityType = LiabilityType.CreditCard, Description = "Credit Card Debt", Value = 15000m },
                new Liability { ClientId = 10001, LiabilityType = LiabilityType.PersonalLoan, Description = "Loan from Friend", Value = 50000m },

                new Liability { ClientId = 10002, LiabilityType = LiabilityType.Mortgage, Description = "Mortgage on Main Residence", Value = 150000m },
                new Liability { ClientId = 10002, LiabilityType = LiabilityType.CreditCard, Description = "Credit Card Debt", Value = 5000m },

                new Liability { ClientId = 10003, LiabilityType = LiabilityType.Mortgage, Description = "Mortgage on Family Home", Value = 0m },
                new Liability { ClientId = 10003, LiabilityType = LiabilityType.Mortgage, Description = "Mortgage on Holiday Home", Value = 30000m },
                new Liability { ClientId = 10003, LiabilityType = LiabilityType.PersonalLoan, Description = "Loan from Bank", Value = 7500m },
                new Liability { ClientId = 10003, LiabilityType = LiabilityType.StudentLoan, Description = "University Debt", Value = 1500m },
                new Liability { ClientId = 10003, LiabilityType = LiabilityType.CreditCard, Description = "Credit Card Debt", Value = 1000m }

            );

            await dbContext.SaveChangesAsync();

            dbContext.FinancialStatuses.AddRange(
                new FinancialStatus { ClientId = 10001, RiskAppetite = RiskAppetite.VeryHigh, AnnualIncome = 3000000m, AnnualExpenses = 750000m, Goals = "To run a successful business empire." },
                new FinancialStatus { ClientId = 10002, RiskAppetite = RiskAppetite.Low, AnnualIncome = 137500, AnnualExpenses = 12500, Goals = "Build a comfortable pension for retirement." },
                new FinancialStatus { ClientId = 10003, RiskAppetite = RiskAppetite.Moderate, AnnualIncome = 235000, AnnualExpenses = 15000, Goals = "Achieve financial independence." }
            );

            dbContext.MeetingNotes.AddRange(
                new MeetingNote { 
                    ClientId = 10001, 
                    Author = "Bob Andrews", 
                    MeetingDate = new DateTime(2025, 03, 14, 0, 0, 0, DateTimeKind.Local), 
                    CreatedAt = DateTime.Now.AddDays(-30), 
                    Notes = "Discussed investment opportunities in emerging markets. Reviewed portfolio performance and rebalanced assets." 
                },
                new MeetingNote { 
                    ClientId = 10002, 
                    Author = "Susan Lee",
                    MeetingDate = new DateTime(2025, 06,11, 0, 0, 0, DateTimeKind.Local), 
                    CreatedAt = DateTime.Now.AddDays(-20), 
                    Notes = "Talked about pension options and retirement planning." 
                },
                new MeetingNote { 
                    ClientId = 10003, 
                    Author = "Michael Brown",
                    MeetingDate = new DateTime(2025, 05, 20, 0, 0, 0, DateTimeKind.Local),
                    CreatedAt = DateTime.Now.AddDays(-10),
                    Notes = "Explored real estate investment strategies. Updated financial goals and risk tolerance." 
                }
            );
            await dbContext.SaveChangesAsync();

        }

    }
}
