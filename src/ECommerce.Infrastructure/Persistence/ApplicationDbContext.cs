using Microsoft.EntityFrameworkCore;
namespace ECommerce.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {        
    }
}
