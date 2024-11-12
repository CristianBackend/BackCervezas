using Backend.Dtos;
using Backend.Models;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly StoredContext _contex;

        private readonly IValidator<BeerInsertDto> _validator;
        private readonly IValidator<BeerUpdateDto> _UpdateValidator;

        private readonly IBeerServices _beerServices;

        public BeerController(StoredContext context, IValidator<BeerInsertDto> validator, IBeerServices beerServices)
        {
            _contex = context;
            _validator = validator;
            _beerServices = beerServices;
            
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> GetBeer() => await _beerServices.GetBeer();
        

        [HttpGet("{Id}")]

        public async Task<IActionResult> GetById(int Id)
        {
            var beerDto = await _beerServices.GetById(Id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> InsertBeer(BeerInsertDto beerInsert)
        {


            var validator = await _validator.ValidateAsync(beerInsert);

            if (!validator.IsValid) 
            {
                return BadRequest(validator.Errors);
            }
            
            var beer = new Beer()
            {
                Name = beerInsert.Name,
                BrandId = beerInsert.BrandId,
                Alcohol = beerInsert.Alcohol

            };

            await _contex.beers.AddAsync(beer);
            await _contex.SaveChangesAsync();


            var response = new BeerDto
            {
                Name = beer.Name,
                Id = beer.BeerId,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            };


            return CreatedAtAction("InsertBeer",new BeerDto { Id = response.Id},response);


        }
        [HttpPut]

        public async Task<ActionResult<BeerDto>> Update(int id,BeerUpdateDto beerUpdate)
        {


            var UpdateValidator = await _UpdateValidator.ValidateAsync(beerUpdate);


            if(!UpdateValidator.IsValid)
            {
                return BadRequest(UpdateValidator.Errors);
            }

            var beer = await _contex.beers.FindAsync(id);

            if (beer == null) return NotFound();

            beer.Name = beerUpdate.Name;
            beer.Alcohol = beerUpdate.Alcohol;
            beer.BrandId = beerUpdate.BrandId;


            var beerReturn = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId,
            };
            await _contex.SaveChangesAsync();
           

            return Ok(beerReturn);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<BeerDto>> Delete(int Id)
        {
            var response = await _contex.beers.FindAsync(Id);

            if( response == null ) return NotFound();   



            _contex.beers.Remove(response);
            await _contex.SaveChangesAsync();

            var beerReturn = new BeerDto
            {
                Id = response.BeerId,
                Name = response.Name,
                Alcohol = response.Alcohol,
                BrandId = response.BrandId,
            };

            return Ok(beerReturn);
        }
           
       
    }
}
