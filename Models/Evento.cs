namespace Piccadilly_Roma_Be.Models
{
    public class Evento
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }

        public Evento() { }
    }
}
