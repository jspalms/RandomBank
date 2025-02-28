namespace Accounts.Api.EndPoints;

using System.Security.Claims;
using Models;
using Accounts.Application.Models;
using Application.Models.Commands;
using Application.Models.Queries;
using Domain.Entities.Accounts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

public static class AccountsEndpoints
{
    public static void RegisterAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        var accounts = builder.MapGroup("/accounts").WithTags("Accounts Api").WithOpenApi();

        accounts.MapGet("", GetAccounts)
            .WithName("GetAccounts");

        accounts.MapGet("/{id}", GetAccount)
            .WithName("GetAccount");

        accounts.MapPost("", CreateAccount)
            .WithName("CreateAccount");

        accounts.MapPut("/{id}", UpdateAccount)
            .WithName("UpdateAccount");

        accounts.MapDelete("/{id}", DeleteAccount)
            .WithName("DeleteAccount");
    }

    private static async Task<Results<Ok<IEnumerable<AccountDetailsDTO>>, NotFound>> GetAccounts(
        IMediator mediator, 
        ClaimsPrincipal userClaims)
    {
        var userId = userClaims.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var accounts = await mediator.Send(new GetAccountsQuery(Guid.Parse(userId)));
        return TypedResults.Ok(accounts);
    }

    private static async Task<Results<Ok<AccountDetailsDTO>, NotFound>> GetAccount(
        IMediator mediator, 
        Guid id,
        ClaimsPrincipal userClaims
        )
    {
        var userId = userClaims.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if(userId == null)
        {
            return TypedResults.NotFound();
        }
        var account = await mediator.Send(new GetAccountQuery(id, Guid.Parse(userId)));

        return account is not null ? TypedResults.Ok(account) : TypedResults.NotFound();
    }

    private static async Task<Results<Created, BadRequest<string>>> CreateAccount(
        CreateAccountRequest createAccountRequest, 
        IValidator<CreateAccountRequest> createAccountRequestValidator,
        IMediator mediator)
    {
        var validationResult = await createAccountRequestValidator.ValidateAsync(createAccountRequest);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.ToString());
        }
        
        //Should get the user ID using the token
        var userId = Guid.NewGuid();

        var command = new OpenAccountCommand(
            userId,
            createAccountRequest.productOptionId,
            createAccountRequest.description,
            createAccountRequest.initialCredit);

        var result = await mediator.Send(command);

        return TypedResults.Created($"/{result}");
    }

    private static object UpdateAccount(int id, object product)
    {
        return Results.Ok(product);
    }

    private static object DeleteAccount(int id)
    {
        return Results.NoContent();
    }
}
