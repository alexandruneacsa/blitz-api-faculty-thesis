using Blitz.API.Configuration;
using Blitz.API.Extensions;
using Blitz.Domain.Entities;
using Blitz.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
IConfiguration _config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AddFileParamTypesOperationFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BlitzAPI", Version = "v1" });
    OpenApiSecurityScheme securityDefinition = new()
    {
        Name = "Bearer",
        BearerFormat = "JWT",
        Scheme = "bearer",
        Description = "Specify the authorization token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
    };
    options.AddSecurityDefinition("jwt_auth", securityDefinition);

    OpenApiSecurityScheme securityScheme = new()
    {
        Reference = new OpenApiReference()
        {
            Id = "jwt_auth",
            Type = ReferenceType.SecurityScheme
        }
    };
    OpenApiSecurityRequirement securityRequirements = new()
    {
        { securityScheme, Array.Empty<string>() },
    };
    options.AddSecurityRequirement(securityRequirements);
    var filePath = Path.Combine(AppContext.BaseDirectory, "Blitz.API.xml");
    options.IncludeXmlComments(filePath);
});

builder.Services.AddDependencyGroup();

//JWT middleware
builder.Services.AddScoped<TransactionalActionFilter>();

//EFCore Context and SQL Server
builder.Services.AddDbContext<BlitzContext>(cfg => { cfg.UseSqlServer(_config.GetConnectionString("Blitz")); });

//Identity for User & Role
builder.Services.AddIdentity<User, IdentityRole>(cfg => { cfg.User.RequireUniqueEmail = true; })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BlitzContext>()
    .AddDefaultTokenProviders();

//JWT bearer auth
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddCookie(setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(60))
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _config["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = _config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

app.UseMiddleware<BlitzMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlitzAPI v1");
        c.RoutePrefix = string.Empty;
        c.InjectStylesheet("/swagger-ui/custom.css");
    });
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.UseEndpoints(cfg => { cfg.MapControllers(); });

app.Run();