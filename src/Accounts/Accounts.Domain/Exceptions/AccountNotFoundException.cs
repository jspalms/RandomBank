namespace Accounts.Domain.Exceptions;

public class AccountNotFoundException: Exception
{
    public AccountNotFoundException(Guid accountId, Guid protfolioId) : base($"Account with id {accountId} was not found within the portfolio with id {protfolioId}")
    {
        
    }
}