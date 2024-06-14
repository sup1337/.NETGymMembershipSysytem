﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BestDzsimPro.Data;
using Entities;

namespace BestDzsimPro.Pages.Trainers
{
    public class DetailsModel : PageModel
    {
        private readonly BestDzsimPro.Data.ApplicationDbContext _context;

        public DetailsModel(BestDzsimPro.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Trainer Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers.FirstOrDefaultAsync(m => m.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }
            else
            {
                Trainer = trainer;
            }
            return Page();
        }
    }
}