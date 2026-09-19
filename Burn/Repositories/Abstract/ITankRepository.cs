using Burn.Domain.Entities;

namespace Burn.Repositories.Abstract
{
    public interface ITankRepository
    {
        // Получить все танки (IQueryable — запрос ещё не выполнен,
        // это позволяет сервису добавить фильтры перед выполнением)
        IQueryable<Tank> GetAll();

        // Найти танк по Id (вместе с нацией и типом)
        Task<Tank> GetByIdAsync(int id);

        // Добавить новый танк
        Task AddAsync(Tank entity);

        // Обновить существующий
        void Update(Tank entity);

        // Удалить
        void Delete(Tank entity);

        // Сохранить изменения в БД
        Task SaveChangesAsync();

    }
}