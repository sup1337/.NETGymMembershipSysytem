using System;
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
    public class IndexModel : PageModel
    {
        private readonly BestDzsimPro.Data.ApplicationDbContext _context;

        public IndexModel(BestDzsimPro.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Trainer> Trainer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Trainer = await _context.Trainers.ToListAsync();
        }
    }
}
