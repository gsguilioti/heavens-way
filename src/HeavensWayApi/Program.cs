using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HeavensWayApi.Data;
using HeavensWayApi.Entities;
using HeavensWayApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<DistritoRepository>();
builder.Services.AddScoped<EnderecoRepository>();
builder.Services.AddScoped<EventoRepository>();
builder.Services.AddScoped<IgrejaRepository>();
builder.Services.AddScoped<TipoEventoRepository>();
builder.Services.AddScoped<UsuarioRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
