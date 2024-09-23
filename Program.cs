using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudentTranscriptPortal.Data;
using StudentTranscriptPortal.Helpers;
using StudentTranscriptPortal.Implements;
using StudentTranscriptPortal.Services;
using System.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StudentContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IUserLoginService, UserLoginService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("DuluxCoarsPolicy",
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:3000",
                                                  "http://localhost:7166")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

//Setting up JWT 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Validate the server (issuer)
        ValidateAudience = true, // Validate the recipient (audience)
        ValidateLifetime = true, // Validate token expiry
        ValidateIssuerSigningKey = true, // Validate signature
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Set the valid issuer
        ValidAudience = builder.Configuration["Jwt:Audience"], // Set the valid audience
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(); // Add authorization support

//setting up CORS
var app = builder.Build();
//app.UseCors(options =>
//options.WithOrigins("http://localhost:4200")
//.AllowAnyMethod()
//.AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Enable JWT Authentication
app.UseAuthorization();  // Enable Authorization

app.UseCors("DuluxCoarsPolicy");

app.MapControllers();
//// Generate JWT token for testing (Remove this later in production)
//var token = Authhelper.GenerateToken(
//    builder.Configuration["Jwt:Key"],
//    builder.Configuration["Jwt:Issuer"],
//    builder.Configuration["Jwt:Audience"]);
//Debug.WriteLine($"JWT Token: {token}"); // Output token to console


app.Run();
