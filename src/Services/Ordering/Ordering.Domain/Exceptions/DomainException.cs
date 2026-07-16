
namespace Ordering.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            :base($"Domail Excption:\"{message}\" throws from Domain Layer.")
        {
            
        }
    }
}
