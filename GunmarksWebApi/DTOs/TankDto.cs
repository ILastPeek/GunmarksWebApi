namespace GunmarksWebApi.DTOs
{
    // DTO — Data Transfer Object.
    // Это НЕ сущность БД. Это объект, который мы отдаём клиенту через API.
    public class TankDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Римское представление уровня (готовое для отображения)
        public string LevelRoman { get; set; }

        // Числовое значение уровня (для сортировки/фильтрации на клиенте)
        public int Level { get; set; }

        // Три отметки
        public int Mark1 { get; set; }
        public int Mark2 { get; set; }
        public int Mark3 { get; set; }

        // Информация о нации (плоские поля вместо вложенного объекта)
        public int NationId { get; set; }
        public string NationName { get; set; }
        public string NationCssClass { get; set; }

        // Информация о типе техники
        public int TankTypeId { get; set; }
        public string TankTypeName { get; set; }
        public string TankTypeShortName { get; set; }
        public string TankTypeCssClass { get; set; }

        // Статус (строкой — чтобы клиент не зависел от enum)
        public string Status { get; set; }
    }
}