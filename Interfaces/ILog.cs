using leonardo_benetti_d3_avaliacao.Model;

namespace leonardo_benetti_d3_avaliacao.Interfaces
{
    internal interface ILog
    {
        void RegisterLogin(User user);

        void RegisterLogout(User user);
    }
}
