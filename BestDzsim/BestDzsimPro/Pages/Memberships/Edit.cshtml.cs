﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BestDzsimPro.Data;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace BestDzsimPro.Pages.Memberships
{
    public class EditModel : PageModel
    {
        private readonly BestDzsimPro.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public EditModel(BestDzsimPro.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membership =  await _context.Memberships.FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }
            Membership = membership;
           ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Address");
           ViewData["PlanId"] = new SelectList(_context.Plans, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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
            _context.Attach(Membership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembershipExists(Membership.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MembershipExists(int id)
        {
            return _context.Memberships.Any(e => e.Id == id);
        }
    }
}
