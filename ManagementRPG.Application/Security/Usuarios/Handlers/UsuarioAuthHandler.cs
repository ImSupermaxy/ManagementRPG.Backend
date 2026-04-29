using ManagementRPG.Application.Security.System.Commands;
using ManagementRPG.Application.Security.Usuarios.Commands;
using ManagementRPG.Application.Security.Usuarios.Errors;
using ManagementRPG.Application.Security.Usuarios.Token;
using ManagementRPG.Application.Utils;
using ManagementRPG.Domain.Abstractions.Commands.Handlers;
using ManagementRPG.Domain.Abstractions.Handlers;
using ManagementRPG.Domain.Abstractions.Messages.Errors;
using ManagementRPG.Domain.Abstractions.Messages.Successes;
using ManagementRPG.Domain.Security.Usuarios.Entities;
using ManagementRPG.Domain.Security.Usuarios.Enums;
using ManagementRPG.Domain.Security.Usuarios.Repositories;
using ManagementRPG.Domain.Shared.ApiConfig;
using ManagementRPG.Domain.Shared.ApiConfig.Settings;
using ManagementRPG.Domain.Shared.Commands;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using V4MAutoMapper;

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
        private IUsuarioAuthLogRepository RepositoryAuthLog;
        private IUsuarioPerfilRepository RepositoryPerfil;
        //private ITokenRepository RepositoryToken;
        private IMapper Mapper;
        private IAuthSettings Settings;

        public UsuarioAuthHandler(
            ISender sender,
            IUsuarioRepository repository,
            IUsuarioAuthLogRepository repositoryAuthLog,
            IUsuarioPerfilRepository repositoryPerfil,
            /*, ITokenRepository repositoryToken*/
            IMapper mapper,
            IAuthSettings settings)
        {
            Sender = sender;
            Repository = repository;
            RepositoryAuthLog = repositoryAuthLog;
            RepositoryPerfil = repositoryPerfil;
            //this.RepositoryToken = repositoryToken;
            Mapper = mapper;
            Settings = settings;
        }

        public async Task<Result<TokenModel>> Handle(UsuarioCommandRegister request, CancellationToken cancellationToken)
        {
            try
            {
                //Mapeia e valida a entidade apartir do command
                var entity = Mapper.Map<Usuario>(request);
                if (!entity.IsValid) return Result.Failure<TokenModel>(UsuarioError.InvalidCredetials);

                //Verifica se o usuário existe
                if (await Repository.UsuarioExist(entity.Email, entity.Arroba)) return Result.Failure<TokenModel>(UsuarioError.AlreadyExist);

                //Obtém o hash e Atualiza a senha da entidade
                var senhaHash = SenhaHasher.GerarHash(request.Password);
                entity.UpdateSenha(senhaHash);

                //Insere o usuário
                var newId = await Repository.Register(entity);
                if (!(newId > 0)) return Result.Failure<TokenModel>(UsuarioError.NotRegistered);

                //Obtém o sistema
                var resultsistema = await Sender.Send(new SistemaCommandGetValidation());
                if (resultsistema.IsFailure) return Result.Failure<TokenModel>(resultsistema.Error);

                //TODO:
                //Alterar para um handler a parte
                //Insere os perfís do usuário
                var resultPerfil = await RepositoryPerfil.Insert(UsuarioPerfil.GetDefaultEntity(newId, resultsistema.Value));
                if (!resultPerfil) return Result.Failure<TokenModel>(UsuarioError.FailureRegistered).Chain(resultsistema);

                //Realiza o Login do usuário
                var commandLogin = new UsuarioCommandLogin(request.Email, request.Password, null);
                var resultLogin = await Sender.Send(commandLogin);
                if (resultLogin.IsFailure) return resultLogin;
                resultLogin.Chain(resultsistema);

                //Retorna sucesso do ação do método
                return Result.Success(resultLogin.Value, 
                    SuccessMethodTask<UsuarioCommandRegister>.CommandMethod, 
                    SuccessTask.GetRunedMethodName()
                ).Chain(resultLogin);
            }
            catch (Exception ex)
            {
                return Result.Failure<TokenModel>(SystemError.GenericError, [ex.Message]);
            }
        }

        public async Task<Result<TokenModel>> Handle(UsuarioCommandLogin request, CancellationToken cancellationToken)
        {
            //TODO:
            //Trocar pela chamada do handler...
            //Verifica se o usuário existe
            var usuario = await Repository.GetByEmail(request.Email);
            if (usuario is null) return Result.Failure<TokenModel>(UsuarioError.FailureLogin);

            //Command de autenticação
            var command = new UsuarioAuthLogCommandInsert() { UsuarioId = usuario.Id, SenhaHash = usuario.SenhaHash };
            var dispatchFailLog = async (Result lastResult = null!) =>
            {
                command.Login = false;

                //TODO:
                //Alterar para um handler a parte
                //Insere o log do login do usuário
                var authLog = Mapper.Map<UsuarioAuthLog>(command);
                await RepositoryAuthLog.Authenticate(authLog);
                var result = Result.Failure<TokenModel>(UsuarioError.FailureLogin);

                return lastResult is not null ? result.Chain(lastResult) : result;
            };

            //Valida a senha do usuário com a senha cadastrada
            if (!SenhaHasher.VerificarSenha(request.Senha, usuario.SenhaHash)) await dispatchFailLog();

            //Obtém o sistema
            var resultsistema = await Sender.Send(new SistemaCommandGetValidation());
            if (resultsistema.IsFailure) return await dispatchFailLog(resultsistema);

            //TODO:
            //Alterar para um handler a parte
            var resultPerfil = await RepositoryPerfil.GetByUsuarioSistema(usuario.Id, resultsistema.Value);
            if (resultPerfil == null || !resultPerfil.Any()) return await dispatchFailLog(resultsistema);

            //Gera o token de autenticação
            var resultToken = await GerarJwt(request.Email, usuario.Id, usuario.Nome, (resultPerfil?.Select(r => r.Perfil) ?? UsuarioPerfil.GetDefaultPerfil.ToList()));
            if (resultToken.IsFailure) await dispatchFailLog(resultsistema.Chain(resultToken));

            //var roles = resultToken.Value.UserToken.Claims!.Select(c => c.Type == "roles") ?? default;
            var token = resultToken.Value.AccessToken;
            command.Login = true;
            command.Token = token;

            var log = Mapper.Map<UsuarioAuthLog>(command);
            var commandResultLog = await RepositoryAuthLog.Authenticate(log);

            //Retorna sucesso na ação do método
            return Result.Success(resultToken.Value, SuccessMethodTask<UsuarioCommandLogin>.CommandMethod, SuccessTask.GetRunedMethodName()).Chain(resultsistema);
        }

        public Task<Result> Handle(UsuarioCommandResetPassword request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Handle(UsuarioCommandUpdatePassword request, CancellationToken cancellationToken)
        {
            //Obtém o usuário pelo email
            var usuario = await Repository.GetByEmail(request.Email);
            if (usuario is null) return Result.Failure(UsuarioError.InvalidCredetials);

            ////TODO:
            ////Criar Tabela para salvar os tokens de redefinição de senha
            ////Criptografar o codigo aqui e comparar com o código criptografado na tabela de TokenRedefinirSenha

            //var tokenChangePass = RepositoryToken.Get(request.SecurityCode);
            //if (tokenChangePass is null)
            //    return new CommandResult(false, "Tempo para redefinição de sneha expirou");

            //Obtem a entidade do usuário
            var command = Mapper.Map<UsuarioCommandUpdate>(usuario);
            var entity = Mapper.Map<Usuario>(command);

            //Gera o hash da nova senha
            var senhaHash = SenhaHasher.GerarHash(request.NewPassword);
            entity.UpdateSenha(senhaHash);

            //Atualiza o usuário
            if (!await Repository.Update(entity)) return Result.Failure(EntityError<Usuario>.NotUpdated);

            return Result.Success(SuccessMethodTask<UsuarioCommandUpdatePassword>.CommandMethod, SuccessTask.GetRunedMethodName());
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
                claims.Add(new Claim(TokenAuthConfig.Role, userRole.ToString()));

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
                    Audience = Settings.Audience,
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
                    UserToken = RunMode.IsDev() ? new UserTokenViewModel
                    {
                        Id = id.ToString(),
                        Email = email,
                        Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                    } : null!
                };

                return Result.Success(response, SuccessMethodTask.CustomMethod("GerarJwt", ""), SuccessTask.GetRunedMethodName());
            }
            catch (Exception ex)
            {
                return Result.Failure<TokenModel>(UsuarioError.FailureAuthentication);
            }
        }
    }
}
