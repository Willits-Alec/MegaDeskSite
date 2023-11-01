using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MegaDeskSite.Models;

namespace MegaDeskSite.Data
{
    public class MegaDeskSiteContext : DbContext
    {
        public MegaDeskSiteContext (DbContextOptions<MegaDeskSiteContext> options)
            : base(options)
        {
        }

        public DbSet<MegaDeskSite.Models.DeskQuote> DeskQuote { get; set; } = default!;
    }
}
