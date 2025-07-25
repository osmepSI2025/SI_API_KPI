using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Entities;
using SME_API_KPI.Repository;
using SME_API_KPI.Service;
using SME_API_KPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<KPIDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));   
//Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// ✅ Register NSwag (Swagger 2.0)
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "API_SME_KPI_v1";
    config.Title = "API SME KPI";
    config.Version = "v1";
    config.Description = "API documentation using Swagger 2.0";
    config.SchemaType = NJsonSchema.SchemaType.Swagger2; // This makes it Swagger 2.0
});

builder.Services.AddScoped<MStatusRepository>();
builder.Services.AddScoped<MStatusService>();
//builder.Services.AddScoped<MPlanNameService>();
//builder.Services.AddScoped<MPlanNameRepository>();
builder.Services.AddScoped<MPlanBudgetYearRepository>();
builder.Services.AddScoped<MPlanBudgetYearService>();
builder.Services.AddScoped<MKpiTypeRepository>();
builder.Services.AddScoped<MKpiTypeService>();
builder.Services.AddScoped<MMeasureRepository>();
builder.Services.AddScoped<MMeasureService>();
builder.Services.AddScoped<MInputFormateRepository>();
builder.Services.AddScoped<MInputFormateService>();
builder.Services.AddScoped<MDimensionSystemRepository>();
builder.Services.AddScoped<MDimensionSystemService>();
builder.Services.AddScoped<MDivisionRepository>();
builder.Services.AddScoped<MDivisionService>();
builder.Services.AddScoped<MPlanPeriodRepository>();
builder.Services.AddScoped<MPlanPeriodService>();

builder.Services.AddScoped<MPlanTargetDescriptionRepository>();
builder.Services.AddScoped<MPlanTargetDescriptionService>();
builder.Services.AddScoped<MPlanKpiDescriptionService>();
builder.Services.AddScoped<MPlanKpiDescriptionRepository>();

builder.Services.AddScoped<MPlanweightService>();
builder.Services.AddScoped<MPlanweightRepository>();
builder.Services.AddScoped<MPlanResultService>();
builder.Services.AddScoped<MPlanResultRepository>();
builder.Services.AddScoped<MPlanKpiListRepository>();
builder.Services.AddScoped<MPlanKpiListService>();

// builder.Services.AddScoped<MPlanKpiService>();
//builder.Services.AddScoped<MPlanKpiRepository>();


builder.Services.AddScoped<MExportEvalSystemRepository>();
builder.Services.AddScoped<MExportEvalSystemService>();

builder.Services.AddScoped<MKpiSystemKpiTargetService>();
builder.Services.AddScoped<MKpiSystemKpiTargetRepository>();

builder.Services.AddScoped<MKpiSystemAssignService>();
builder.Services.AddScoped<MKpiSystemAssignRepository>();


builder.Services.AddScoped<IApiInformationRepository, ApiInformationRepository>();
builder.Services.AddScoped<ICallAPIService, CallAPIService>(); // Register ICallAPIService with CallAPIService
builder.Services.AddHttpClient<CallAPIService>();

builder.Services.AddHostedService< JobSchedulerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseOpenApi();     // Serve the Swagger JSON
    app.UseSwaggerUi3();  // Use Swagger UI v3
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
