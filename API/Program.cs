using REPO.Data;
using Microsoft.AspNetCore.Builder;
using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddScoped<ICashFlowRepository, CashFlowRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IProgressRepository, ProgressRepository>();
builder.Services.AddScoped<IStakeholderRepository, StakeholderRepository>();
builder.Services.AddScoped<ISubAccountRepository, SubAccountRepository>();
builder.Services.AddSingleton<ICryptographyServices, CryptographyServices>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserAuthorizationService, UserAuthorizationService>();

var app = builder.Build();

// Inicializar banco de dados dentro do escopo
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    string path = Path.Combine(Directory.GetCurrentDirectory(), "EasyBussiness.sqlite");
    if (!File.Exists(path))
    {
        SQLiteInitializer.Initialize(dbContext);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
