using AutoMapper;
using CarWashApi.Models;
using DataBase.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashApi.Controllers
{
    [Route("api/carWash")]
    [ApiController]
    public class CarWashController : ControllerBase
    {
        private readonly CarWashDbContext _carWashDb;
        private readonly IMapper _mapper;
        public CarWashController(CarWashDbContext dbContext, IMapper mapper)
        {
            _carWashDb = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Pokaż wszystkie Myjne Samochodowe")]
        public ActionResult<IEnumerable<CarWash>> GetAll()
        {
            var carWash = _carWashDb
                .CarWashes
                .Include(r => r.Address)
                .ToList();

            var carWashDtos = _mapper.Map<List<CarWashDto>>(carWash);

            return Ok(carWashDtos);
        }
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Pokaż wszystkie wybraną myjnie")]
        public ActionResult<CarWash> Get([FromRoute] int id)
        {
            var carWash = _carWashDb.CarWashes.Include(r => r.Address).FirstOrDefault(r => r.Id == id);
            if (carWash is null)
            {
                return NotFound();
            }
            var carWashDto = _mapper.Map<CarWashDto>(carWash);
            return Ok(carWashDto);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Utwórz nowe profil restauracji z adresem do niej")]
        public ActionResult<CarWash> Create(CarWashCreateDto dto)
        {
            var carWash = _mapper.Map<CarWash>(dto);
            _carWashDb.CarWashes.Add(carWash);
            _carWashDb.SaveChanges();

            return Created($"/api/restaurant/{carWash.Id}", null);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Usuń restaracje po ID")]
        public ActionResult<CarWash> Delete([FromRoute] int id)
        {
            var carwash = _carWashDb.CarWashes.FirstOrDefault(x => x.Id == id);
            var address = _carWashDb.Address.FirstOrDefault(x => x.Id == id);
            if (carwash is null && address is null)
            {
                return BadRequest("Restaurant not found");
            }

            _carWashDb.CarWashes.Remove(carwash);
            _carWashDb.Address.Remove(address);
            _carWashDb.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modyfikuj Danne Myjni Samochodowej")]
        public ActionResult<CarWash> Modify([FromRoute] int id, [FromBody] CarWashModifyDto dto)
        {
            var carWashHas = _carWashDb.CarWashes.FirstOrDefault(x => x.Id == id);
            if (carWashHas is null)
            {
                return BadRequest();
            }
            carWashHas.Name = dto.Name;
            carWashHas.Description = dto.Description;
            carWashHas.ContactEmail = dto.ContactEmail;
            carWashHas.ContactNumber = dto.ContactNumber;
            carWashHas.NameOfOwner = dto.NameOfOwner;

            _carWashDb.SaveChanges();

            return Ok();
        }

    }
}
