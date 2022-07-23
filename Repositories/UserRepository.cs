using leonardo_benetti_d3_avaliacao.Interfaces;
using leonardo_benetti_d3_avaliacao.Model;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace leonardo_benetti_d3_avaliacao.Repositories
{
    internal class UserRepository : IUser
    {

        private readonly string stringConnection = "Data Source=LEONARDOBENETTI\\SQLEXPRESS; initial catalog=leonardo-benetti-d3-avaliacao; integrated security=true;";

        public void Create(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int idUser)
        {
            throw new NotImplementedException();
        }

        public List<User> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public User CheckLogin(string email, byte[] hashedPassword)
        {
            using (SqlConnection con = new SqlConnection(stringConnection))
            {
                string queryFindUser = "SELECT IdUser, Name, Email, Password FROM Users WHERE Email LIKE @Email";

                SqlDataReader rdr;
                User user;

                using (SqlCommand cmd = new(queryFindUser, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    con.Open();

                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        user = new()
                        {
                            IdUser = Convert.ToInt32(rdr["IdUser"]),

                            Name = rdr["Name"].ToString(),

                            Email = rdr["Email"].ToString(),

                            Password = (byte[])rdr["Password"]
                        };
                    }
                    else
                    {
                        throw new Exception("User not found in database");
                    }
                }

                if (hashedPassword.SequenceEqual(user.Password))
                {
                    return user;
                }
                else
                {
                    throw new Exception("Incorrect Password");
                }
            }
        }
    }
}
