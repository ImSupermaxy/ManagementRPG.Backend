using FluentValidation;
using ManagementRPG.Domain.Abstractions.Validators;
using ManagementRPG.Domain.Security.Usuarios.Entities;

namespace ManagementRPG.Domain.Security.Usuarios.Validators
{
    public class UsuarioValidator : ValidatorEntity<Usuario, int, int>
    {
        public UsuarioValidator(Usuario entity) 
            : base(entity)
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .NotNull()
                .Must(u => u.Split(' ').Count() >= 2)
                .Must(u => u.Split(' ').ToList().Select(v => 
                    char.IsUpper(v[0])).All(v => v == true))
                .Must(u => u.Split(' ').Select(x => 
                    x.Select(v => char.IsLetter(v)).All(v => v == true)).All(v => v == true))
                .WithMessage("Nome do usuário inválido!");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .Must(u => u.Contains('@'))
                .Must(u => u.Contains('.'))
                .WithMessage("Insira um Email válido!");

            RuleFor(x => x.Arroba)
                .NotEmpty()
                .NotNull()
                .Must(u => u.Split(' ').Count() == 1)
                .Must(u => u.Split(' ').Select(x =>
                    x.Select(v => char.IsLetterOrDigit(v)).All(v => v == true)).All(v => v == true))
                .WithMessage("Arroba do usuário inválido!");

            var result = Validate(entity);

            if (!result.IsValid)
                entity.UpdateValid(result.Errors);
            else
                entity.UpdateValid();
        }
    }
}
