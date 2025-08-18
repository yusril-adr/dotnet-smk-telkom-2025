using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Infrastructure.Middlewares;
using dotnet_smk_telkom_2025.Infrastructure.Shared;
using dotnet_smk_telkom_2025.Repositories;
using dotnet_smk_telkom_2025.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<InMemoryDbContext>();
builder.Services.AddDbContext<SQLServerDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration["DB:SQLServer:ConnectionString"]);
});

builder.Services.AddHttpContextAccessor(); // -> So controllers and utils can access HttpContext

/* ------------------------------ Repositories ------------------------------ */
builder.Services.AddScoped<UserQueryRepository>();
builder.Services.AddScoped<UserStoreRepository>();

builder.Services.AddScoped<PostQueryRepository>();
builder.Services.AddScoped<PostStoreRepository>();

builder.Services.AddScoped<PostCommentQueryRepository>();
builder.Services.AddScoped<PostCommentStoreRepository>();

/* -------------------------------- Services -------------------------------- */
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<PostCommentService>();

/* ---------------------------------- Utils --------------------------------- */
builder.Services.AddSingleton<AuthUtil>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandler>();
app.UseMiddleware<AuthorizationMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
