using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService
        (DiscountContext dbContext, ILogger<DiscountService> logger) 
        : DiscountProtoService.DiscountProtoServiceBase
    {

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            logger.LogInformation("Received ProductName = '{ProductName}'", request.ProductName);
            var coupons = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupons is null)
                coupons = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discout Desc" };

            logger.LogInformation("Discount is retrived for ProductName : {productName}, Amount : {amount}", coupons.ProductName, coupons.Amount);

            var couponModel = coupons.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid requst object"));

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully created for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid requst object"));

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully updated for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product Name={request.ProductName} is not Found."));

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully deleted. ProductName : {productName}", coupon.ProductName);

            return new DeleteDiscountResponse { Success = true };

        }
    }
}
