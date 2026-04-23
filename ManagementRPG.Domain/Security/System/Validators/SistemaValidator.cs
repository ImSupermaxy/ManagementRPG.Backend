using FluentValidation;
using ManagementRPG.Domain.Abstractions.Validators;
using ManagementRPG.Domain.Security.System.Entities;

namespace ManagementRPG.Domain.Security.System.Validators
{
    public class SistemaValidator : ValidatorEntity<Sistema, int, int>
    {
        public SistemaValidator(Sistema entity)
            : base(entity)
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome do sistema é obrigatório");
            RuleFor(x => x.Versao).NotEmpty().NotNull().WithMessage("A versão do sistema do projeto não pode ser vazia ou nula.");
            RuleFor(x => x.Status).NotEqual(Enums.EStatusSistema.Todos).WithMessage("Valor do status não é válido para inserir ou atualizar");
        }
    }
}
