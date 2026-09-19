using System.ComponentModel.DataAnnotations.Schema;
using Burn.Domain.Enums;
using Burn.Helpers;

namespace Burn.Domain.Entities
{
    public class Tank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        public int Mark1 { get; set; }   // 65%
        public int Mark2 { get; set; }   // 85%
        public int Mark3 { get; set; }   // 95%

        public TankStatus Status { get; set; }

        // Внешние ключи
        public int NationId { get; set; }
        public int TankTypeId { get; set; }

        // Навигационные свойства (связи)
        public Nation Nation { get; set; }
        public TankType TankType { get; set; }

        [NotMapped]
        public string LevelRoman => RomanNumerals.ToRoman(Level);
    }
}