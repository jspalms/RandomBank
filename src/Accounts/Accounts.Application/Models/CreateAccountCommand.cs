﻿using MediatR;

namespace Accounts.Api.Models;

public record CreateAccountCommand(Guid CustomerId, string AccountType, string Description, decimal InitialCredit): IRequest<Guid>;
