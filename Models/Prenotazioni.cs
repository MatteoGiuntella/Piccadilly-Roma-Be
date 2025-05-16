using System;

namespace Piccadilly_Roma_Be.Models
{
    public class Prenotazione
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public int NumeroPersone { get; set; }
        public string Messaggio { get; set; } = string.Empty;

        public Prenotazione() { }
    }
}