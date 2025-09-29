using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Application.Security.Usuarios.Errors;
using ManagementRPG.Application.Security.Usuarios.Mappers;
using ManagementRPG.Application.Security.Usuarios.Token;
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
        ICommandHandler<UsuarioCommandRegister, TokenModel>,
        ICommandHandler<UsuarioCommandLogin, TokenModel>,
        ICommandHandler<UsuarioCommandResetPassword>,
        ICommandHandler<UsuarioCommandUpdatePassword>
    {
        public ISender Sender;
        private IUsuarioRepository Repository;
        //private ISistemaRepository RepositorySistema;
        //private ITokenRepository RepositoryToken;
        private UsuarioMapper Mapper;
        private UsuarioAuthLogMapper MapperAuthLog;
        private IAppSettings Settings;

        public UsuarioAuthHandler(
            ISender sender,
            IUsuarioRepository repository,
            //ISistemaRepository repositorySistema,
            /*, ITokenRepository repositoryToken*/
            UsuarioMapper mapper,
            UsuarioAuthLogMapper mapperAuthLog, 
            IAppSettings settings)
        {
            Sender = sender;
            Repository = repository;
            //RepositorySistema = repositorySistema;
            Mapper = mapper;
            MapperAuthLog = mapperAuthLog;
            Settings = settings;
            //this.RepositoryToken = repositoryToken;
        }

        public async Task<Result<TokenModel>> Handle(UsuarioCommandRegister request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = Mapper.GetEntity(request);
                if (!entity.IsValid)
                    return Result.Failure<TokenModel>(UsuarioError.InvalidCredetials);

                //REMOVE this validation
                if (await Repository.UsuarioExist(entity.Email))
                    return Result.Failure<TokenModel>(UsuarioError.AlreadyExist);

                //REMOVE this validation
                if (await Repository.UsuarioExist(null, entity.Arroba))
                    return Result.Failure<TokenModel>(UsuarioError.AlreadyExist);

                var senhaHash = SenhaHasher.GerarHash(request.Password);
                entity.UpdateSenha(senhaHash);

                var newId = await Repository.Register(entity);
                if (!(newId > 0))
                    return Result.Failure<TokenModel>(UsuarioError.NotRegistered);

                var resultToken = await GerarJwt(request.Email, entity.Id, entity.Nome, entity.Perfis);
                if (resultToken.IsFailure)
                    return resultToken;

                var roles = resultToken.Value.UserToken.Claims!.Select(c => c.Type == "roles") ?? default;
                var token = resultToken.Value.AccessToken;

                var resultsistema = await Sender.Send(new SistemaCommandGetValidation());
                if (resultsistema.IsFailure)
                    return Result.Failure<TokenModel>(resultsistema.Error);

                //sender insert na tabela de usuário perfil
                var resultPerfil = await Repository.InsertUpdatePerfis(entity.Perfis.ToArray(), newId, resultsistema.Value);
                if (!resultPerfil)
                    return Result.Failure<TokenModel>(UsuarioError.FailureRegistered);

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
                return Result.Failure<TokenModel>(SystemError.GenericError, ex.Message);
            }
        }

        public async Task<Result<TokenModel>> Handle(UsuarioCommandLogin request, CancellationToken cancellationToken)
        {
            var usuario = await Repository.GetByEmail(request.Email);
            if (usuario is null)
                return Result.Failure<TokenModel>(UsuarioError.FailureLogin);

            if (!SenhaHasher.VerificarSenha(request.Senha, usuario.SenhaHash))
                return Result.Failure<TokenModel>(UsuarioError.FailureLogin);

            var resultToken = await GerarJwt(request.Email, usuario.Id, usuario.Nome, usuario.Perfis);
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

        private async Task<Result<TokenModel>> GerarJwt(string email, int id, string name, IEnumerable<EPerfil> userRoles = null!)
        {
            var claims = new List<Claim> //todo: criar claims
            {
                new Claim(TokenAuthConfig.UserIdentifier, id.ToString()),
            };
            userRoles = userRoles ?? new List<EPerfil> { EPerfil.USUARIO };

            // Adiciona as claims padrão do JWT
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, name.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64));

            // Adiciona as roles como claims
            foreach (var userRole in userRoles)
                claims.Add(new Claim(TokenAuthConfig.Role, EnumHelper<EPerfil>.GetDisplayValue(userRole)));

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
                var response = new TokenModel
                {
                    AccessToken = encodedToken,
                    ExpiresIn = TimeSpan.FromHours(Settings.ExpirationHours).TotalSeconds,
                    UserToken = new UserTokenViewModel
                    {
                        Id = id.ToString(),
                        Email = email,
                        Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                    }
                };

                return Result.Success(response);
            }
            catch (Exception ex)
            {
                return Result.Failure<TokenModel>(UsuarioError.FailureAuthentication);
            }
        }
    }
}
