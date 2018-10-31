﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchers.Models;
using Lunchers.Models.Domain;

namespace Lunchers.Data
{
    public class DummyDataInitializer
    {

        private readonly ApplicationDbContext _dbContext;

        public DummyDataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                // ROLLEN BEGIN
                Rol rolAdmin = new Rol { Naam = "Admin" };
                Rol rolStandaard = new Rol { Naam = "Standaard" };
                Rol rolHandelaar = new Rol { Naam = "Handelaar" };

                var rollen = new List<Rol>
                {
                    rolAdmin, rolStandaard, rolHandelaar
                };
                // ROLLEN EINDE

                // GEBRUIKERS BEGIN
                Gebruiker gebruikerStandaardLennert = new Klant { Voornaam = "Lennert", Achternaam = "Bontinck", Email = "info@lennertbontinck.com", Telefoonnummer = "0491234514" };
                gebruikerStandaardLennert.Login = new Login { Gebruikersnaam = "lennert", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerStandaardLennert, Rol = rolStandaard };
                Gebruiker gebruikerStandaard1 = new Klant { Voornaam = "Kathi", Achternaam = "Bramblett", Email = "bramblett@me.com", Telefoonnummer = "0491234515" };
                gebruikerStandaard1.Login = new Login { Gebruikersnaam = "kathi", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerStandaard1, Rol = rolStandaard };
                Gebruiker gebruikerStandaard2 = new Klant { Voornaam = "Liza", Achternaam = "Imboden", Email = "liza@optonline.net", Telefoonnummer = "0491234515" };
                gebruikerStandaard2.Login = new Login { Gebruikersnaam = "liza", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerStandaard2, Rol = rolStandaard };
                Gebruiker gebruikerStandaard3 = new Klant { Voornaam = "Christine", Achternaam = "Heisler", Email = "christine@msn.com", Telefoonnummer = "0491234515" };
                gebruikerStandaard3.Login = new Login { Gebruikersnaam = "christine", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerStandaard3, Rol = rolStandaard };
                Gebruiker gebruikerStandaard4 = new Klant { Voornaam = "Jena", Achternaam = "Ocampo", Email = "jena@sbcglobal.net", Telefoonnummer = "0491234515" };
                gebruikerStandaard4.Login = new Login { Gebruikersnaam = "jena", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerStandaard4, Rol = rolStandaard };
                Gebruiker gebruikerStandaard5 = new Klant { Voornaam = "Jan", Achternaam = "Vermassen", Email = "jan@mac.com", Telefoonnummer = "0491234515" };
                gebruikerStandaard5.Login = new Login { Gebruikersnaam = "jan", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerStandaard5, Rol = rolStandaard };

                Gebruiker gebruikerAdmin1TeamGdpr = new Administrator { Voornaam = "Team", Achternaam = "GDPR", Email = "info@teamgdpr.be", Telefoonnummer = "0491234514" };
                gebruikerAdmin1TeamGdpr.Login = new Login { Gebruikersnaam = "teamGDPR", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerAdmin1TeamGdpr, Rol = rolAdmin };
                Gebruiker gebruikerAdmin2QarfaRenate = new Administrator { Voornaam = "Renate", Achternaam = "Coen", Email = "renate@qarfa.be", Telefoonnummer = "0494157077" };
                gebruikerAdmin2QarfaRenate.Login = new Login { Gebruikersnaam = "Renate", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerAdmin2QarfaRenate, Rol = rolAdmin };

