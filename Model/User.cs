using System.Security.Cryptography;
using System.Text;

namespace leonardo_benetti_d3_avaliacao.Model
{
    internal class User
    {
        public int IdUser { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public byte[]? Password  { get; set; }

        public User()
        {

        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            byte[] hashedPassword;

            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                hashedPassword = sha512Hash.ComputeHash(sourceBytes);
            }

            Password = hashedPassword;
        }

        public bool Equals(User obj)
        {
            return Name == obj.Name && Email == obj.Email && Password.SequenceEqual(obj.Password);
        }
    }
}
