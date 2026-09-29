namespace GunmarksWebApi.Domain.Entities
{
    public class Nation
    {
        public int Id { get; set; }
        public required string Name { get; set; }          // "СССР"
        public required string CssClass { get; set; }      // "ussr"

        // Связь: одна нация имеет много танков
        public ICollection<Tank> Tanks { get; set; } = new List<Tank>();
    }
}