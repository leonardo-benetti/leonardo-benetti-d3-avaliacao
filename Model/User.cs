using System.Security.Cryptography;

namespace leonardo_benetti_d3_avaliacao.Model
{
    internal class User
    {
        public int IdUser { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public byte[]? Password  { get; set; }

    }
}
