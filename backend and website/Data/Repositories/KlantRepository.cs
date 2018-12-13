﻿using Lunchers.Models.Domain;
using Lunchers.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Data.Repositories
{
    public class KlantRepository : IKlantRepository
    {
        private readonly DbSet<Klant> _klanten;
        private readonly ApplicationDbContext _context;

        public KlantRepository(ApplicationDbContext context)
        {
            _context = context;
            _klanten = context.Klanten;
        }

        public Klant GetById(int customerId)
        {
            return _klanten.Include(k => k.Reservaties).Include(k => k.Favorieten).SingleOrDefault(k => k.GebruikerId == customerId);
        }

        public void AddAllergy(int gebruikersId, string allergy)
        {
            Klant klant = _klanten.Include(k => k.Allergies).SingleOrDefault(k => k.GebruikerId == gebruikersId);
            if (klant != null)
            {
                klant.Allergies.Add(new Allergy() { AllergyNaam = allergy });
                SaveChanges();
            }
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
