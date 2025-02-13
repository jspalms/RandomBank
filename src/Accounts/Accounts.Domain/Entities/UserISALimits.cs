namespace Accounts.Domain.Entities;

public class UserISALimits
{
    public int VariableAccounts { get; set; }
    public int FixedAccounts { get; set; }
    public decimal RemainingYearlyAllowance { get; set; }
    public decimal TotalISAValue { get; set; }
    public decimal PreviousYearsSubscription { get; set; }

    public UserISALimits()
    {
        VariableAccounts = 0;
        FixedAccounts = 0;
        RemainingYearlyAllowance = 20000;
        TotalISAValue = 0;
        PreviousYearsSubscription = 0;
    }
}