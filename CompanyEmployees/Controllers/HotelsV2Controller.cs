using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
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
