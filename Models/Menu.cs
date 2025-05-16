namespace Piccadilly_Roma_Be.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Titolo { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public Menu() { }
    }
}
