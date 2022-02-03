using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class CityRepository : ICityRepository
    {

        private readonly DataContext _dataContext;

        public CityRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        

        public void AddCity(City city)
        {
            _dataContext.Cities.AddAsync(city);
        }

        public void DeleteCity(int cityId)
        {
            var city = _dataContext.Cities.Find(cityId);
            _dataContext.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _dataContext.Cities.ToListAsync();
        }

        
    }
}
