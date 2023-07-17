using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Versioning.API.Model.DTO;

namespace WebAPI.Versioning.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CountriesController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        // https://localhost:7247/api/v1/Countries
        public IActionResult GetV1()
        {
            var countries = CountriesData.Get();

            var response = new List<CountryDto>();
            foreach (var country in countries)
            {
                response.Add(new CountryDto 
                { Id = country.Id,
                Name = country.Name });
            }

            return Ok(response);
        }

        //https://localhost:7247/api/Countries?api-version=2.0  => Remove [Route v{version:apiVersion} tag
        // https://localhost:7247/api/v2/Countries
        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult GetV2()
        {
            var countries = CountriesData.Get();

            var response = new List<CountryDtoV2>();
            foreach (var country in countries)
            {
                response.Add(new CountryDtoV2   
                {
                    Id = country.Id,
                    CountryName = country.Name
                });
            }

            return Ok(response);
        }
    }
}
