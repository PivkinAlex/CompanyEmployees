using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/hotels/{hotelId}/lodgers")]
    [ApiController]
    public class LodgersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public LodgersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetLodgersForHotel(Guid hotelId)
        {
            var hotel = _repository.Hotel.GetHotel(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgersFromDb = _repository.Lodger.GetLodgers(hotelId, trackChanges: false);
            var lodgerDto = _mapper.Map<IEnumerable<LodgerDto>>(lodgersFromDb);
            return Ok(lodgerDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetLodgerForHotel(Guid hotelId, Guid id)
        {
            var hotel = _repository.Hotel.GetHotel(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerDb = _repository.Lodger.GetLodger(hotelId, id, trackChanges: false);
            if (lodgerDb == null)
            {
                _logger.LogInfo($"Lodger with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var lodger = _mapper.Map<EmployeeDto>(lodgerDb);
            return Ok(lodger);
        }
    }
}
