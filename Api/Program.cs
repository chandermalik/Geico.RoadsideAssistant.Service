using RoadsideAssistant.DataAccess;
using RoadsideAssistant.Manager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register the Swagger generator, defining one or more Swagger documents
builder.Services.AddSwaggerGen();


// Add services to the container.
builder.Services.AddScoped<IRoadsideAssistantManager, RoadSideAssistanceManager>();
builder.Services.AddScoped<IRoadsideAssistantRepository, RoadsideAssistantRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
