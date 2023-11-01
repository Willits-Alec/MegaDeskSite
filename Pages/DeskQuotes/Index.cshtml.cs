using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskSite.Data;
using MegaDeskSite.Models;
using Microsoft.Data.SqlClient;

namespace MegaDeskSite.Pages.DeskQuotes
{
    public class IndexModel : PageModel
    {

        private readonly MegaDeskSite.Data.MegaDeskSiteContext _context;

        public IndexModel(MegaDeskSite.Data.MegaDeskSiteContext context)
        {
            _context = context;
        }

        public IList<DeskQuote> DeskQuote { get;set; } = default!;

        
       [BindProperty(SupportsGet = true)]
        public string? SearchName { get; set; }

        public async Task OnGetAsync(string sortBy)
        {

            var quotes = from e in _context.DeskQuote
                          select e;

            if (!string.IsNullOrEmpty(SearchName))
            {
                quotes = quotes.Where(e => e.CustomerName.ToLower() != null && e.CustomerName.Contains(SearchName.ToLower()));
    
            }

            if (sortBy == "descDate")
            {
                quotes = quotes.OrderByDescending(e => e.Date);
            }
            else if (sortBy == "ascName")
            {
                quotes = quotes.OrderBy(e => e.CustomerName);
            }
            else
            {
                quotes = quotes.OrderBy(e => e.Date);
            }

            DeskQuote = await quotes.ToListAsync();
        }
    }
}
