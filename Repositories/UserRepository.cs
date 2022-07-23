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

        public User Create(User user)
        {
            using (SqlConnection con = new(stringConnection))
            {
                string queryInsert = "INSERT INTO Users (Name, Email, Password) VALUES (@Name, @Email, @Password)";

                using (SqlCommand cmd = new(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

            using (SqlConnection con = new(stringConnection))
            {
                string querySelect = "SELECT IdUser FROM Users WHERE Name LIKE @Name AND Email LIKE @Email AND Password LIKE @Password";

                SqlDataReader rdr;
                using (SqlCommand cmd = new(querySelect, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa a query
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        user.IdUser = Convert.ToInt32(rdr["IdUser"]);
                    }
                }
            }
            return user;
        }

        public List<User> ReadAll()
        {
            List<User> listUsers = new();

            using (SqlConnection con = new SqlConnection(stringConnection))
            {
                string querySelectAll = "SELECT IdUser, Name, Email, Password FROM Users";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        User user = new()
                        {
                            IdUser = Convert.ToInt32(rdr["IdUser"]),

                            Name = rdr["Name"].ToString(),

                            Email = rdr["Email"].ToString(),

                            Password = (byte[])rdr["Password"]
                        };

                        listUsers.Add(user);
                    }
                }
            }

            return listUsers;
        }

        public void Delete(int idUser)
        {
            using (SqlConnection con = new(stringConnection))
            {
                string queryDelete = "DELETE FROM Users WHERE IdUser = @IdUser";

                using (SqlCommand cmd = new(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@IdUser", idUser);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(User user)
        {
            using (SqlConnection con = new(stringConnection))
            {
                string queryUpdate = "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password WHERE IdUser = @IdUser";

                using (SqlCommand cmd = new(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@IdUser", user.IdUser);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
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
