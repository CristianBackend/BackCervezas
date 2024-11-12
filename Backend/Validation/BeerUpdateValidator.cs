using Backend.Dtos;
using FluentValidation;

namespace Backend.Validation
{
    public class BeerUpdateValidator:AbstractValidator<BeerUpdateDto>
    {
        public BeerUpdateValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("EL nombre no puede ser null ni estar vacio");
        }
    }
}
