

using Discount.Grpc;

namespace BasketAPI.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValiator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValiator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be Null.");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {

            await DeductDiscount(command.Cart, cancellationToken);
            await repository.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(command.Cart.UserName);
        }


        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName}, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
            
        }
    }

}