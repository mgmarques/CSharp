// It a just a simple mock of the process
namespace GameShopping.PaymentProcessor
{
    public class ProcessPayment : IProcessPayment
    {
        public bool PaymentProcessor()
        {
            return true;
        }
    }
}
