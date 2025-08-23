using System.Security.Cryptography;

namespace UnitTestsGeneral
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string random = GenerateRandomString(32);
            Console.WriteLine(random);

            return; 
        }

        private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GenerateRandomString(int length = 32)
        {
            var result = new char[length];
            using var rng = RandomNumberGenerator.Create();
            var buffer = new byte[sizeof(uint)];

            for (int i = 0; i < length; i++)
            {
                rng.GetBytes(buffer);
                uint num = BitConverter.ToUInt32(buffer, 0);
                result[i] = AllowedChars[(int)(num % (uint)AllowedChars.Length)];
            }

            return new string(result);
        }
    }
}