                Gebruiker gebruikerHandelaar1Qarfa = new Handelaar { Naam = "Qarfa", Voornaam = "Renate", Achternaam = "Coen", Email = "info@qarfa.be", Telefoonnummer = "0494157077", Locatie = new Locatie { Straat = "Stationsstraat", Huisnummer = "13", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.970252, Longitude = 3.984861 }, Website = "http://www.qarfa.be/", PromotieRange = 10 };
                gebruikerHandelaar1Qarfa.Login = new Login { Gebruikersnaam = "qarfa", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerHandelaar1Qarfa, Rol = rolHandelaar };
                Gebruiker gebruikerHandelaar2BrasserieBlomme = new Handelaar { Naam = "Brasserie Blomme", Voornaam = "Ann", Achternaam = "Blomme", Email = "info@brasserieblomme.be", Telefoonnummer = "0475529592", Locatie = new Locatie { Straat = "Gentsesteenweg", Huisnummer = "100", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.938074, Longitude = 4.024402 }, Website = "http://www.brasserieblomme.be/", PromotieRange = 2 };
                gebruikerHandelaar2BrasserieBlomme.Login = new Login { Gebruikersnaam = "blomme", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerHandelaar2BrasserieBlomme, Rol = rolHandelaar };
                Gebruiker gebruikerHandelaar3Kelderman = new Handelaar { Naam = "Kelderman", Voornaam = "Dirk", Achternaam = "Kelderman", Email = "info@kelderman.be", Telefoonnummer = "053776125", Locatie = new Locatie { Straat = "Parklaan", Huisnummer = "4", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.892543, Longitude = 4.074539 }, Website = "http://www.visrestaurant-kelderman.be/", PromotieRange = 5 };
                gebruikerHandelaar3Kelderman.Login = new Login { Gebruikersnaam = "kelderman", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerHandelaar3Kelderman, Rol = rolHandelaar };
                Gebruiker gebruikerHandelaar4Zorba = new Handelaar { Naam = "Zorba Aalst", Voornaam = "Johan", Achternaam = "De Mulder", Email = "info@zorbaaalst.be", Telefoonnummer = "053776506", Locatie = new Locatie { Straat = "Houtmarkt", Huisnummer = "3", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.934408, Longitude = 4.043971 }, Website = "https://www.facebook.com/pages/Zorba/140775739321413", PromotieRange = 0 };
                gebruikerHandelaar4Zorba.Login = new Login { Gebruikersnaam = "zorbaaalst", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerHandelaar4Zorba, Rol = rolHandelaar };
                Gebruiker gebruikerHandelaar5Dion = new Handelaar { Naam = "Restaurant Dion", Voornaam = "John", Achternaam = "Dion", Email = "info@Dion.be", Telefoonnummer = "053787815", Locatie = new Locatie { Straat = "Oude Gentbaan", Huisnummer = "51", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.940219, Longitude = 4.017006 }, Website = "http://www.restaurantdion.be/", PromotieRange = 10 };
                gebruikerHandelaar5Dion.Login = new Login { Gebruikersnaam = "dion", Wachtwoord = "Wachtwoord123", gebruiker = gebruikerHandelaar5Dion, Rol = rolHandelaar };



                var gebruikers = new List<Gebruiker>
                {
                    gebruikerStandaardLennert, gebruikerStandaard1, gebruikerStandaard2, gebruikerStandaard3, gebruikerStandaard4, gebruikerStandaard5,
                    gebruikerAdmin1TeamGdpr, gebruikerAdmin2QarfaRenate,
                    gebruikerHandelaar1Qarfa, gebruikerHandelaar2BrasserieBlomme, gebruikerHandelaar3Kelderman, gebruikerHandelaar4Zorba, gebruikerHandelaar5Dion
                };

                // GEBRUIKERS EINDE

                //INGREDIENT
                IngredientInLunch ingredient1 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Paprika" } };
                IngredientInLunch ingredient2 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Kip" } };
                IngredientInLunch ingredient3 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Boontjes" } };
                IngredientInLunch ingredient4 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Melk producten" } };
                IngredientInLunch ingredient5 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Pasta" } };
                IngredientInLunch ingredient6 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Tomaat" } };
                IngredientInLunch ingredient7 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Brocoli" } };
                IngredientInLunch ingredient8 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Noten" } };
                IngredientInLunch ingredient9 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Rundsvlees" } };
                IngredientInLunch ingredient10 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Varkensvlees" } };
                IngredientInLunch ingredient11 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Vis" } };
                IngredientInLunch ingredient12 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Ui" } };

                var ingredientenVlees = new List<IngredientInLunch>{
                    ingredient9, ingredient10, ingredient12, ingredient6, ingredient9, ingredient10, ingredient12
                };

                var ingredientenVegan = new List<IngredientInLunch>{
                    ingredient1, ingredient3, ingredient6, ingredient4, ingredient6, ingredient7, ingredient8
                };

                var ingredientenPasta = new List<IngredientInLunch>{
                    ingredient1, ingredient2, ingredient5, ingredient6, ingredient8
                };

                var ingredientenVis = new List<IngredientInLunch>{
                    ingredient11, ingredient7
                };
                //INGREDIENT EINDE

