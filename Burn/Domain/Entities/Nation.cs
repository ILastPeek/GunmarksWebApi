using Burn.Domain.Entities;

namespace Burn.Domain.Entities
{
    public class Nation
    {
        public int Id { get; set; }
        public string Name { get; set; }          // "СССР"
        public string CssClass { get; set; }      // "ussr"

        // Связь: одна нация имеет много танков
        public ICollection<Tank> Tanks { get; set; }
    }
}