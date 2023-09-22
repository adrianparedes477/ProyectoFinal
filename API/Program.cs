using Core.Negocio.INegocio;
using Core.Negocio;
using Infraestructura.Data;
using Infraestructura.Data.Repositorio;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using API.Negocio.INegocio;
using API.Negocio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autorizacion JWT",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });



    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type= ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, new string[]{ }
                    }
    });

});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Administrador"));

    options.AddPolicy("AdminOrConsultor", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c =>
                (c.Type == ClaimTypes.Role && c.Value == "Administrador") ||
                (c.Type == ClaimTypes.Role && c.Value == "Consultor")
            )
        ));
});




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    });

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IProyectoRepositorio, ProyectoRepositorio>();
builder.Services.AddScoped<IServicioRepositorio, ServicioRepositorio>();
builder.Services.AddScoped<ITrabajoRepositorio, TrabajoRepositorio>();

builder.Services.AddScoped<IUsuarioNegocio, UsuarioNegocio>();
builder.Services.AddScoped<IProyectoNegocio, ProyectoNegocio>();
builder.Services.AddScoped<IServicioNegocio, ServicioNegocio>();
builder.Services.AddScoped<ITrabajoNegocio, TrabajoNegocio>();
builder.Services.AddScoped<ILoginNegocio, LoginNegocio>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
