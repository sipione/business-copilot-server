using REPO.Data;
using Microsoft.AspNetCore.Builder;
using APP.Entities;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Services;
using APP.UseCases;

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

//Usecases to User
builder.Services.AddScoped<GetAllUsersUseCase>();
builder.Services.AddScoped<GetUserByIdUseCase>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<UpdateUserUseCase>();
builder.Services.AddScoped<DeleteUserUseCase>();
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();

//Usecases to Stakejolder
builder.Services.AddScoped<GetAllStakeholdersUsecase>();
builder.Services.AddScoped<GetStakeholderByIdUseCase>();
builder.Services.AddScoped<CreateStakeholderUseCase>();
builder.Services.AddScoped<UpdateStakeholderUseCase>();
builder.Services.AddScoped<DeleteStakeholderUseCase>();

//Usecases to IncomeCashFlow
builder.Services.AddScoped<GetAllIncomesUseCase>();
builder.Services.AddScoped<GetIncomeByIdUseCase>();
builder.Services.AddScoped<CreateIncomeUseCase>();
builder.Services.AddScoped<UpdateIncomeUseCase>();
builder.Services.AddScoped<DeleteIncomeUseCase>();

//Usecases to ExpenseCashFlow
builder.Services.AddScoped<GetAllExpensesUseCase>();
builder.Services.AddScoped<GetExpenseByIdUseCase>();
builder.Services.AddScoped<CreateExpenseUseCase>();
builder.Services.AddScoped<UpdateExpenseUseCase>();
builder.Services.AddScoped<DeleteExpenseUseCase>();

//Usecases to Contract
// builder.Services.AddScoped<GetAllContractsUseCase>();
// builder.Services.AddScoped<GetContractByIdUseCase>();
// builder.Services.AddScoped<CreateContractUseCase>();
// builder.Services.AddScoped<UpdateContractUseCase>();
// builder.Services.AddScoped<DeleteContractUseCase>();

// //Usecases to Progress
// builder.Services.AddScoped<GetAllProgressUseCase>();
// builder.Services.AddScoped<GetProgressByIdUseCase>();
// builder.Services.AddScoped<CreateProgressUseCase>();
// builder.Services.AddScoped<UpdateProgressUseCase>();
// builder.Services.AddScoped<DeleteProgressUseCase>();

// //Usecases to Voucher
// builder.Services.AddScoped<GetAllVouchersUseCase>();
// builder.Services.AddScoped<GetVoucherByIdUseCase>();
// builder.Services.AddScoped<CreateVoucherUseCase>();
// builder.Services.AddScoped<UpdateVoucherUseCase>();
// builder.Services.AddScoped<DeleteVoucherUseCase>();




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
