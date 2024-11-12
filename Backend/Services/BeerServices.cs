using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerServices : IBeerServices
    {

        private readonly StoredContext _context;

        public BeerServices(StoredContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BeerDto>> GetBeer()
        
            => await _context.beers.Select(b => new BeerDto
            {
                Id = b.BeerId,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandId = b.BrandId,
            }).AsNoTracking().ToListAsync();
        
        public async Task<BeerDto> GetById(int Id)
        {
            var response = await _context.beers.FindAsync(Id);

            if (response != null)
            {
                
                var beer = new BeerDto
                {
                    Id = response.BeerId,
                    Name = response.Name,
                    Alcohol = response.Alcohol,
                    BrandId = response.BrandId

                };
            }
            return null;
        }
        public Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int Id)
        {
            throw new NotImplementedException();
        }

       

       
        public Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