                //TAGS
                string RodeKleur = "FF6A6A";
                string GroeneKleur = "82CA9D ";
                string Gelekleur = "FFF79A ";


                TagInLunch tag1 = new TagInLunch { Tag = new Tag { Naam = "hambuger", Kleur = RodeKleur } };
                TagInLunch tag2 = new TagInLunch { Tag = new Tag { Naam = "varkensvlees", Kleur = RodeKleur } };
                TagInLunch tag3 = new TagInLunch { Tag = new Tag { Naam = "frietjes", Kleur = Gelekleur } };
                TagInLunch tag4 = new TagInLunch { Tag = new Tag { Naam = "sla", Kleur = GroeneKleur } };
                TagInLunch tag5 = new TagInLunch { Tag = new Tag { Naam = "gezond", Kleur = GroeneKleur } };
                TagInLunch tag6 = new TagInLunch { Tag = new Tag { Naam = "vegetarisch", Kleur = GroeneKleur } };
                TagInLunch tag7 = new TagInLunch { Tag = new Tag { Naam = "vis", Kleur = RodeKleur } };
                TagInLunch tag8 = new TagInLunch { Tag = new Tag { Naam = "zalm", Kleur = RodeKleur } };
                TagInLunch tag9 = new TagInLunch { Tag = new Tag { Naam = "italiaans", Kleur = RodeKleur } };
                TagInLunch tag10 = new TagInLunch { Tag = new Tag { Naam = "dieet", Kleur = GroeneKleur } };
                TagInLunch tag11 = new TagInLunch { Tag = new Tag { Naam = "kip", Kleur = RodeKleur } };
                TagInLunch tag12 = new TagInLunch { Tag = new Tag { Naam = "kaas", Kleur = Gelekleur } };
                TagInLunch tag13 = new TagInLunch { Tag = new Tag { Naam = "augurk", Kleur = RodeKleur } };
                TagInLunch tag14 = new TagInLunch { Tag = new Tag { Naam = "champignonsaus", Kleur = Gelekleur } };
                TagInLunch tag15 = new TagInLunch { Tag = new Tag { Naam = "Rosbief", Kleur = RodeKleur } };
                TagInLunch tag16 = new TagInLunch { Tag = new Tag { Naam = "Roomsaus", Kleur = Gelekleur } };


                var tagsHamburger = new List<TagInLunch>{
                    tag1,tag2, tag12, tag13
                };

                var tagsVleesFriet = new List<TagInLunch>{
                    tag2,tag3,tag14
                };

                var tagsVegan = new List<TagInLunch>{
                    tag4, tag5, tag6, tag10
                };

                var tagsPasta = new List<TagInLunch>{
                    tag4, tag10, tag9, tag11, tag15
                };

                var tagsVis = new List<TagInLunch>{
                    tag7, tag8, tag10
                };

                var tagsZalm = new List<TagInLunch>{
                    tag7, tag8, tag10, tag16
                };
                //TAGS EINDE


                //LUNCHES BEGIN
                //nog Afbeeldingen
                Lunch lunchStandaardHamburger = new Lunch { Naam = "American hamburger", Prijs = 10.00, Ingredienten = ingredientenVlees, Beschrijving = "Een echte American burger met alles wat er bij hoort zoals bacon, cheddar kaas en augurkjes.", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Tags = tagsHamburger };
                Lunch lunchStandaardPasta = new Lunch { Naam = "Italiaanse pasta rosbief", Prijs = 20, Ingredienten = ingredientenPasta, Beschrijving = "Rosbief is een klassieker, maar waarom niet eens combineren met pasta en lekkere Italiaanse producten?", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Tags = tagsPasta };
                Lunch lunchStandaardVis = new Lunch { Naam = "Visschotel", Prijs = 15.50, Ingredienten = ingredientenVis, Beschrijving = "Gegratineerde visschotel met duo van puree op grootmoeders wijze.", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Tags = tagsVis };
                Lunch lunchStandaardVegan = new Lunch { Naam = "Vegan salad", Prijs = 25.00, Ingredienten = ingredientenVegan, Beschrijving = "Een lekker frisse en bovenal gezonde vegan salade.", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Tags = tagsVegan };
                Lunch lunchStandaardZalm = new Lunch { Naam = "Zalm met venkel", Prijs = 50.00, Ingredienten = ingredientenVis, Beschrijving = "Zalm vergezelf met venkel en heerlijke roomsaus op oma's wijze", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Tags = tagsZalm };
                Lunch lunchStandaardBiefstuk = new Lunch { Naam = "Biefstuk met frietjes", Prijs = 22.50, Ingredienten = ingredientenVlees, Beschrijving = "Wat smaakt er beter dan een lekkere steak, zeker wanneer die nét goed gebakken is? Bleu, saignant, à point of bien cuit: u zegt het, wij bakken het.", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Tags = tagsVleesFriet };

