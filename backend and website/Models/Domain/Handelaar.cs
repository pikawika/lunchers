﻿using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models
{
    public class Handelaar : Gebruiker
    {
        public string Naam { get; set; }
        public Locatie Locatie { get; set; }
        public string Website { get; set; }
        public List<Lunch> Lunches { get; set; }
        public int PromotieRange { get; set; }
    }
}
