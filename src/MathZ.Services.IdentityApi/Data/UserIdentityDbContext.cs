namespace MathZ.Services.IdentityApi.Data;

using MathZ.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class UserIdentityDbContext(DbContextOptions options)
    : IdentityDbContext<User>(options)
{
}
