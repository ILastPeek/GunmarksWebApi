namespace GunmarksWebApi.Domain.Entities
{
    public class TankType
    {
        public int Id { get; set; }
        public required string Name { get; set; }          // "Лёгкие танки"
        public required string ShortName { get; set; }     // "ЛТ"
        public required string CssClass { get; set; }      // "lightTank"

        public ICollection<Tank> Tanks { get; set; } = new List<Tank>();
    }
} 