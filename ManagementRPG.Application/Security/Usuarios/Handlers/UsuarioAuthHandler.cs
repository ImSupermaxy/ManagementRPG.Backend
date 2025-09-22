using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Application.Security.Usuarios.Errors;
using ManagementRPG.Application.Security.Usuarios.Mappers;
using ManagementRPG.Application.Utils;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Errors;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Enums;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using ManagementRPG.Domain.Shared.ApiConfig.Authentication;
using ManagementRPG.Domain.Shared.Commands;
using ManagementRPG.Domain.Shared.Helpers;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagementRPG.Application.Security.Usuarios.Handlers
{
    public class UsuarioAuthHandler : IHandler,
        ICommandHandler<UsuarioCommandRegister, object>,
        ICommandHandler<UsuarioCommandLogin, object>,
        ICommandHandler<UsuarioCommandResetPassword>,
        ICommandHandler<UsuarioCommandUpdatePassword>
    {
        //public ISender Sender;
        private IUsuarioRepository Repository;
        //private ISistemaRepository RepositorySistema;
        //private ITokenRepository RepositoryToken;
        private UsuarioMapper Mapper;
        private UsuarioAuthLogMapper MapperAuthLog;
        private IAppSettings Settings;

        public UsuarioAuthHandler(
            //ISender sender,
            IUsuarioRepository repository,
            //ISistemaRepository repositorySistema,
            /*, ITokenRepository repositoryToken*/
            UsuarioMapper mapper,
            UsuarioAuthLogMapper mapperAuthLog, 
            IAppSettings settings)
        {
            //Sender = sender;
            Repository = repository;
            //RepositorySistema = repositorySistema;
            Mapper = mapper;
            MapperAuthLog = mapperAuthLog;
            Settings = settings;
            //this.RepositoryToken = repositoryToken;
        }

        public async Task<Result<object>> Handle(UsuarioCommandRegister request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = Mapper.GetEntity(request);
                if (!entity.IsValid)
                    return Result.Failure(UsuarioError.InvalidCredetials);

                if (await Repository.UsuarioExist(entity))
                    return Result.Failure(UsuarioError.AlreadyExist);

                var senhaHash = SenhaHasher.GerarHash(request.Password);
                entity.UpdateSenha(senhaHash);

                var newId = await Repository.Register(entity);
                if (newId > 0)
                    return Result.Failure(UsuarioError.NotRegistered);

                var resultToken = await GerarJwt(request.Email, entity.Id, entity.Perfis);
                if (resultToken.IsFailure)
                    return resultToken;

                var roles = (resultToken.Value.UserToken.Claims as List<dynamic>)!.Select(c => c.Type == "roles") ?? default;
                var token = resultToken.Value.AccessToken as string ?? default;

                //var resultsistema = await Sender.Send(new SistemaCommandGetValidation());
                //if (resultsistema == null)
                //    return Result.Failure(SystemError.GenericError);
                //if (resultsistema.IsFailure)
                //    return resultsistema;

                //sender insert na tabela de usuário perfil
                var resultPerfil = await Repository.InsertUpdatePerfis(entity.Perfis.ToArray());
                if (!resultPerfil)
                    return Result.Failure(UsuarioError.FailureRegistered);

                var log = MapperAuthLog.GetAuthenticate(new UsuarioAuthLogCommandInsert()
                {
                    UsuarioId = newId,
                    SenhaHash = senhaHash,
                    Token = token!
                });
                var commanResultLog = await Repository.Authenticate(log);

                return Result.Success(resultToken.Value);
            }
            catch (Exception ex)
            {
                return Result.Failure(SystemError.GenericError, ex.Message);
            }
        }

        public async Task<Result<object>> Handle(UsuarioCommandLogin request, CancellationToken cancellationToken)
        {
            var usuario = await Repository.GetByEmail(request.Email);
            if (usuario is null)
                return Result.Failure(UsuarioError.FailureLogin);

            if (!SenhaHasher.VerificarSenha(request.Senha, usuario.SenhaHash))
                return Result.Failure(UsuarioError.FailureLogin);

            var resultToken = await GerarJwt(request.Email, usuario.Id, usuario.Perfis);
            return Result.Success(resultToken.Value);
        }

        public Task<Result> Handle(UsuarioCommandResetPassword request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Handle(UsuarioCommandUpdatePassword request, CancellationToken cancellationToken)
        {
            ////TODO
            ////Criar Tabela para salvar os tokens de redefinição de senha
            ////Criptografar o codigo aqui e comparar com o código criptografado na tabela de TokenRedefinirSenha
            
            //var tokenChangePass = RepositoryToken.Get(request.SecurityCode);
            //if (tokenChangePass is null)
            //    return new CommandResult(false, "Tempo para redefinição de sneha expirou");

            var usuario = await Repository.GetByEmail(request.Email);

            if (usuario is null)
                return Result.Failure(UsuarioError.InvalidCredetials);

            var entity = Mapper.GetEntity(usuario);

            var senhaHash = SenhaHasher.GerarHash(request.NewPassword);
            entity.UpdateSenha(senhaHash);

            if (!await Repository.Update(entity))
                return Result.Failure(EntityError<Usuario, int>.NotUpdated);

            return Result.Success();
        }

        private async Task<Result<dynamic>> GerarJwt(string email, int id, IEnumerable<EPerfil> userRoles = null!)
        {
            var claims = new List<Claim> //todo: criar claims
            {
                new Claim("CodigoUsuario", id.ToString()),
            };
            userRoles = userRoles ?? new List<EPerfil> { EPerfil.USUARIO };

            // Adiciona as claims padrão do JWT
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64));

            // Adiciona as roles como claims
            foreach (var userRole in userRoles)
                claims.Add(new Claim("role", EnumHelper<EPerfil>.GetDisplayValue(userRole)));

            // Cria a identidade de claims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            try
            {
                // Gera o token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Settings.Secret);
                var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = Settings.Sender,
                    Audience = Settings.ValidAt,
                    Subject = identityClaims,
                    Expires = DateTime.UtcNow.AddHours(Settings.ExpirationHours),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                });

                var encodedToken = tokenHandler.WriteToken(token);

                // Cria a resposta do token
                var response = new
                {
                    AccessToken = encodedToken,
                    ExpiresIn = TimeSpan.FromHours(Settings.ExpirationHours).TotalSeconds,
                    UserToken = new
                    {
                        Id = id.ToString(),
                        Email = email,
                        Claims = claims.Select(c => new { c.Type, c.Value })
                    }
                };

                return Result.Success<dynamic>(response);
            }
            catch (Exception ex)
            {
                return Result.Failure(UsuarioError.FailureAuthentication);
            }
        }
    }
}
