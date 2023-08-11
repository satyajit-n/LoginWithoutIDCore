using LoginWithoutIDCore.CustomAuth;
using LoginWithoutIDCore.Data;
using LoginWithoutIDCore.Mapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LoginDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("LoginWithoutIDCore")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.SlidingExpiration = false;
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                //DateTime expirationDateTime = DateTime.UtcNow.AddMinutes(10);
                options.Events = new CookieAuthenticationEvents
                {
                    OnSigningIn = context =>
                    {
                        DateTime expirationDateTime = DateTime.UtcNow.AddMinutes(2);
                        // Set the expiration datetime for the cookie
                        context.CookieOptions.Expires = expirationDateTime;


                        return Task.CompletedTask;
                    }
                };
                //options.Cookie.Domain = ".mydomain.com";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.Name = "AuthCookie";

                //options.ExpireTimeSpan = expirationDateTime;
            });

//var serviceProvider = builder.Services.BuildServiceProvider();
//var dbContext = serviceProvider.GetRequiredService<LoginDbContext>();
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("UniqueSessionPolicy", policy =>
//    {
//        policy.Requirements.Add(new UniqueSessionRequirement(dbContext));
//    });
//});
//builder.Services.AddScoped<IAuthorizationHandler, UniqueSessionRequirement>();

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
