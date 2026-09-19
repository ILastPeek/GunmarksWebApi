using Microsoft.EntityFrameworkCore;
using Burn.Data;
using Burn.Repositories.Abstract;
using Burn.Repositories.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 1. ¡¿«¿ ƒ¿ÕÕ€’
// ============================================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ============================================================
// 2. –≈œŒ«»“Œ–»»
// ============================================================
builder.Services.AddScoped<ITankRepository, EFTankRepository>();
builder.Services.AddScoped<INationRepository, EFNationRepository>();
builder.Services.AddScoped<ITankTypeRepository, EFTankTypeRepository>();

// ============================================================
// 3.  ŒÕ“–ŒÀÀ≈–€ + SWAGGER
// ============================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ============================================================
// 4. »Õ»÷»¿À»«¿÷»ﬂ ¡ƒ
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
        logger.LogError(ex, "Œ¯Ë·Í‡ ÔË ËÌËˆË‡ÎËÁ‡ˆËË ·‡Á˚ ‰‡ÌÌ˚ı");
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