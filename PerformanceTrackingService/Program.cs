using Microsoft.EntityFrameworkCore;
using PerformanceTrackingService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PerformanceContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(Options =>
{
    Options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PerformanceContext>();
    db.Database.Migrate();
}

app.Run();
