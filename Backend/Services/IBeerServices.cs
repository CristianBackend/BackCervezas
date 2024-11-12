using Backend.Dtos;

namespace Backend.Services
{
    public interface IBeerServices
    {
        Task<IEnumerable<BeerDto>> GetBeer();
        Task<BeerDto> GetById(int Id);

        Task<BeerDto> Add(BeerInsertDto beerInsertDto);

        Task<BeerDto> Update(int id,BeerUpdateDto beerUpdateDto);

        Task DeleteById(int Id);
    }
}
