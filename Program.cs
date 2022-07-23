using leonardo_benetti_d3_avaliacao.Model;
using leonardo_benetti_d3_avaliacao.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace leonardo_benetti_d3_avaliacao
{
    internal class Program
    {
        const string lineBreak = "================================================";

       // var sb = new StringBuilder("new byte[] { ");
       // foreach (var b in hashedPassword)
       // {
       //     sb.Append(b.ToString("x") + ", ");
       //}
       //sb.Append("}");
       //Console.WriteLine(sb.ToString());

        static void Main(string[] args)
        {
            UserRepository _user = new();
            LogRepository _log = new();

            string option;
            bool logged_in = false;
            User user = new User();

            do
            {
                if (logged_in)
                {
                    Console.WriteLine(lineBreak);
                    Console.WriteLine($"Seja bem vindo {user.Name}!");
                    Console.WriteLine(lineBreak);
                    Console.WriteLine("Escolha uma das opções abaixo:");
                    Console.WriteLine("1 - Deslogar");
                    Console.WriteLine("0 - Encerrar sistema");

                    option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            _log.RegisterLogout(user);
                            logged_in = false;
                            user = new User();
                            Console.WriteLine("Logout efetuado com sucesso\n\n");
                            break;
                        case "0":
                            Console.WriteLine("Encerrando sistema");
                            break;
                        default:
                            Console.WriteLine("Operacao invalida");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(lineBreak);
                    Console.WriteLine("Bem vindo ao programa");
                    Console.WriteLine(lineBreak);
                    Console.WriteLine("Escolha uma das opções abaixo:");
                    Console.WriteLine("1 - Acessar");
                    Console.WriteLine("0 - Cancelar");

                    option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            Console.WriteLine("Forneca suas credenciais:");
                            Console.Write("email: ");
                            var email = Console.ReadLine();
                            Console.Write("senha: ");
                            var password = Console.ReadLine();

                            byte[] hashedPassword;

                            using (SHA512 sha512Hash = SHA512.Create())
                            {
                                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                                hashedPassword = sha512Hash.ComputeHash(sourceBytes);
                            }

                            try
                            {
                                user = _user.CheckLogin(email, hashedPassword);
                                _log.RegisterLogin(user);
                                logged_in = true;

                                Console.WriteLine("Login efetuado com sucesso!\n\n");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                                Console.WriteLine("Credenciais invalidas\n\n");
                            }

                            break;
                        case "0":
                            Console.WriteLine("Encerrando sistema");
                            break;
                        default:
                            Console.WriteLine("Operacao invalida");
                            break;
                    }
                }

            } while (option != "0");
        }
    }
}