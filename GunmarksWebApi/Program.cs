using Microsoft.EntityFrameworkCore;
using GunmarksWebApi.Data;
using GunmarksWebApi.Repositories.Abstract;
using GunmarksWebApi.Repositories.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 1. ���� ������
// ============================================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ============================================================
// 2. �����������
// ============================================================
builder.Services.AddScoped<ITankRepository, EFTankRepository>();
builder.Services.AddScoped<INationRepository, EFNationRepository>();
builder.Services.AddScoped<ITankTypeRepository, EFTankTypeRepository>();

// ============================================================
// 3. ����������� + SWAGGER
// ============================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ============================================================
// 4. ������������� ��
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
        logger.LogError(ex, "������ ��� ������������� ���� ������");
    }
}

// ============================================================
// 5. MIDDLEWARE
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