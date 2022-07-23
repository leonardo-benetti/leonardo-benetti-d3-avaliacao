using leonardo_benetti_d3_avaliacao.Model;

namespace leonardo_benetti_d3_avaliacao.Interfaces
{
    internal interface IUser
    {
        List<User> ReadAll();

        User Create(User user);

        void Update(User user);

        void Delete(int idUser);
    }
}
