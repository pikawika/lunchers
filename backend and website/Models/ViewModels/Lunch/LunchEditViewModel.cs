﻿using Lunchers.Models.Domain;
using Lunchers.Models.ViewModels.Ingredient;
using Lunchers.Models.ViewModels.Tag;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.ViewModels.Lunch
{
    public class LunchEditViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [StringLength(60, MinimumLength = 3)]
        public string Naam { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Range(1.00, 999.99, ErrorMessage = "Prijs moet tussen {1} en {2} liggen.")]
        public string Prijs { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [StringLength(2500, MinimumLength = 10)]
        public string Beschrijving { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime BeginDatum { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        public List<IngredientViewModel> Ingredienten { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        public List<TagViewModel> Tags { get; set; }

        public IFormCollection RawData { get; set; }

        [DataType(DataType.Upload)]
        public IFormCollection Afbeeldingen { set; get; }
    }
}
