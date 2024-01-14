using Microsoft.EntityFrameworkCore;
using System;
using RestfulBankApi.Data;
using RestfulBankApi.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using RestfulBankApi.Models;
using RestfulBankApi.MapperProfile;
using RestfulBankApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Repositories;
using RestfulBankApi.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "BankAccountAPI", Version = "v1",
        Description = "Bu API, banka hesaplarý , kredi iþlemleri , hesaplar arasý transferler ve para iþlemlerini yapar."


    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[]{}
        }
    });
});

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(options =>
    {

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["SecretKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestfulBankDb")));
builder.Services.AddScoped<JwtAuthenticationManager>(t => new JwtAuthenticationManager(jwtSettings["SecretKey"], t.GetRequiredService<AppDbContext>(), t.GetRequiredService<PasswordHasher>()));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("MinimumBalance"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("DailyLimitSettings"));

builder.Services.AddSingleton<PasswordHasher>();
builder.Services.AddSingleton<RandomIBANSeeder>();
builder.Services.AddScoped<ICreditScore, CreditScoreService>();
builder.Services.AddHostedService<AutomaticPaymentService>();
builder.Services.AddScoped<IScopedProcessingService,ScopedProcessingService>();

builder.Services.AddScoped<DailyLimits>();
builder.Services.AddScoped<IAccountTransactionRepository, AccountTransactionRepository>();
builder.Services.AddScoped<IAccountTransactionService, AccountTransactionService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITransferRepository, TransferRepository>();
builder.Services.AddScoped<ITransferService, TransferService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.Requirements.Add(new RoleRequirement("Admin")));
    options.AddPolicy("AuditorPolicy", policy => policy.Requirements.Add(new RoleRequirement("Auditor")));
});
builder.Services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
