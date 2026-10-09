namespace GunmarksWebApi.DTOs
{
    public class NationDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }          // "СССР"
        public required string CssClass { get; set; }      // "ussr"
    }
}
