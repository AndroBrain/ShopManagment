using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using ShopManagmentAPI.data.db.user;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain;
using ShopManagmentAPI.domain.model.authentication;
using ShopManagmentAPI.domain.model.user;
using ShopManagmentAPI.domain.service.email;
using ShopManagmentAPI.domain.service.user;
using AuthenticationService = ShopManagmentAPI.domain.service.user.AuthenticationService;
using IAuthenticationService = ShopManagmentAPI.domain.service.user.IAuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Configuration.GetSection("Authentication").Bind(new AuthenticationSettings());
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
})
    .AddJwtBearer(cfg =>
    { 
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = AuthenticationSettings.Issuer,
            ValidAudience = AuthenticationSettings.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(AuthenticationSettings.Key))
        };
    });

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{ 
    options.AddPolicy("FrontendApp", builder =>
    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!File.Exists(DbSettings.dbPath))
{
    File.Create(DbSettings.dbPath).Close();
    var user = new RegisterDto()
    {
        Name = "Admin",
        Email = "admin@shopmanagment.com",
        Password = "123"
    };
    IAuthenticationService authService = new AuthenticationService(new UserRepository(new UserDb()), new EmailSender());
    authService.RegisterUser(user, new UserRole() { Name = "admin" });
}

app.UseCors("FrontendApp");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
