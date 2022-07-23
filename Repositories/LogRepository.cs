using leonardo_benetti_d3_avaliacao.Interfaces;
using leonardo_benetti_d3_avaliacao.Model;

namespace leonardo_benetti_d3_avaliacao.Repositories
{
    internal class LogRepository : ILog
    {
        private const string path = "database/log.txt";

        public LogRepository()
        {
            CreateFolderAndFile(path);
        }

        private static string PrepareLoginLine(User user)
        {
            var datetime = DateTime.Now;
            var time = datetime.ToString("HH:mm:ss.ff");
            var date = datetime.ToString("dd/MM/yyyy");
            return $"[{date}] O usuario {user.Name} ({user.IdUser}) logou no sistema as {time}.";
        }

        private static string PrepareLogoutLine(User user)
        {
            var datetime = DateTime.Now;
            var time = datetime.ToString("HH:mm:ss.ff");
            var date = datetime.ToString("dd/MM/yyyy");
            return $"[{date}] O usuario {user.Name} ({user.IdUser}) deslogou do sistema as {time}.";
        }

        private static string PrepareCreationLine(User user)
        {
            var datetime = DateTime.Now;
            var time = datetime.ToString("HH:mm:ss.ff");
            var date = datetime.ToString("dd/MM/yyyy");
            return $"[{date}] O usuario {user.Name} ({user.IdUser}) foi adicionado no sistema as {time}.";
        }

        private static string PrepareUpdateLine(User user)
        {
            var datetime = DateTime.Now;
            var time = datetime.ToString("HH:mm:ss.ff");
            var date = datetime.ToString("dd/MM/yyyy");
            string return_string = $"[{date}] O usuario de ID {user.IdUser} alterou seus dados as {time}.\n";
            return_string += $"    nome atual : {user.Name}\n";
            return_string += $"    email atual: {user.Email}";
            return return_string;
        }

        private static string PrepareDeleteLine(User user)
        {
            var datetime = DateTime.Now;
            var time = datetime.ToString("HH:mm:ss.ff");
            var date = datetime.ToString("dd/MM/yyyy");
            return $"[{date}] O usuario {user.Name} ({user.IdUser}) esta sendo deletado do sistema as {time}.";
        }

        private static string PrepareListAllLine(User user)
        {
            var datetime = DateTime.Now;
            var time = datetime.ToString("HH:mm:ss.ff");
            var date = datetime.ToString("dd/MM/yyyy");
            return $"[{date}] O usuario {user.Name} ({user.IdUser}) esta listando os usuarios do sistema as {time}.";
        }

        public static void CreateFolderAndFile(string path)
        {
            string folder = path.Split("/")[0];
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public void RegisterOperation(User user, DbOperation operation)
        {
            string line = string.Empty;
            switch (operation)
            {
                case DbOperation.Login:
                    line = PrepareLoginLine(user);
                    break;
                case DbOperation.Logout:
                    line = PrepareLogoutLine(user);
                    break;
                case DbOperation.Creation:
                    line = PrepareCreationLine(user);
                    break;
                case DbOperation.Update:
                    line = PrepareUpdateLine(user);
                    break;
                case DbOperation.Delete:
                    line = PrepareDeleteLine(user);
                    break;
                case DbOperation.ListAll:
                    line = PrepareListAllLine(user);
                    break;
                default:
                    break;
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(line);
            }
        }
    }
}
