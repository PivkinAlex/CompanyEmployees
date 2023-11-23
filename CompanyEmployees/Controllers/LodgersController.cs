using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async Task<IActionResult> GetLodgersForHotel(Guid hotelId, [FromQuery] LodgerParameters lodgerParameters)
        {
            var hotel = await _repository.Hotel.GetHotelAsync(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgersFromDb = await _repository.Lodger.GetLodgersAsync(hotelId, lodgerParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(lodgersFromDb.MetaData));
            var lodgerDto = _mapper.Map<IEnumerable<LodgerDto>>(lodgersFromDb);
            return Ok(lodgerDto);
        }
        [HttpGet("{id}", Name = "GetLodgerForHotel")]
        public async Task<IActionResult> GetLodgerForHotel(Guid hotelId, Guid id)
        {
            var hotel = await _repository.Hotel.GetHotelAsync(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerDb = await _repository.Lodger.GetLodgerAsync(hotelId, id, trackChanges: false);
            if (lodgerDb == null)
            {
                _logger.LogInfo($"Lodger with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var lodger = _mapper.Map<EmployeeDto>(lodgerDb);
            return Ok(lodger);
        }
        [HttpPost]
        public async Task<IActionResult> CreateLodgerForHotel(Guid hotelId, [FromBody] LodgerForCreationDto lodger)
        {
            if (lodger == null)
            {
                _logger.LogError("LodgerForCreationDto object sent from client is null.");
                return BadRequest("LodgerForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var hotel = await _repository.Hotel.GetHotelAsync(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerEntity = _mapper.Map<Lodger>(lodger);
            _repository.Lodger.CreateLodgerForHotel(hotelId, lodgerEntity);
            await _repository.SaveAsync();
            var lodgerToReturn = _mapper.Map<LodgerDto>(lodgerEntity);
            return CreatedAtRoute("GetLodgerForHotel", new
            {
                hotelId,
                id = lodgerToReturn.Id
            }, lodgerToReturn);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLodgerForHotel(Guid hotelId, Guid id)
        {
            var hotel = await _repository.Hotel.GetHotelAsync(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerForHotel = await _repository.Lodger.GetLodgerAsync(hotelId, id, trackChanges: false);
            if (lodgerForHotel == null)
            {
                _logger.LogInfo($"Lodger with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Lodger.DeleteLodger(lodgerForHotel);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLodgerForHotel(Guid hotelId, Guid id, [FromBody] LodgerForUpdateDto lodger)
        {
            if (lodger == null)
            {
                _logger.LogError("LodgerForUpdateDto object sent from client is null.");
                return BadRequest("LodgerForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the LodgerForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var hotel = await _repository.Hotel.GetHotelAsync(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerEntity = await _repository.Lodger.GetLodgerAsync(hotelId, id, trackChanges: true);
            if (lodgerEntity == null)
            {
                _logger.LogInfo($"Lodger with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(lodger, lodgerEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateLodgerForHotel(Guid hotelId, Guid id, [FromBody] JsonPatchDocument<LodgerForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var hotel = await _repository.Hotel.GetHotelAsync(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerEntity = await _repository.Lodger.GetLodgerAsync(hotelId, id, trackChanges: true);
            if (lodgerEntity == null)
            {
                _logger.LogInfo($"Lodger with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var lodgerToPatch = _mapper.Map<LodgerForUpdateDto>(lodgerEntity);
            patchDoc.ApplyTo(lodgerToPatch, ModelState);
            TryValidateModel(lodgerToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(lodgerToPatch, lodgerEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
