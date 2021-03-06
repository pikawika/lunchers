﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Lunchers.Models;
using Lunchers.Models.Domain;
using Lunchers.Models.IRepositories;
using Lunchers.Models.Repositories;
using Lunchers.Models.ViewModels.Reservatie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Lunchers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReservatieController : ControllerBase
    {
        private IReservatieRepository _reservatieRepository;
        private IKlantRepository _klantRepository;
        private ILunchRespository _lunchRespository;
        private IHandelaarRepository _handelaarRepository;

        public ReservatieController(IReservatieRepository reservatieRepository, IKlantRepository klantRepository, ILunchRespository lunchRespository, IHandelaarRepository handelaarRepository)
        {
            _reservatieRepository = reservatieRepository;
            _klantRepository = klantRepository;
            _lunchRespository = lunchRespository;
            _handelaarRepository = handelaarRepository;
        }

        // GET: api/Reservatie
        [HttpGet]
        public IActionResult Get()
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                List<Reservatie> reservaties = _reservatieRepository.GetAllFromCustomer(int.Parse(User.FindFirst("gebruikersId")?.Value)).OrderByDescending(r => r.Datum).ToList();
                return Ok(reservaties);
            }
            else if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "handelaar")
            {
                List<Reservatie> reservaties = _reservatieRepository.GetAllFromMerchant(int.Parse(User.FindFirst("gebruikersId")?.Value)).ToList();
                reservaties.Reverse();
                return Ok(reservaties);
            }
            else if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                List<Reservatie> reservaties = _reservatieRepository.GetAll().ToList();
                return Ok(reservaties);
            }
            return Unauthorized(new { error = "U bent niet aangemeld." });
        }

        // GET: api/Reservatie/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (User.FindFirst("gebruikersId")?.Value != null)
            {
                Reservatie reservatie = _reservatieRepository.GetById(id);
                if (reservatie != null)
                {
                    if (User.FindFirst("rol")?.Value == "klant")
                    {
                        if (reservatie.Klant.GebruikerId == int.Parse(User.FindFirst("gebruikersId")?.Value))
                        {
                            return Ok(reservatie);
                        }
                        return Unauthorized(new { error = "Deze reservatie behoort niet toe aan deze klant." });
                    }
                    else if (User.FindFirst("rol")?.Value == "handelaar")
                    {
                        if (reservatie.Lunch.Handelaar.GebruikerId == int.Parse(User.FindFirst("gebruikersId")?.Value))
                        {
                            return Ok(reservatie);
                        }
                        return Unauthorized(new { error = "Deze reservatie behoort niet toe aan deze handelaar." });
                    }
                    else if (User.FindFirst("rol")?.Value == "admin")
                    {
                        return Ok(reservatie);
                    }
                }
                return BadRequest(new { error = "Deze reservatie bestaat niet." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als klant." });
        }

        // POST: api/Reservatie
        [HttpPost]
        public IActionResult Post([FromBody]ReservatieViewModel nieuweReservatie)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Klant klant = _klantRepository.GetById(int.Parse(User.FindFirst("gebruikersId")?.Value));
                        Lunch lunch = _lunchRespository.GetById(nieuweReservatie.LunchId);

                        if (klant != null && lunch != null)
                        {
                            if (nieuweReservatie.Datum >= lunch.BeginDatum && nieuweReservatie.Datum <= lunch.EindDatum)
                            {
                                Reservatie reservatie = new Reservatie
                                {
                                    Lunch = lunch,
                                    Aantal = nieuweReservatie.Aantal,
                                    Datum = nieuweReservatie.Datum,
                                    Opmerking = nieuweReservatie.Opmerking,
                                    Status = Status.InAfwachting
                                };

                                klant.Reservaties.Add(reservatie);
                                _reservatieRepository.SaveChanges();

                                //mail service
                                var message = new MailMessage();
                                message.From = new MailAddress(klant.Email);
                                message.To.Add(lunch.Handelaar.Email);
                                message.ReplyToList.Add(klant.Email);
                                message.Subject = "Er werd een reservatie geplaatst.";
                                message.Body = string.Format("Beste {3} \n\nEr werd een nieuwe reservatie geplaatst voor {0} op {1} om {2}.\n\nMet volgende opmerking: \n\n{4}\n\nGegevens van de klant: \nNaam: {5} {6} \nTelefoonnummer: {8}\nEmail: {7}\n\nU kan de reservatie goedkeuren op de website van Lunchers.\n\nMet vriedelijke groeten,\nHet Lunchers team ",
                                lunch.Naam,
                                reservatie.Datum.ToString("d", CultureInfo.CreateSpecificCulture("pt-BR")),
                                reservatie.Datum.ToString("t", CultureInfo.CreateSpecificCulture("es-ES")),
                                lunch.Handelaar.HandelsNaam,
                                reservatie.Opmerking,
                                klant.Voornaam,
                                klant.Achternaam,
                                klant.Email,
                                klant.Telefoonnummer);

                                //smpt server
                                var SmtpServer = new SmtpClient("smtp.gmail.com");
                                SmtpServer.Port = 587;
                                SmtpServer.Credentials = new System.Net.NetworkCredential("lunchersteam@gmail.com", "reallyStrongPwd123");
                                SmtpServer.EnableSsl = true;

                                //message sent
                                SmtpServer.Send(message);

                                return Ok(new { bericht = "De reservatie werd succesvol aangemaakt." });
                            }
                            return BadRequest(new { error = "De gekozen lunch is niet beschikbaar op de opgegeven datum." });
                        }
                        return BadRequest(new { error = "De opgegeven lunch of klant kon niet worden teruggevonden." });
                    }
                    catch
                    {
                        return BadRequest(new { error = "Er is iets fout gegaan tijdens het aanmaken van de reservatie." });
                    }
                }
                return BadRequest(new { error = ModelState });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als klant." });
        }

        // PUT: api/Reservatie/5
        [HttpPut]
        public IActionResult Put([FromBody]ReservatieEditViewModel aangepasteReservatie)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "handelaar")
            {
                if (ModelState.IsValid)
                {
                    Handelaar handelaar = _handelaarRepository.GetById(int.Parse(User.FindFirst("gebruikersId")?.Value));
                    Reservatie reservatie = _reservatieRepository.GetById(aangepasteReservatie.ReservatieId);
                    
                    if (reservatie.Lunch.Handelaar.GebruikerId == handelaar.GebruikerId)
                    {
                        reservatie.Status = aangepasteReservatie.Status;
                        _reservatieRepository.SaveChanges();

                        //mail service
                        var message = new MailMessage();
                        message.From = new MailAddress(handelaar.Email);
                        message.To.Add(reservatie.Klant.Email);
                        message.ReplyToList.Add(handelaar.Email);
                        message.Subject = string.Format("Uw reservatie werd {0}", reservatie.Status.ToString().ToLower());
                        message.Body = string.Format("Beste {0} \n\nUw reservatie voor {1} op {2} om {3} werd {4}.\n\nU kan de handelaar altijd contacteren via mail {5} of via het nummer {6}.\n\nU kan al uw reservaties altijd controleren op de website van Lunchers.\n\nMet vriedelijke groeten,\nHet Lunchers team ",
                        reservatie.Klant.Voornaam,
                        reservatie.Lunch.Naam,
                        reservatie.Datum.ToString("d", CultureInfo.CreateSpecificCulture("pt-BR")),
                        reservatie.Datum.ToString("t", CultureInfo.CreateSpecificCulture("es-ES")),
                        reservatie.Status.ToString().ToLower(),
                        handelaar.Email,
                        handelaar.Telefoonnummer);

                        //smpt server
                        var SmtpServer = new SmtpClient("smtp.gmail.com");
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("lunchersteam@gmail.com", "reallyStrongPwd123");
                        SmtpServer.EnableSsl = true;

                        //message sent
                        SmtpServer.Send(message);

                        return Ok(new { bericht = "De reservatie werd succesvol bijgewerkt." });
                    }
                    return BadRequest(new { error = "De aangemelde handelaar heeft geen toegang tot de opgegeven reservatie" });
                }
                return BadRequest(new { error = "De opgestuurde gegevens zijn onvolledig of incorrect." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als handelaar." });
        }
    }
}
