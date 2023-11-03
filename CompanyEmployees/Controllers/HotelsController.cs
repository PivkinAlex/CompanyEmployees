using AutoMapper;
using CompanyEmployees.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
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
        public IActionResult GetHotels()
        {
            var hotels = _repository.Hotel.GetAllHotels(trackChanges: false);
            var hotelDto = _mapper.Map<IEnumerable<HotelDto>>(hotels);
            return Ok(hotelDto);
        }
        [HttpGet("{id}", Name = "HotelById")]
        public IActionResult GetHotel(Guid id)
        {
            var hotel = _repository.Hotel.GetHotel(id, trackChanges: false);
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
        public IActionResult CreateHotel([FromBody] HotelForCreatonDto hotel)
        {
            if (hotel == null)
            {
                _logger.LogError("HotelForCreationDto object sent from client is null.");
                return BadRequest("HotelForCreationDto object is null");
            }
            var hotelEntity = _mapper.Map<Hotel>(hotel);
            _repository.Hotel.CreateHotel(hotelEntity);
            _repository.Save();
            var hotelToReturn = _mapper.Map<HotelDto>(hotelEntity);
            return CreatedAtRoute("HotelById", new { id = hotelToReturn.Id }, hotelToReturn);
        }
        [HttpGet("collection/({ids})", Name = "HotelCollection")]
        public IActionResult GetHotelCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var hotelEntities = _repository.Hotel.GetByIds(ids, trackChanges: false);
            if (ids.Count() != hotelEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var hotelsToReturn = _mapper.Map<IEnumerable<HotelDto>>(hotelEntities);
            return Ok(hotelsToReturn);
        }
        [HttpPost("collection")]
        public IActionResult CreateHotelCollection([FromBody] IEnumerable<HotelForCreatonDto> hotelCollection)
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
            _repository.Save();
            var hotelCollectionToReturn = _mapper.Map<IEnumerable<HotelDto>>(hotelEntities);
            var ids = string.Join(",", hotelCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("HotelCollection", new { ids }, hotelCollectionToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(Guid id)
        {
            var hotel = _repository.Hotel.GetHotel(id, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Hotel.DeleteHotel(hotel);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateHotel(Guid id, [FromBody] HotelForUpdateDto hotel)
        {
            if (hotel == null)
            {
                _logger.LogError("HotelForUpdateDto object sent from client is null.");
                return BadRequest("HotelForUpdateDto object is null");
            }
            var hotelEntity = _repository.Hotel.GetHotel(id, trackChanges: true);
            if (hotelEntity == null)
            {
                _logger.LogInfo($"Hotel with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(hotel, hotelEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
