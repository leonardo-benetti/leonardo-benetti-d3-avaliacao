using leonardo_benetti_d3_avaliacao.Model;
using leonardo_benetti_d3_avaliacao.Repositories;
using leonardo_benetti_d3_avaliacao.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace leonardo_benetti_d3_avaliacao
{
    internal class Program
    {
        const string lineBreak = "================================================";
        const string tableBreak = "------------------------------------------------";

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
                    Console.WriteLine("2 - Atualizar dados");
                    Console.WriteLine("3 - Deletar seu usuario");
                    if (user.IdUser == 1)
                        Console.WriteLine("4 - [admin] Listar usuarios");
                    Console.WriteLine("0 - Encerrar sistema");

                    option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            _log.RegisterOperation(user, DbOperation.Logout);
                            logged_in = false;
                            user = new User();
                            Console.WriteLine("Logout efetuado com sucesso\n\n");
                            break;
                        case "2":
                            Console.WriteLine("Forneca seus novos dados abaixo");
                            Console.Write($"Forneca seu novo nome (atual = {user.Name}, se nao desejar alterar aperte apenas ENTER): ");
                            var name = Console.ReadLine();
                            if (name == null || name == string.Empty)
                                name = user.Name;
                            Console.Write($"Forneca seu novo email (atual = {user.Email}, se nao desejar alterar aperte apenas ENTER): ");
                            var email = Console.ReadLine();
                            if (email == null || email == string.Empty)
                                email = user.Email;
                            string? pwd = null;
                            Console.Write($"Forneca sua nova senha: ");
                            pwd = Console.ReadLine();
                            while (pwd == null || pwd == string.Empty)
                            {
                                Console.Write("Senha invalida, forneca alguma senha: ");
                                pwd = Console.ReadLine();
                            }
                            
                            User updated_user = new User(name, email, pwd);
                            updated_user.IdUser = user.IdUser;
                            _user.Update(updated_user);

                            user = updated_user;
                            _log.RegisterOperation(user, DbOperation.Update);

                            Console.WriteLine("Seu usuario foi atualizado com os novos dados!\n\n");
                            break;
                        case "3":
                            Console.Write("Seu usuario sera deletado, para confirmar digite sua senha: ");
                            var pwd_conf = Console.ReadLine();
                            User check_user = new User(user.Name, user.Email, pwd_conf);
                            if (user.Equals(check_user))
                            {
                                _user.Delete(user.IdUser);
                                logged_in = false;
                                _log.RegisterOperation(user, DbOperation.Delete);
                                user = new User();
                                Console.WriteLine("Usuario deletado com sucesso\n\n");
                            }
                            else
                            {
                                Console.WriteLine("Operacao cancelada\n\n");
                            }
                            break;
                        case "4":
                            if (user.IdUser != 1)
                            {
                                Console.WriteLine("Operacao invalida");
                                break;
                            }
                            _log.RegisterOperation(user, DbOperation.ListAll);
                            Console.WriteLine("Listando todos usuarios do banco:\n");
                            var userList = _user.ReadAll();
                            Console.WriteLine($"ID\t| Nome\t| Email\n{tableBreak}");
                            foreach (var item in userList)
                            {
                                Console.WriteLine($"{item.IdUser}\t| {item.Name}\t| {item.Email}");
                            }
                            Console.WriteLine("\n");

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
                    Console.WriteLine("2 - Cadastrar novo usuario");
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
                                _log.RegisterOperation(user, DbOperation.Login);
                                logged_in = true;

                                Console.WriteLine("Login efetuado com sucesso!\n\n");
                            }
                            catch (Exception e)
                            {
                                //Console.WriteLine(e.ToString());
                                Console.WriteLine("Credenciais invalidas\n\n");
                            }

                            break;
                        case "2":
                            Console.Write("Forneca seu nome: ");
                            var c_name = Console.ReadLine();
                            Console.Write("Forneca seu email: ");
                            var c_email = Console.ReadLine();
                            Console.Write("Forneca uma senha: ");
                            var c_pwd = Console.ReadLine();
                            
                            User new_user = new User(c_name, c_email, c_pwd);
                            user = _user.Create(new_user);

                            if (user.IdUser != 0)
                            {
                                _log.RegisterOperation(user, DbOperation.Creation);
                                _log.RegisterOperation(user, DbOperation.Login);
                                logged_in = true;
                                Console.WriteLine("Usuario cadastrado com sucesso!\n\n");
                            }
                            else
                            {
                                Console.WriteLine("Falha no cadastro do usuario\n\n");
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