using Microsoft.EntityFrameworkCore;
using GunmarksWebApi.Data;
using GunmarksWebApi.Repositories.Abstract;
using GunmarksWebApi.Repositories.EntityFramework;
using GunmarksWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// Настройка БД
// ============================================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ============================================================
// Репозитории
// ============================================================
builder.Services.AddScoped<ITankRepository, EFTankRepository>();
builder.Services.AddScoped<INationRepository, EFNationRepository>();
builder.Services.AddScoped<ITankTypeRepository, EFTankTypeRepository>();

// ============================================================
// РЕГИСТРАЦИЯ СЕРВИСОВ
// ============================================================
builder.Services.AddScoped<ITankService, TankService>();

// ============================================================
// Контроллеры + Swagger
// ============================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ============================================================
// Инициализация БД
// ============================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при инициализации базы данных");
    }
}

// ============================================================
// Middleware
// ============================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();