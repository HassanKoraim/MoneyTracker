using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoneyTracker.API;
using MoneyTracker.API.Exceptions;
using MoneyTracker.Application;
using MoneyTracker.Application.Behaviors;
using MoneyTracker.Application.RepositoryContracts;
using MoneyTracker.Infrastructure.Data;
using MoneyTracker.Infrastructure.Repositroies;
using System.Text;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddFixedWindowLimiter("fixed", opt =>
//    {
//        opt.PermitLimit = 4;
//        opt.Window = TimeSpan.FromSeconds(12);
//        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//        opt.QueueLimit = 2;
//    });
//});
/// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepositoryContracts<>), typeof(Repository<>));
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddAutoMapper(typeof(MappingConfig));

//builder.Services.AddIdentityApiEndpoints<IdentityUser>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));
//Rate Limiting 
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name
                          ?? httpContext.Connection.RemoteIpAddress?.ToString()
                          ?? "anonymous-client",
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 20,
                QueueLimit = 20,
                Window = TimeSpan.FromMinutes(1)
            }));
    options.RejectionStatusCode = 429;  // Too Many Requests
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
 .AddJwtBearer(x =>
 {
     x.RequireHttpsMetadata = false;
     x.SaveToken = true;
     x.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidIssuer = builder.Configuration.GetValue<string>("AppSettings:Issuer"),
         ValidateAudience = true,
         ValidAudience = builder.Configuration.GetValue<string>("AppSettings:Audience"),
         ValidateLifetime = true,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
             builder.Configuration.GetValue<string>("AppSettings:Token")!)),
         ValidateIssuerSigningKey = true
     };

 });
builder.Services.AddControllers(option =>
{
    //option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
        "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        //Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
             new OpenApiSecurityScheme()
             {
                 Reference = new OpenApiReference()
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 },
                 Scheme = "oauth2",
                 Name = "Bearer",
                 In = ParameterLocation.Header
             },
             new List<string>()
        }

    });
});
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
    //cfg.RegisterServicesFromAssembly(typeof(GetParentCategoriesByTypeQueryHandler).Assembly);
    //cfg.RegisterServicesFromAssembly(typeof(GetCategoryByIdQueryHandler).Assembly);
});
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();
app.UseRouting();
app.UseRateLimiter();   
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers();

app.Run();
