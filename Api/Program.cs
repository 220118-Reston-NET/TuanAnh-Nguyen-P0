global using Serilog;
using BL;
using DL;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration().WriteTo.File("./logs/server.txt").CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerRepository>(repo => new CustomerSQLRepository(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IStoreFrontRepository>(repo => new StoreFrontSQLRepository(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IProductRepository>(repo => new ProductSQLRepository(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IOrderRepository>(repo => new OrderSQLRepository(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IInventoryRepository>(repo => new InventorySQLRepository(builder.Configuration.GetConnectionString("Reference2DB")));

builder.Services.AddScoped<ICustomerBL, CustomerBL>();
builder.Services.AddScoped<IStoreFrontBL, StoreFrontBL>();
builder.Services.AddScoped<IProductBL, ProductBL>();
builder.Services.AddScoped<IOrderBL, OrderBL>();
builder.Services.AddScoped<IInventoryBL, InventoryBL>();

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
