using Countries.Infra.Data.DataContext;
using Countries.Models.Models;
using System.Linq;

namespace Countries.UnitTests.ResourcesDatabase
{
    public class DbInitializerTest
    {
        public static void Initialize(CountriesDbContext context)
        {
            if (context.Countries.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(CountriesDbContext context)
        {
            // Create Countries 
            var countries = new Country[]
            {
                new Country { Name = "Francia", Alpha_2 = "Fr", Alpha_3 = "Fra", NumericCode = "250" },
                new Country { Name = "Alemania", Alpha_2 = "De", Alpha_3 = "Deu", NumericCode = "276" },
                new Country { Name = "Guatemala", Alpha_2 = "Gt", Alpha_3 = "Gtm", NumericCode = "320" }
            };
            
            context.Countries.AddRange(countries);
            context.SaveChanges();
        }
    }
}
