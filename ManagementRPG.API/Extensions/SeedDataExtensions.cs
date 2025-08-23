using Bogus;
using System.Data;

namespace ExpensesMonitor.Api.Extensions
{
    internal static class SeedDataExtensions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            //Teste para inserir uma campanha...

            //using IServiceScope scope = app.ApplicationServices.CreateScope();

            //ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
            //using IDbConnection connection = sqlConnectionFactory.CreateConnection();

            //var faker = new Faker();

            //List<object> notificacoes = new();
            //for (int i = 0; i < 2; i++)
            //{
            //    notificacoes.Add(new
            //    {

            //        Id = Guid.NewGuid(),
            //        UsuarioId = Guid.NewGuid(),
            //        Mensagem = $"Teste seedData {i}",
            //        DataEnvio = DateTime.UtcNow,
            //        IsLida = false

            //    });
            //}

            //const string sql = """
            //INSERT INTO public.Notificacoes
            //(Id, UsuarioId, Mensagem, DataEnvio, IsLida)
            //VALUES (@Id, @UsuarioId, @Mensagem, @DataEnvio, @IsLida)";
            //""";

            //connection.Execute(sql, notificacoes);
        }

    }
}
