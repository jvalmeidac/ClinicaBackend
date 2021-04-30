using System.Text;

namespace backend.Domain.Extensions
{
    public static class StringExtensions
    {
        //Criptografa a senha para MD5
        public static string Encrypt(this string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            string password = (text += "5ebe2294ecd0e0f08eab7690d2a6ee69");
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder stringBuilder = new();
            foreach(var b in bytes)
            {
                stringBuilder.Append(b.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
