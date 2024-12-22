namespace Users.Api.Endponts;

using Application.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Models;

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
            .WithName("CreateUser");

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
        CreateUserRequest createUserRequest, 
        AbstractValidator<CreateUserRequest> createUserRequestValidator,
        IMediator mediator)
    {
        var validationResult = await createUserRequestValidator.ValidateAsync(createUserRequest);

        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.ToString());
        }

        var command = new CreateUserCommand(createUserRequest.FirstName, createUserRequest.LastName, createUserRequest.UserEmail, createUserRequest.PhoneNumber);

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