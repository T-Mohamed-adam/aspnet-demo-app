using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.ServiceContracts;
using EmployeeAdminPortal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


using Serilog;
using System.Text;
using EmployeeAdminPortal.Helpers;

var builder = WebApplication.CreateBuilder(args);


// JWT Configuration
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddAuthentication();

// Access auth user data
builder.Services.AddHttpContextAccessor();

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Console logging
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // File logging
    .WriteTo.Seq("http://localhost:5341") // Seq logging (replace with your Seq server URL)
    .CreateLogger();

builder.Host.UseSerilog();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllersWithViews(); // Enables MVC and Razor views

builder.Services.AddScoped<IEmployeeService,    EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOwnerService, OwnerService>();

// Register services
builder.Services.AddScoped<PasswordHasher<object>>();

builder.Services.AddScoped<FileHelper>(); // Register FileHelper as a service
builder.Services.AddScoped<GenerateRandomNumberHelper>();
builder.Services.AddScoped<PasswordHashingHelper>();
builder.Services.AddScoped<MembershipNumberHelper>();

// Database connection 
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add CORS services
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


var app = builder.Build();

// Use the CORS middleware
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
