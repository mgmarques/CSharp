using GameShopping.Email.Messages;
using System.Threading.Tasks;

namespace GameShopping.Email.Repository
{
    public interface IEmailRepository
    {
        Task LogEmail(UpdatePaymentResultMessage message);
    }
}
