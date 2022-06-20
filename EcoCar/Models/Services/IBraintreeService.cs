// MainAuthors : Farah&FrancoisNoel
using Braintree;

namespace EcoCar.Models.Services
{
    public interface IBraintreeService
    {
        IBraintreeGateway CreateGateway();
        IBraintreeGateway GetGateway();
    }
}
