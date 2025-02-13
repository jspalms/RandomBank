﻿namespace Accounts.Api.EndPoints;

using Accounts.Api.Models;
using Accounts.Application.Models;
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

    private static object[] GetAccounts()
    {
        return new[] { new { Id = 1, Name = "Account 1" }, new { Id = 2, Name = "Account 2" } };
    }

    private static object GetAccount(int id)
    {
        return new { Id = id, Name = $"Product {id}" };
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
        
        //Need to get user email from the JWT token
        var userEmail = "dummyValue";

        var command = new CreateAccountCommand(
            Guid.NewGuid(),
            createAccountRequest.CustomerId,
            createAccountRequest.AccountType,
            createAccountRequest.description,
            createAccountRequest.InitialCredit,
            userEmail);

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
