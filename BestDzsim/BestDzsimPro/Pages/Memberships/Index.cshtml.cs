using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BestDzsimPro.Data;
using Entities;

namespace BestDzsimPro.Pages.Memberships
{
    public class IndexModel : PageModel
    {
        private readonly BestDzsimPro.Data.ApplicationDbContext _context;

        public IndexModel(BestDzsimPro.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Membership> Membership { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Membership = await _context.Memberships
                .Include(m => m.Member)
                .Where(m => m.Member.FirstName == "Simon")
                .Include(m => m.Plan).ToListAsync();
        }
    }
}
