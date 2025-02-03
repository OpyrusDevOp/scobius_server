using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Scobius;
using Scobius.Handlers;
using Scobius.Hubs;
using Scobius.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var firebaseApp = FirebaseApp.Create(
    new AppOptions()
    {
        Credential = GoogleCredential.FromFile(builder.Configuration["GoogleCredentialFile"]),
        ProjectId = builder.Configuration["FirebaseID"],
    }
);
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<ChatService>();
builder.Services.AddTransient<FrienshipService>();
builder.Services.AddTransient<MediaService>();


builder.Services.AddSingleton(FirebaseAuth.GetAuth(firebaseApp));
builder.Services.AddSingleton<IUserIdProvider, HubUserIdProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "Firebase";
        options.DefaultChallengeScheme = "Firebase";
    })
.AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(
  "Firebase", options => { });

// Add authorization
builder.Services.AddAuthorization(options =>
    {
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    });
builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddDbContext<ScobiusTest_Context>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("local")));

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<MainHub>("/app");

using (var scope = app.Services.CreateScope())
{
    var userService = scope.ServiceProvider.GetRequiredService<UserService>();

    try
    {
        await userService.RetrieveUser();
        Console.WriteLine("Successfully fetched and stored verified users.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error fetching users: {ex.Message}");
    }
}

app.Run();
