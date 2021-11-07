using AutoMapper;
using CarWashApi.Models;
using DataBase.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashApi.Controllers
{
    [Route("api/carWash")]
    public class CarWashController:ControllerBase
    {
        private readonly CarWashDbContext _carWashDb;
        private readonly IMapper _mapper;
        public CarWashController(CarWashDbContext dbContext, IMapper mapper)
        {
            _carWashDb = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CarWash>> GetAll()
        {
            var carWash = _carWashDb
                .CarWashes
                .ToList();

            var carWashDtos = _mapper.Map<List<CarWashDto>>(carWash);

            return Ok(carWashDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<CarWash> Get([FromRoute] int id)
        {
            var carWash = _carWashDb.CarWashes.FirstOrDefault(r => r.Id == id);
            if (carWash is null)
            {
                return NotFound();
            }
            var carWashDto = _mapper.Map<CarWashDto>(carWash);
            return Ok(carWashDto);
        }
    }
}
