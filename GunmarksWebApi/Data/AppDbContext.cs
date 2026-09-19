using Microsoft.EntityFrameworkCore;
using GunmarksWebApi.Domain.Entities;

namespace GunmarksWebApi.Data
{
    // Класс-контекст базы данных. Он наследуется от DbContext (базовый класс EF Core)
    public class AppDbContext : DbContext
    {
        // Конструктор. Принимает настройки подключения к БД и передаёт их в базовый класс
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // ============================================================
        // 1. DbSet — это "таблицы" в базе данных
        // Каждый DbSet соответствует одной таблице
        // ============================================================

        // Таблица "Tanks" (танки)
        public DbSet<Tank> Tanks { get; set; }
        public DbSet<Nation> Nations { get; set; }
        public DbSet<TankType> TankTypes { get; set; }

        // ============================================================
        // 2. OnModelCreating — настройка моделей для БД
        // Здесь мы задаём правила: какие поля уникальные, как хранить enum и т.д.
        // ============================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Нация: поле Name должно быть уникальным (чтобы не было двух "СССР")
            modelBuilder.Entity<Nation>()
                .HasIndex(n => n.Name)
                .IsUnique();

            // Тип техники: поле Name должно быть уникальным
            modelBuilder.Entity<TankType>()
                .HasIndex(t => t.Name)
                .IsUnique();

            // Танк: поле Name должно быть уникальным (чтобы не было двух "Mausekönig")
            modelBuilder.Entity<Tank>()
                .HasIndex(t => t.Name)
                .IsUnique();

            // ============================================================
            // 3. Настройка хранения enum'а TankStatus
            // По умолчанию EF хранит enum как строку ("Standard", "Premium"...)
            // Мы хотим хранить как число (0, 1, 2, 3) — это быстрее и компактнее
            // ============================================================
            modelBuilder.Entity<Tank>()
                .Property(t => t.Status)
                .HasConversion<int>();

            // ============================================================
            // 4. Настройка связей (внешних ключей)
            // ============================================================

            // У танка есть Нация (Nation). Один танк принадлежит одной нации.
            // У одной нации может быть много танков.
            modelBuilder.Entity<Tank>()
                .HasOne(t => t.Nation)           // У танка есть одна нация
                .WithMany(n => n.Tanks)          // У нации есть много танков
                .HasForeignKey(t => t.NationId)  // Внешний ключ — NationId
                .OnDelete(DeleteBehavior.Restrict); // Не удалять нацию, если есть танки

            // У танка есть Тип (TankType). Один танк принадлежит одному типу.
            // У одного типа может быть много танков.
            modelBuilder.Entity<Tank>()
                .HasOne(t => t.TankType)         // У танка есть один тип
                .WithMany(tt => tt.Tanks)        // У типа есть много танков
                .HasForeignKey(t => t.TankTypeId) // Внешний ключ — TankTypeId
                .OnDelete(DeleteBehavior.Restrict); // Не удалять тип, если есть танки

            // Вызов базового метода (для стандартных настроек EF)
            base.OnModelCreating(modelBuilder);
        }
    }
}