using Accounts.Api.Models;

namespace Accounts.Api.Extensions;

public static class AccountsEndpoints
{
    public static void RegisterAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        var accounts = builder.MapGroup("/accounts").WithTags("Accounts Api").WithOpenApi();

        accounts.MapGet("", () =>
        {
            return new[] { new { Id = 1, Name = "Account 1" }, new { Id = 2, Name = "Account 2" } };
        })
        .WithName("GetAccounts");

        accounts.MapGet("/{id}", (int id) =>
        {
            return new { Id = id, Name = $"Product {id}" };
        })
        .WithName("GetAccount");

        accounts.MapPost("", (CreateAccountRequest createAccountRequest) =>
        {
            return Results.Created($"/account/{Guid.NewGuid()}", new { });
        })
        .WithName("CreateAccount");

        accounts.MapPut("/{id}", (int id, object product) =>
        {
            return Results.Ok(product);
        })
        .WithName("UpdateAccount");

        accounts.MapDelete("/{id}", (int id) =>
        {
            return Results.NoContent();
        })
        .WithName("DeleteAccount");
    }
}
