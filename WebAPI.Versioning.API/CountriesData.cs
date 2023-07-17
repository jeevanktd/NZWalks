using WebAPI.Versioning.API.Model.Domain;

namespace WebAPI.Versioning.API
{
    public class CountriesData
    {
        public static List<Country> Get()
        {
            var countries = new[]
            {
                new {Id = 1, Name = "United States"},
                new {Id = 2, Name = "Germany"},
                new {Id = 3, Name = "Brazil"},
                new {Id = 4, Name = "China"},
                new {Id = 5, Name = "India"},
                new {Id = 6, Name = "South Africa"},
                new {Id = 7, Name = "Mexicco"},
                new {Id = 8, Name = "Japan"},
                new {Id = 9, Name = "Russia"},
                new {Id = 10, Name = "Newziland"},
            };

            return countries.Select(c => new Country { Id = c.Id, Name = c.Name }).ToList();

        }
    }
}
