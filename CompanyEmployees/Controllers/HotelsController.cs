﻿using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
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
    }
}
