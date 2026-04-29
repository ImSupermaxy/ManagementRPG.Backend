namespace ManagementRPG.Application.Utils
{
    internal class SenhaHasher
    {
        public static string GerarHash(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new ArgumentException("A senha não pode ser vazia ou nula.", nameof(senha));
            }

            return BCrypt.Net.BCrypt.HashPassword(senha);
        }
        public static bool VerificarSenha(string senha, string hash)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new ArgumentException("A senha não pode ser vazia ou nula.", nameof(senha));
            }

            if (string.IsNullOrWhiteSpace(hash))
            {
                throw new ArgumentException("O hash não pode ser vazio ou nulo.", nameof(hash));
            }

            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }
    }
}
