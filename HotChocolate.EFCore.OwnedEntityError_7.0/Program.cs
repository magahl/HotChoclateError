using HotChocolate.EFCore.OwnedEntityError;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services
    .AddDbContext<HotContext>(ServiceLifetime.Scoped);
services
    .AddGraphQLServer()
    .AddQueryType(x => x.Name("Query"))
    .AddTypeExtension<ShipmentQueries>()
    .RegisterDbContext<HotContext>(DbContextKind.Synchronized)
    .AddAuthorization()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .InitializeOnStartup();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapGraphQL(); });

var sf = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = sf.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HotContext>();
    context.Database.Migrate();
    var shipmentId = Guid.Parse("87DF0A44-8C0A-469B-8E91-15067385BA25");
    var shipment = await context.Shipments.SingleOrDefaultAsync(x => x.ShipmentId == shipmentId);
    if (shipment == null)
    {
        context.Shipments.Add(new Shipment()
        {
            ShipmentId = shipmentId,
            Consignor = new Address
            {
                AddressLine1 = null,
                Name = "Test123"
            }
        });

        await context.SaveChangesAsync();
    }
    
}
app.Run();