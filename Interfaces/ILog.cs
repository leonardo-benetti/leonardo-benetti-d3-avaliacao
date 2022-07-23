using leonardo_benetti_d3_avaliacao.Model;

namespace leonardo_benetti_d3_avaliacao.Interfaces
{
    internal enum DbOperation
    {
        Login, Logout, Creation, Update, Delete, ListAll
    }

    internal interface ILog
    {
        void RegisterOperation(User user, DbOperation operation);
    }
}
