namespace Accounts.Api.EndPoints;

using System.Security.Claims;
using Models;
using Accounts.Application.Models;
using Application.Models.Commands;
using Application.Models.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Accounts.Api.Utilities;

public static class AccountsEndpoints
{
    public static void RegisterAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        var accounts = builder
            .MapGroup("/accounts")
            .WithTags("Accounts Api")
            .WithOpenApi()
            .RequireAuthorization();

        accounts.MapGet("", GetAccounts)
            .WithName("GetAccounts");

        accounts.MapGet("/{id}", GetAccount)
            .WithName("GetAccount");

        accounts.MapPost("", OpenAccount)
            .WithName("CreateAccount");

        accounts.MapPut("/{id}", UpdateAccount)
            .WithName("UpdateAccount");

        accounts.MapDelete("/{id}", DeleteAccount)
            .WithName("DeleteAccount");
    }

    private static async Task<Results<Ok<IEnumerable<AccountDetailsDTO>>, UnauthorizedHttpResult>> GetAccounts(
        IMediator mediator,
        ClaimsPrincipal userClaims)
    {
        var userId = ClaimsHelper.GetUserId(userClaims);
        if (userId == null)
        {
            return TypedResults.Unauthorized();
        }
        var accounts = await mediator.Send(new GetAccountsQuery(userId.Value));
        return TypedResults.Ok(accounts);
    }

    private static async Task<Results<Ok<AccountDetailsDTO>, NotFound, UnauthorizedHttpResult>> GetAccount(
        IMediator mediator, 
        Guid id,
        ClaimsPrincipal userClaims
        )
    {
        var userId = ClaimsHelper.GetUserId(userClaims);
        if (userId == null)
        {
            return TypedResults.Unauthorized();
        }
        var account = await mediator.Send(new GetAccountQuery(id, userId.Value));

        return account is not null ? TypedResults.Ok(account) : TypedResults.NotFound();
    }

    private static async Task<Results<Created, UnauthorizedHttpResult, BadRequest<string>>> OpenAccount(
        OpenAccountRequest openAccountRequest, 
        IValidator<OpenAccountRequest> openAccountRequestValidator,
        IMediator mediator,
        ClaimsPrincipal userClaims)
    {
        var validationResult = await openAccountRequestValidator.ValidateAsync(openAccountRequest);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.ToString());
        }

        var userId = ClaimsHelper.GetUserId(userClaims);
        if (userId == null)
        {
            return TypedResults.Unauthorized();
        }

        var command = new OpenAccountCommand(
            userId.Value,
            openAccountRequest.productOptionId,
            openAccountRequest.description,
            openAccountRequest.initialCredit);

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
