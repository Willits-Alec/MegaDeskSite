using Microsoft.EntityFrameworkCore;
using MegaDeskSite.Data;

namespace MegaDeskSite.Models
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MegaDeskSiteContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MegaDeskSiteContext>>()))
            {
                if (context == null || context.DeskQuote == null)
                {
                    throw new ArgumentNullException("Null MegaDeskSiteContext");
                }

                // Look for any movies.
                if (context.DeskQuote.Any())
                {
                    return;   // DB has been seeded
                }

                context.DeskQuote.AddRange(
                    new DeskQuote
                    {

                        Date = DateTime.Parse("1989-2-12"),
                        CustomerName = "Bob Smith",
                        NumDrawers = 3,
                        Width = 30,
                        Depth = 30,
                        Material = "laminate",
                        RushOrderChoice = 7,
                        Price = 1090M

                    },
                    new DeskQuote
                    {
                        Date = DateTime.Parse("1989-9-12"),
                        CustomerName = "Mike Stevens",
                        NumDrawers = 4,
                        Width = 25,
                        Depth = 30,
                        Material = "pine",
                        RushOrderChoice = 3,
                        Price = 1090M
                    },
                    new DeskQuote
                    {
                        Date = DateTime.Parse("2002-2-12"),
                        CustomerName = "Mike Jones",
                        NumDrawers = 4,
                        Width = 25,
                        Depth = 25,
                        Material = "veneer",
                        RushOrderChoice = 14,
                        Price = 1090M
                    },
                    new DeskQuote
                    {
                        Date = DateTime.Parse("2023-12-12"),
                        CustomerName = "Sally Winters",
                        NumDrawers = 7,
                        Width = 50,
                        Depth = 45,
                        Material = "Oak",
                        RushOrderChoice = 3,
                        Price = 1090M
                    }

                );
                context.SaveChanges();


            }

        }



    }
}
