using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RecruitmentProcessBusiness.Mapper;
using RecruitmentProcessInfrastructure.Persistence.Context;
using RecruitmentProcessInfrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddTransient<IAppDbContext, AppDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RecruitmentAPI", Version = "v1" });
});

var _config = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseCosmos(_config["CosmosDb:AccountEnd"], _config["CosmosDb:AccountKey"], _config["CosmosDb:Database"]);
});
var assembly = AppDomain.CurrentDomain.Load("RecruitmentProcessBusiness");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
builder.Services.AddAutoMapper(typeof(ProfileMapping).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Open");
app.UseAuthorization();

app.MapControllers();

app.Run();
