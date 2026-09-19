namespace GunmarksWebApi.Data
{
    // Вспомогательный класс — модель данных из JSON-файла.
    // Не путать с DTO API! Это DTO для десериализации seed-файла.
    public class TankSeedDto
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Mark1 { get; set; }
        public int Mark2 { get; set; }
        public int Mark3 { get; set; }
        public string NationCss { get; set; }
        public string TypeCss { get; set; }
        public string Status { get; set; }
    }
}