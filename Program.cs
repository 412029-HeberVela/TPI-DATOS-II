using TPIntegrador.Data;
using TPIntegrador.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mongoSettings = builder.Configuration.GetSection("MongoDB");

builder.Services.AddSingleton<MongoContext>(sp =>
    new MongoContext(
        mongoSettings["ConnectionString"],
        mongoSettings["DatabaseName"]
    ));

builder.Services.AddScoped<ReportService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
