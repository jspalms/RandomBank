﻿using MediatR;
using Users.Domain.Entities;

namespace Users.Application.Models.Queries;

public record GetUsersQuery() : IRequest<List<User>>;