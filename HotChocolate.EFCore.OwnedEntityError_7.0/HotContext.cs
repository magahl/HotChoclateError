using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotChocolate.EFCore.OwnedEntityError;

public class HotContext : DbContext
{
    public DbSet<Shipment> Shipments { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = System.IO.Path.Join(path, "hoterror7.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}",
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ShipmentMapper());
    }
}


public class ShipmentMapper : IEntityTypeConfiguration<Shipment>
{
    public DbSet<Shipment> Shipments { get; set; }
    
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.ToTable("Shipments");
        builder.HasKey(x => x.ShipmentId);
        builder.OwnsOne(e => e.Consignor);
    }
}


public class Shipment
{
    public Guid ShipmentId { get; set; } = Guid.NewGuid();
    public Address Consignor { get; set; }
}

public class Address
{
    public string Name { get; set; } 
    public string AddressLine1 { get; set; }
}



[ExtendObjectType("Query")]
public class ShipmentQueries
{
    [UsePaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Shipment> Shipments(HotContext context)
        => context.Shipments;
}