using System.Text.Json;
using Burn.Domain.Entities;
using Burn.Domain.Enums;

namespace Burn.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Nations.Any())
                return;

            // ============================================================
            // 1. НАЦИИ
            // ============================================================
            var nations = new Nation[]
            {
                new Nation { Name = "СССР",             CssClass = "ussr" },
                new Nation { Name = "Германия",         CssClass = "germany" },
                new Nation { Name = "США",              CssClass = "usa" },
                new Nation { Name = "Китай",            CssClass = "china" },
                new Nation { Name = "Франция",          CssClass = "france" },
                new Nation { Name = "Великобритания",   CssClass = "uk" },
                new Nation { Name = "Япония",           CssClass = "japan" },
                new Nation { Name = "Чехословакия",     CssClass = "czech" },
                new Nation { Name = "Швеция",           CssClass = "sweden" },
                new Nation { Name = "Польша",           CssClass = "poland" },
                new Nation { Name = "Италия",           CssClass = "italy" },
                new Nation { Name = "Сборная наций",    CssClass = "intunion" }
            };
            context.Nations.AddRange(nations);
            context.SaveChanges();

            // ============================================================
            // 2. ТИПЫ ТЕХНИКИ
            // ============================================================
            var tankTypes = new TankType[]
            {
                new TankType { Name = "Лёгкие танки",   ShortName = "ЛТ",  CssClass = "lightTank" },
                new TankType { Name = "Средние танки",  ShortName = "СТ",  CssClass = "mediumTank" },
                new TankType { Name = "Тяжёлые танки",  ShortName = "ТТ",  CssClass = "heavyTank" },
                new TankType { Name = "ПТ-САУ",         ShortName = "ПТ",  CssClass = "AT-SPG" },
                new TankType { Name = "САУ",            ShortName = "САУ", CssClass = "SPG" }
            };
            context.TankTypes.AddRange(tankTypes);
            context.SaveChanges();

            // ============================================================
            // 3. ТАНКИ ИЗ JSON
            // ============================================================
            // Определяем путь к JSON-файлу.
            // AppContext.BaseDirectory — папка, где лежит .exe (bin/Debug/net8.0/).
            // Мы копировали tanks.json туда через "Copy if newer".
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "tanks.json");

            if (!File.Exists(jsonPath))
            {
                // Если файл не найден — не падаем, но логируем.
                Console.WriteLine($"⚠ Файл не найден: {jsonPath}");
                return;
            }

            // Читаем содержимое файла
            var json = File.ReadAllText(jsonPath);

            // Настройки десериализации:
            // PropertyNameCaseInsensitive = true — чтобы "name" в JSON совпало с "Name" в C#
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Десериализуем JSON в список объектов TankSeedDto
            var seedTanks = JsonSerializer.Deserialize<List<TankSeedDto>>(json, options);

            if (seedTanks == null || seedTanks.Count == 0)
                return;

            // Справочники в память — чтобы не искать их в БД на каждой итерации
            var nationsDict = context.Nations.ToDictionary(n => n.CssClass, n => n.Id);
            var typesDict = context.TankTypes.ToDictionary(t => t.CssClass, t => t.Id);

            var tanks = new List<Tank>();

            foreach (var seed in seedTanks)
            {
                // Ищем Id нации по её CSS-классу
                if (!nationsDict.TryGetValue(seed.NationCss, out var nationId))
                    continue; // пропускаем, если такой нации нет

                // Ищем Id типа по его CSS-классу
                if (!typesDict.TryGetValue(seed.TypeCss, out var typeId))
                    continue;

                // Преобразуем строку статуса в enum
                if (!Enum.TryParse<TankStatus>(seed.Status, out var status))
                    status = TankStatus.Standard;

                tanks.Add(new Tank
                {
                    Name = seed.Name,
                    Level = seed.Level,
                    Mark1 = seed.Mark1,
                    Mark2 = seed.Mark2,
                    Mark3 = seed.Mark3,
                    NationId = nationId,
                    TankTypeId = typeId,
                    Status = status
                });
            }

            context.Tanks.AddRange(tanks);
            context.SaveChanges();
        }
    }
}