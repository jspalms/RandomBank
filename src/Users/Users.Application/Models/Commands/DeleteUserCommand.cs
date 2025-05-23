﻿using MediatR;

namespace Users.Application.Models.Commands;

public record DeleteUserCommand(Guid Id) : IRequest;