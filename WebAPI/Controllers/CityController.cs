using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{

    [Authorize]
    public class CityController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _uow.CityRepository.GetCitiesAsync();
            var cityDto = _mapper.Map<IEnumerable<CityDto>>(cities);
          /*  var cityDto = from c in cities
                          select new CityDto()
                          {
                              Id = c.Id,
                              Name = c.Name
                          };
            */
            return Ok(cityDto);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            
            /*var city = new City();
            city.Name = cityDto.Name;
            */
            _uow.CityRepository.AddCity(city);
            await _uow.SaveAsync();
            return Ok(cityDto);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(CityDto cityDto, int id)
        {
            var cityFromDb = await _uow.CityRepository.GetCity(id);
            if(cityFromDb == null || cityFromDb.Id != id)
            {
                return BadRequest("Update not allowed");
            }

            _mapper.Map(cityDto, cityFromDb);
            await _uow.SaveAsync();
            return StatusCode(200);

        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateCityPatch(JsonPatchDocument<City> cityToPatch, int id)
        {
            var cityFromDb = await _uow.CityRepository.GetCity(id);
            cityToPatch.ApplyTo(cityFromDb, ModelState);
            await _uow.SaveAsync();
            return StatusCode(200);

        }


        //[HttpPost("add")]
        [HttpPost("add/{cityName}")]
        public async Task<IActionResult> AddCity(string cityName)
        {
            City city = new City();
            city.Name = cityName;
            _uow.CityRepository.AddCity(city);
            await _uow.SaveAsync();
            return Ok(city);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            _uow.CityRepository.DeleteCity(id);
            await _uow.SaveAsync();
            return Ok(id);

        }
        
    }
}
