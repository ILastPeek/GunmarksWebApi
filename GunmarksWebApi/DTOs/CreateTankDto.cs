using System.ComponentModel.DataAnnotations;

namespace GunmarksWebApi.DTOs
{
    // DTO для создания нового танка (POST /api/tanks).
    // Содержит только те поля, которые клиент должен передать.
    public class CreateTankDto
    {
        [Required(ErrorMessage = "Название танка обязательно")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 100 символов")]
        public string Name { get; set; }

        [Range(1, 11, ErrorMessage = "Уровень должен быть от 1 до 11")]
        public int Level { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Отметка не может быть отрицательной")]
        public int Mark1 { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Отметка не может быть отрицательной")]
        public int Mark2 { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Отметка не может быть отрицательной")]
        public int Mark3 { get; set; }

        [Range(0, 3, ErrorMessage = "Статус должен быть от 0 до 3")]
        public int Status { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "NationId должен быть положительным")]
        public int NationId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "TankTypeId должен быть положительным")]
        public int TankTypeId { get; set; }
    }
}