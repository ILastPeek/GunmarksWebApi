namespace GunmarksWebApi.DTOs
{
    // Параметры фильтрации, сортировки и пагинации.
    // Приходят из query-строки: /api/tanks?level=10&sortOrder=mark3_desc&pageNumber=1
    public class TankFilterDto
    {
        // Поиск по названию (частичное совпадение)
        public string? SearchName { get; set; }

        // Фильтр по нации
        public int? NationId { get; set; }

        // Фильтр по типу техники
        public int? TankTypeId { get; set; }

        // Фильтр по уровню
        public int? Level { get; set; }

        // Фильтр по статусу (число из enum)
        public int? Status { get; set; }

        // Сортировка ("mark3_desc", "name", "level" и т.д.)
        public string? SortOrder { get; set; }

        // Номер страницы (по умолчанию 1)
        public int PageNumber { get; set; } = 1;

        // Размер страницы (по умолчанию 20)
        public int PageSize { get; set; } = 20;
    }
}