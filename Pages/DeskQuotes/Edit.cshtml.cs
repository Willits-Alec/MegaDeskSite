using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaDeskSite.Data;
using MegaDeskSite.Models;

namespace MegaDeskSite.Pages.DeskQuotes
{
    public class EditModel : PageModel
    {
        public const decimal BASEDESKPRICE = 200;
        public const decimal EXTRAAREAPRICE = 1;
        public const decimal DRAWERPRICE = 50;

        private readonly MegaDeskSite.Data.MegaDeskSiteContext _context;

        public EditModel(MegaDeskSite.Data.MegaDeskSiteContext context)
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

            var deskquote =  await _context.DeskQuote.FirstOrDefaultAsync(m => m.Id == id);
            if (deskquote == null)
            {
                return NotFound();
            }
            DeskQuote = deskquote;
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

            DeskQuote.Price = GetTotalPrice();

            _context.Attach(DeskQuote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeskQuoteExists(DeskQuote.Id))
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

        private bool DeskQuoteExists(int id)
        {
          return (_context.DeskQuote?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public int GetArea()
        {
            int area = DeskQuote.Depth * DeskQuote.Width;

            return area;
        }

        public decimal GetDrawersPrice()
        {
            decimal drawerTotalPrice = DRAWERPRICE * DeskQuote.NumDrawers;
            return drawerTotalPrice;
        }

        public decimal GetMaterialPrice()
        {
            decimal materialPrice = 0;
            // switch case for all the materials. Set the materialPrice to the correct number in each case.
            switch (DeskQuote.Material)
            {
                case "laminate":
                    materialPrice = 100;
                    break;
                case "Oak":
                    materialPrice = 200;
                    break;
                case "Rosewood":
                    materialPrice = 300;
                    break;
                case "Veneer":
                    materialPrice = 125;
                    break;
                default:  //this is for pine
                    materialPrice = 50;
                    break;
            }
            return materialPrice;
        }

        public string GetAreaRange()
        {
            string size = "";
            if (GetArea() < 1000)
            {
                size = "small";

            }
            else if (GetArea() >= 1000 && GetArea() <= 2000)
            {
                size = "mid";
            }
            else
            {
                size = "large";
            }
            return size;
        }

        public decimal GetRushPrice()
        {
            decimal rushPrice = 0;
            string size = GetAreaRange();
            switch (DeskQuote.RushOrderChoice)
            {
                case (3):
                    if (size == "small")
                    {
                        rushPrice = 60;
                    }
                    else if (size == "mid")
                    {
                        rushPrice = 70;
                    }
                    else
                    {
                        rushPrice = 80;
                    }
                    break;
                case (5):
                    if (size == "small")
                    {
                        rushPrice = 40;
                    }
                    else if (size == "mid")
                    {
                        rushPrice = 50;
                    }
                    else
                    {
                        rushPrice = 60;
                    }
                    break;
                case (7):
                    if (size == "small")
                    {
                        rushPrice = 30;
                    }
                    else if (size == "mid")
                    {
                        rushPrice = 35;
                    }
                    else
                    {
                        rushPrice = 40;
                    }
                    break;
                default:
                    rushPrice = 0;
                    break;

            }
            return rushPrice;
        }

        public decimal GetDeskPrice()
        {
            decimal deckPrice = BASEDESKPRICE + GetArea() + GetDrawersPrice() + GetMaterialPrice();
            return deckPrice;
        }

        public decimal GetTotalPrice()
        {
            //math equation like so that gets the total price:
            decimal totalPrice = GetDeskPrice() + GetRushPrice();

            return totalPrice;
        }
    }
}
