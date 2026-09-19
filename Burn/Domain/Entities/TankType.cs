using System.Threading.Tasks;
using System.Collections.Generic;

namespace Burn.Domain.Entities
{
    public class TankType
    {
        public int Id { get; set; }
        public string Name { get; set; }          // "Лёгкие танки"
        public string ShortName { get; set; }     // "ЛТ"
        public string CssClass { get; set; }      // "lightTank"

        public ICollection<Tank> Tanks { get; set; }
    }
} 