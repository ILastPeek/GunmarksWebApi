namespace GunmarksWebApi.DTOs
{
    // Универсальный контейнер для постраничной выдачи.
    // T — тип элемента (в нашем случае TankDto).
    // Благодаря <T> этот класс можно переиспользовать для любых сущностей.
    public class PaginatedResultDto<T>
    {
        // Список элементов на текущей странице
        public IEnumerable<T> Items { get; set; }

        // Общее количество записей (без учёта пагинации)
        public int TotalItems { get; set; }

        // Текущая страница (1, 2, 3...)
        public int PageNumber { get; set; }

        // Размер страницы (сколько максимум элементов на странице)
        public int PageSize { get; set; }

        // Общее количество страниц (вычисляется автоматически)
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        // Удобный флаг: есть ли следующая страница
        public bool HasNextPage => PageNumber < TotalPages;

        // Удобный флаг: есть ли предыдущая страница
        public bool HasPreviousPage => PageNumber > 1;
    }
}