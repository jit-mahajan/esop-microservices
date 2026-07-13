
using System.Linq.Expressions;

namespace BasketAPI.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);

    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            //TODO : Get basket from database
            //var bucket = await _repository.GetBasket(request.UserName);

            return new GetBasketResult(new ShoppingCart("swn"));
        }
    }
}
