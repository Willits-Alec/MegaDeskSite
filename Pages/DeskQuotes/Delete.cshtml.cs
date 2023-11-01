using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskSite.Data;
using MegaDeskSite.Models;

namespace MegaDeskSite.Pages.DeskQuotes
{
    public class DeleteModel : PageModel
    {
        private readonly MegaDeskSite.Data.MegaDeskSiteContext _context;

        public DeleteModel(MegaDeskSite.Data.MegaDeskSiteContext context)
        {
            _context = context;
        }

        [BindProperty]
      public DeskQuote DeskQuote { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }

            var deskquote = await _context.DeskQuote.FirstOrDefaultAsync(m => m.Id == id);

            if (deskquote == null)
            {
                return NotFound();
            }
            else 
            {
                DeskQuote = deskquote;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }
            var deskquote = await _context.DeskQuote.FindAsync(id);

            if (deskquote != null)
            {
                DeskQuote = deskquote;
                _context.DeskQuote.Remove(DeskQuote);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
