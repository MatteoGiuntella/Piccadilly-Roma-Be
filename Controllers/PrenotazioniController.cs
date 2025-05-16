using System;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using Piccadilly_Roma_Be.Models;

namespace Piccadilly_Roma_Be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrenotazioniController : ControllerBase
    {
        private readonly IConfiguration _config;

        public PrenotazioniController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult InviaPrenotazione([FromBody] Prenotazione prenotazione)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(_config["EmailSettings:To"]));
            email.Subject = $"Nuova prenotazione da {prenotazione.Nome}";
            email.Body = new TextPart("plain")
            {
                Text = $"Nome: {prenotazione.Nome}\n" +
                       $"Email: {prenotazione.Email}\n" +
                       $"Telefono: {prenotazione.Telefono}\n" +
                       $"Data: {prenotazione.Data}\n" +
                       $"Persone: {prenotazione.NumeroPersone}\n" +
                       $"Messaggio: {prenotazione.Messaggio}"
            };

            using var smtp = new SmtpClient();
            smtp.Connect(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), true);
            smtp.Authenticate(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok("Prenotazione inviata con successo.");
        }
    }
}

