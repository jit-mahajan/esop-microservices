using FluentValidation;

namespace BasketAPI.Basket.StoreBasket
{
    public record StoreBasketCommand (ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(bool Issuccess);

    public class StoreBasketCommandValiator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValiator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be Null.");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
    public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;

            //TODO Store basket in database (use Marten upsert - if exist = update)
            //TODO update Cache


            return new StoreBasketResult(true);
        }
    }
}
