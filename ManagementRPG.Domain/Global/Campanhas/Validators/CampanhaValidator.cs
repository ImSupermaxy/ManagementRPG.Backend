using FluentValidation;
using ManagementRPG.Domain.Abstractions.Validators;
using ManagementRPG.Domain.Global.Campanhas.Entities;

namespace ManagementRPG.Domain.Global.Campanhas.Validators
{
    public class CampanhaValidator : ValidatorEntityDefault<Campanha, int, int>
    {
        public CampanhaValidator(Campanha entity)
            : base(entity)
        {
            RuleFor(x => x.MestreId).NotEmpty().Must(c => c > 0).WithMessage("Deve haver um Mestre para a Campanha");
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome da Campanha é obrigatório");
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("A descrição do projeto não pode ser vazia.");
            RuleFor(x => x.Sinopse).NotEmpty().WithMessage("A descrição do projeto não pode ser vazia.");

            var result = Validate(entity);

            if (!result.IsValid)
                entity.UpdateValid(result.Errors);
            else
                entity.UpdateValid();
        }
    }
}
