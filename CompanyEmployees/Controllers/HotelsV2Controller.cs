using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/hotelsv2")]
    [ApiController]
    public class HotelsV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public HotelsV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _repository.Hotel.GetAllHotelsAsync(trackChanges: false);
            return Ok(hotels);
        }
    }
}
