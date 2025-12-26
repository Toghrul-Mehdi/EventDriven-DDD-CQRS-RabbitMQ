using ECommerce.Domain.Products.Entities;
using ECommerce.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ECommerce.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    //DbContext daxilinde event qaldirma  mexanizminin confiqurasiyasi
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
}