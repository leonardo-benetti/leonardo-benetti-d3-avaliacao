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

        public void RegisterLogin(User user)
        {
            string line = PrepareLoginLine(user);
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(line);
            }
        }

        public void RegisterLogout(User user)
        {
            string line = PrepareLogoutLine(user);
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(line);
            }
        }
    }
}
