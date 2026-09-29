namespace GunmarksWebApi.Data
{
    // Вспомогательный класс — модель данных из JSON-файла.
    // Не путать с DTO API! Это DTO для десериализации seed-файла.
    public class TankSeedDto
    {
        public required string Name { get; set; }
        public int Level { get; set; }
        public int Mark1 { get; set; }
        public int Mark2 { get; set; }
        public int Mark3 { get; set; }
        public required string NationCss { get; set; }
        public required string TypeCss { get; set; }
        public required string Status { get; set; }
    }
}