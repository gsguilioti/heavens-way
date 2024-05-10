using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HeavensWayApi.Data;
using HeavensWayApi.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<Usuario>()
    .AddEntityFrameworkStores<DataContext>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<Usuario>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