                Lunch LunchUitzonderingVervallen = new Lunch { Naam = "Schotse Hamburger", Prijs = 12.50, Ingredienten = ingredientenVlees, Beschrijving = "Een ", BeginDatum = new DateTime(2017, 10, 30), EindDatum = new DateTime(2017, 12, 30), Tags = tagsHamburger };

                ((Handelaar)gebruikerHandelaar1Qarfa).Lunches.Add(lunchStandaardHamburger);
                ((Handelaar)gebruikerHandelaar1Qarfa).Lunches.Add(LunchUitzonderingVervallen);
                ((Handelaar)gebruikerHandelaar1Qarfa).Lunches.Add(lunchStandaardBiefstuk);
                ((Handelaar)gebruikerHandelaar2BrasserieBlomme).Lunches.Add(lunchStandaardPasta);
                ((Handelaar)gebruikerHandelaar3Kelderman).Lunches.Add(lunchStandaardVis);
                ((Handelaar)gebruikerHandelaar4Zorba).Lunches.Add(lunchStandaardVegan);
                ((Handelaar)gebruikerHandelaar5Dion).Lunches.Add(lunchStandaardZalm);
                //LUNCHES EINDE

                //AFBEELDINGEN
                lunchStandaardHamburger.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch1/1.jpg" });
                lunchStandaardHamburger.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch1/2.jpg" });
                lunchStandaardHamburger.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch1/3.jpg" });

                lunchStandaardPasta.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch2/1.jpg" });
                lunchStandaardPasta.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch2/2.jpg" });
                lunchStandaardPasta.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch2/3.jpg" });

                lunchStandaardVis.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch3/1.jpg" });
                lunchStandaardVis.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch3/2.jpg" });

                lunchStandaardVegan.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch4/1.jpg" });
                lunchStandaardVegan.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch4/2.jpg" });
                lunchStandaardVegan.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch4/3.jpg" });

                lunchStandaardZalm.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch5/1.jpg" });
                lunchStandaardZalm.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch5/2.jpg" });
                lunchStandaardZalm.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch5/3.jpg" });

                lunchStandaardBiefstuk.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch6/1.jpg" });
                lunchStandaardBiefstuk.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch6/2.jpg" });
                lunchStandaardBiefstuk.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch6/3.jpg" });

                LunchUitzonderingVervallen.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch7/1.jpg" });
                LunchUitzonderingVervallen.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch7/2.jpg" });
                //AFBEELDINGEN EINDE

                //RESERVATIES -> user lennert
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 5, Datum = new DateTime(2018, 10, 26), Lunch = lunchStandaardHamburger });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 3, Datum = new DateTime(2018, 10, 25), Lunch = lunchStandaardPasta });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 2, Datum = new DateTime(2018, 10, 28), Lunch = lunchStandaardVis });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 1, Datum = new DateTime(2018, 10, 31), Lunch = lunchStandaardVegan });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 9, Datum = new DateTime(2018, 10, 30), Lunch = lunchStandaardZalm });
                //RESERVATIES EINDE

                //FAVORIETEN -> user lennert
                ((Klant)gebruikerStandaardLennert).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 26), Lunch = lunchStandaardHamburger });
                ((Klant)gebruikerStandaardLennert).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 26), Lunch = LunchUitzonderingVervallen });
                ((Klant)gebruikerStandaardLennert).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 28), Lunch = lunchStandaardVis });
                ((Klant)gebruikerStandaardLennert).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 30), Lunch = lunchStandaardVegan });
                //FAVORIETEN EINDE

                // SAVE CHANGES
                _dbContext.Rollen.AddRange(rollen);
                _dbContext.Gebruikers.AddRange(gebruikers);
                _dbContext.SaveChanges();
            }
        }
    }
}