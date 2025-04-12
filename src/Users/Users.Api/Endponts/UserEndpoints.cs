using Microsoft.AspNetCore.Mvc;
using Users.Application.Models.Commands;
using Users.Application.Models.Queries;
using Users.Domain.Entities;

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
        
        users.MapPost("WithoutAuth", CreateUserWithoutAuth)
            .WithName("CreateUser");

        users.MapPut("/{id}", UpdateUser)
            .WithName("UpdateUser");

        users.MapDelete("/{id}", DeleteUser)
            .WithName("DeleteUser");
    }
    
    private static async Task<Results<Ok<List<User>>, BadRequest<string>>> GetUsers(
        IMediator mediator)
    {
        var result = await mediator.Send(new GetUsersQuery());
        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<User>, BadRequest<string>>> GetUser(
        Guid id,
        IMediator mediator)
    {
        var result = await mediator.Send(new GetUserQuery(id));
        return TypedResults.Ok(result);
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

    private static async Task<Results<Ok, BadRequest>> UpdateUser(
        UpdateUserRequest request,
        IMediator mediator)
    {
        var command = new UpdateUserCommand(request.Id, request.FirstName, request.LastName, new Email(request.Email));
        await mediator.Send(command);
        return TypedResults.Ok();
    }

    private static async Task<Results<NoContent, BadRequest>> DeleteUser(
        Guid id,
        IMediator mediator)
    {
        await mediator.Send(new DeleteUserCommand(id));
        return TypedResults.BadRequest();
    }
    
    public static async Task<Results<Created, BadRequest<string>>> CreateUserWithoutAuth(
        CreateUserRequest createUserRequest,
        IMediator mediator)
    {
        var firstName = createUserRequest.FirstName;
        var lastName = createUserRequest.LastName;
        var userEmail = createUserRequest.UserEmail;
        var userId = createUserRequest.UserId;

        var command = new CreateUserCommand(userId, firstName, lastName, new Email(userEmail));

        var result = await mediator.Send(command);

        return TypedResults.Created($"/{result}");
    }
    
}