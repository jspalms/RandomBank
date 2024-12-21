namespace Identities.Api.EndPoints;

using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Models;

public static class IdentitiesEndpoints
{
    public static void RegisterIdentitiesEndpoints(this IEndpointRouteBuilder builder)
    {
        var accounts = builder.MapGroup("/accounts").WithTags("Accounts Api").WithOpenApi();

        accounts.MapGet("", GetIdentities)
            .WithName("GetAccounts");

        accounts.MapGet("/{id}", GetIdentity)
            .WithName("GetAccount");

        accounts.MapPost("", CreateIdentity)
            .WithName("CreateAccount");

        accounts.MapPut("/{id}", UpdateIdentity)
            .WithName("UpdateAccount");

        accounts.MapDelete("/{id}", DeleteIdentity)
            .WithName("DeleteAccount");
    }
    

    private static object[] GetIdentities()
    {
        return new[] { new { Id = 1, Name = "Account 1" }, new { Id = 2, Name = "Account 2" } };
    }

    private static object Getidentity(int id)
    {
        return new { Id = id, Name = $"Product {id}" };
    }

    private static async Task<Results<Created, BadRequest<string>>> CreateIdentity(
        CreateIdentityRequest createAccountRequest, 
        AbstractValidator<CreateIdentityRequest> createAccountRequestValidator,
        IMediator mediator)
    {
        var validationResult = await createAccountRequestValidator.ValidateAsync(createAccountRequest);

        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.ToString());
        }

        var command = new CreateAccountCommand(Guid.NewGuid(), createAccountRequest.CustomerId, createAccountRequest.AccountType, createAccountRequest.description, createAccountRequest.InitialCredit);

        var result = await mediator.Send(command);

        return TypedResults.Created($"/{result}");
    }

    private static object pdateIdentity(int id, object product)
    {
        return Results.Ok(product);
    }

    private static object DeleteIdentity(int id)
    {
        return Results.NoContent();
    }
    
}