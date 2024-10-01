using FluentValidation;
using GARP.Application.DTO.Auto;

namespace GARP.Application.Validators
{
    public class DtoCUAutoValidator : AbstractValidator<DtoCUAuto>
    {
        public DtoCUAutoValidator()
        {
            When(c => c.Id.HasValue, () =>
            {
                RuleFor(c => c.Id).GreaterThan(0);
            });
            RuleFor(p => p.IdMarca).NotNull().NotEmpty();
            RuleFor(p => p.IdTipoAuto).NotNull().NotEmpty();
            RuleFor(p => p.Modelo).NotNull().NotEmpty();
            RuleFor(p => p.Anio).NotNull().NotEmpty();
            RuleFor(p => p.Kilometraje).NotNull();
            RuleFor(p => p.Precio).NotNull();
            RuleFor(p => p.Color).NotNull().NotEmpty();
        }
    }
}