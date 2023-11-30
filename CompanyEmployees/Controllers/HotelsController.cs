using AutoMapper;
using CompanyEmployees.ActionFilters;
using CompanyEmployees.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public HotelsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _repository.Hotel.GetAllHotelsAsync(trackChanges: false);
            var hotelDto = _mapper.Map<IEnumerable<HotelDto>>(hotels);
            return Ok(hotelDto);
        }
        [HttpGet("{id}", Name = "HotelById")]
        public async Task<IActionResult> GetHotel(Guid id)
        {
            var hotel = await _repository.Hotel.GetHotelAsync(id, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var hotelDto = _mapper.Map<HotelDto>(hotel);
                return Ok(hotelDto);
            }
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateHotel([FromBody] HotelForCreatonDto hotel)
        {
            var hotelEntity = _mapper.Map<Hotel>(hotel);
            _repository.Hotel.CreateHotel(hotelEntity);
            await _repository.SaveAsync();
            var hotelToReturn = _mapper.Map<HotelDto>(hotelEntity);
            return CreatedAtRoute("HotelById", new { id = hotelToReturn.Id }, hotelToReturn);
        }
        [HttpGet("collection/({ids})", Name = "HotelCollection")]
        public async Task<IActionResult> GetHotelCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var hotelEntities = await _repository.Hotel.GetByIdsAsync(ids, trackChanges: false);
            if (ids.Count() != hotelEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var hotelsToReturn = _mapper.Map<IEnumerable<HotelDto>>(hotelEntities);
            return Ok(hotelsToReturn);
        }
        [HttpPost("collection")]
        public async Task<IActionResult> CreateHotelCollection([FromBody] IEnumerable<HotelForCreatonDto> hotelCollection)
        {
            if (hotelCollection == null)
            {
                _logger.LogError("Hotel collection sent from client is null.");
                return BadRequest("Hotel collection is null");
            }
            var hotelEntities = _mapper.Map<IEnumerable<Hotel>>(hotelCollection);
            foreach (var hotel in hotelEntities)
            {
                _repository.Hotel.CreateHotel(hotel);
            }
            await _repository.SaveAsync();
            var hotelCollectionToReturn = _mapper.Map<IEnumerable<HotelDto>>(hotelEntities);
            var ids = string.Join(",", hotelCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("HotelCollection", new { ids }, hotelCollectionToReturn);
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateHotelExistsAttribute))]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            var hotel = HttpContext.Items["hotel"] as Hotel;
            _repository.Hotel.DeleteHotel(hotel);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateHotelExistsAttribute))]
        public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] HotelForUpdateDto hotel)
        {
            var hotelEntity = HttpContext.Items["hotel"] as Hotel;
            _mapper.Map(hotel, hotelEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpOptions]
        public IActionResult GetHotelsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
    }
}
