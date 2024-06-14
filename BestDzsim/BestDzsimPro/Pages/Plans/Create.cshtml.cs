using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BestDzsimPro.Data;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace BestDzsimPro.Pages.Plans
{
    public class CreateModel : PageModel
    {
        private readonly BestDzsimPro.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(BestDzsimPro.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Plan Plan { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return Page();
            }
            Plan.UpdateAt = DateTime.Now;
            Plan.CreatedAt = DateTime.Now;
            Plan.CreatedBy = loggedInUser?.UserName;

            _context.Plans.Add(Plan);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

            _context.Plans.Add(Plan);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
