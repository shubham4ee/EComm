using UserServer.BLL.Services;
using UserServer.DAL.DataContext;
using UserServer.DAL.Repositories;
using UserServer.BLL.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Minio;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
#region Swagger authentication header configuration
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the bearer scheme. Enter Bearer [Space] add your token in the text input. Example: Bearer swersdf877sdf",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference()
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

#endregion

#region Adding minioClient dependency service
builder.Services.AddMinio(configureClient =>
{
    // Construct the full endpoint URL using hostUrl and hostPort
    var hostUrl = builder.Configuration.GetValue<string>("STORAGE_SERVICE_OPTIONS:MINIO:hostUrl");
    var hostPort = builder.Configuration.GetValue<int>("STORAGE_SERVICE_OPTIONS:MINIO:hostPort");
    var endpoint = $"{hostUrl}:{hostPort}"; // Combine the host and port into an endpoint URL

    configureClient
        .WithEndpoint(endpoint)  // Use the full endpoint
        .WithCredentials(
            builder.Configuration.GetValue<string>("STORAGE_SERVICE_OPTIONS:MINIO:accessKeyId"),
            builder.Configuration.GetValue<string>("STORAGE_SERVICE_OPTIONS:MINIO:secretAccessKey")
        )
        .Build();
});

#endregion

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Project"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ICADFileRepository, CADFileRepository>();
builder.Services.AddScoped<ICADFileService, CADFileService>();
builder.Services.AddScoped<ICADFileRepository, CADFileRepository>();
builder.Services.AddScoped<IUserProjectsService, UserProjectsService>();
builder.Services.AddScoped<IUserProjectsRepository, UserProjectsRepository>();



builder.Services.AddTransient<AuthService>();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddScoped<IMinioService, MinioService>();

# region Adding CORS policies to the services   

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
    

});
#endregion
#region JWT AUTHONTICATION CONFIGURATION

var localKey = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTLocalSecret"));

// creating audiance and issuer strings defined in appsetting.json
string LocalAudience = builder.Configuration.GetValue<string>("LocalAudience");
string LocalIssuer = builder.Configuration.GetValue<string>("LocalIssuer");


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(localKey),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
}).AddJwtBearer("LoginForLocalUser", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(localKey),
        ValidateIssuer = true,
        ValidIssuer = LocalIssuer,
        ValidateAudience = true,
        ValidAudience = LocalAudience,
    };
});

#endregion


var app = builder.Build();
// -----------------------ADD THIS SECTION TO APPLY MIGRATIONS ON STARTUP----------------------
try
{
    // Create a scope to resolve services
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // Apply pending migrations (creates DB and tables if needed)
    dbContext.Database.Migrate();


    // OPTIONAL: Add initial seed data here if you have any
    // SeedData.Initialize(scope.ServiceProvider); 
}
catch (Exception ex)
{
    // Log the error. This is important if the Postgres container isn't ready yet.
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while migrating the database.");
    // The application will likely shut down or throw an error here if the connection fails repeatedly.
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
