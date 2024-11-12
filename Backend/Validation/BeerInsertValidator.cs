using Backend.Dtos;
using FluentValidation;

namespace Backend.Validation
{
    public class BeerInsertValidator:AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es Obligatorio");

            RuleFor(x => x.Name).Length(2, 20).WithMessage("Debe medir entre el rango");
        }
    }
}
