namespace GunmarksWebApi.DTOs
{
    // DTO — Data Transfer Object.
    // Это НЕ сущность БД. Это объект, который мы отдаём клиенту через API.
    public class TankDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // Римское представление уровня (готовое для отображения)
        public required string LevelRoman { get; set; }

        // Числовое значение уровня (для сортировки/фильтрации на клиенте)
        public int Level { get; set; }

        // Три отметки
        public int Mark1 { get; set; }
        public int Mark2 { get; set; }
        public int Mark3 { get; set; }

        // Информация о нации (плоские поля вместо вложенного объекта)
        public int NationId { get; set; }
        public required string NationName { get; set; }
        public required string NationCssClass { get; set; }

        // Информация о типе техники
        public int TankTypeId { get; set; }
        public required string TankTypeName { get; set; }
        public required string TankTypeShortName { get; set; }
        public required string TankTypeCssClass { get; set; }

        // Статус (строкой — чтобы клиент не зависел от enum)
        public required string Status { get; set; }
    }
}