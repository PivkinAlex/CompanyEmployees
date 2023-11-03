using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet("{id}", Name = "GetLodgerForHotel")]
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
        [HttpPost]
        public IActionResult CreateLodgerForHotel(Guid hotelId, [FromBody] LodgerForCreationDto lodger)
        {
            if (lodger == null)
            {
                _logger.LogError("LodgerForCreationDto object sent from client is null.");
                return BadRequest("LodgerForCreationDto object is null");
            }
            var hotel = _repository.Hotel.GetHotel(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerEntity = _mapper.Map<Lodger>(lodger);
            _repository.Lodger.CreateLodgerForHotel(hotelId, lodgerEntity);
            _repository.Save();
            var lodgerToReturn = _mapper.Map<LodgerDto>(lodgerEntity);
            return CreatedAtRoute("GetLodgerForHotel", new
            {
                hotelId,
                id = lodgerToReturn.Id
            }, lodgerToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLodgerForHotel(Guid hotelId, Guid id)
        {
            var hotel = _repository.Hotel.GetHotel(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerForHotel = _repository.Lodger.GetLodger(hotelId, id, trackChanges: false);
            if (lodgerForHotel == null)
            {
                _logger.LogInfo($"Lodger with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Lodger.DeleteLodger(lodgerForHotel);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateLodgerForHotel(Guid hotelId, Guid id, [FromBody] LodgerForUpdateDto lodger)
        {
            if (lodger == null)
            {
                _logger.LogError("LodgerForUpdateDto object sent from client is null.");
                return BadRequest("LodgerForUpdateDto object is null");
            }
            var hotel = _repository.Hotel.GetHotel(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerEntity = _repository.Lodger.GetLodger(hotelId, id, trackChanges: true);
            if (lodgerEntity == null)
            {
                _logger.LogInfo($"Lodger with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(lodger, lodgerEntity);
            _repository.Save();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateLodgerForHotel(Guid hotelId, Guid id, [FromBody] JsonPatchDocument<LodgerForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var hotel = _repository.Hotel.GetHotel(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerEntity = _repository.Lodger.GetLodger(hotelId, id, trackChanges: true);
            if (lodgerEntity == null)
            {
                _logger.LogInfo($"Lodger with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerToPatch = _mapper.Map<LodgerForUpdateDto>(lodgerEntity);
            patchDoc.ApplyTo(lodgerToPatch);
            _mapper.Map(lodgerToPatch, lodgerEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
