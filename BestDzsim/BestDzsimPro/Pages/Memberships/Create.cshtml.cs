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

namespace BestDzsimPro.Pages.Memberships
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
        ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FirstName");
        ViewData["PlanId"] = new SelectList(_context.Plans, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;

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
            Membership.UpdateAt = DateTime.Now;
            Membership.CreatedAt = DateTime.Now;
            Membership.CreatedBy = loggedInUser?.UserName;
            _context.Memberships.Add(Membership);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
