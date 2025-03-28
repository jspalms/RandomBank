using Users.Domain.Entities;
using Users.Domain.Interfaces;

namespace Users.Infrastructure.Data.Repositories;

public class UserRepository(ApplicationDbContext dbContext) : BaseRepository<User>(dbContext), IUserRepository
{
    
}