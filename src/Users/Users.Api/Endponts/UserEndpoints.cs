namespace Users.Api.Endponts;

using Application.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Models;
using System.Security.Claims;
using Users.Api.Utilities;
using Users.Domain.ValueObjects;

public static class UsersEndpoints
{
    public static void RegisterUsersEndpoints(this IEndpointRouteBuilder builder)
    {
        var users = builder.MapGroup("/users").WithTags("Users Api").WithOpenApi();

        users.MapGet("", GetUsers)
            .WithName("GetUsers");

        users.MapGet("/{id}", GetUser)
            .WithName("GetUser");

        users.MapPost("", CreateUser)
            .WithName("CreateUser").RequireAuthorization();

        users.MapPut("/{id}", UpdateUser)
            .WithName("UpdateUser");

        users.MapDelete("/{id}", DeleteUser)
            .WithName("DeleteUser");
    }
    
    private static object[] GetUsers()
    {
        return new[] { new { Id = 1, Name = "Account 1" }, new { Id = 2, Name = "Account 2" } };
    }

    private static object GetUser(int id)
    {
        return new { Id = id, Name = $"Product {id}" };
    }

    private static async Task<Results<Created, BadRequest<string>>> CreateUser(
        IMediator mediator,
        ClaimsPrincipal userClaims)
    {
        var firstName = ClaimsHelper.GetFirstName(userClaims);
        var lastName = ClaimsHelper.GetLastName(userClaims);
        var userEmail = ClaimsHelper.GetEmail(userClaims);
        var userId = ClaimsHelper.GetUserId(userClaims);

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(userEmail) || userId is null)
        {
            return TypedResults.BadRequest("User claims are missing required information");
        }


        var command = new CreateUserCommand(userId.Value, firstName, lastName, new Email(userEmail));

        var result = await mediator.Send(command);

        return TypedResults.Created($"/{result}");
    }

    private static object UpdateUser(int id, object product)
    {
        return Results.Ok(product);
    }

    private static object DeleteUser(int id)
    {
        return Results.NoContent();
    }
    
}