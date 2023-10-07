using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI.Data
{
    public static class AppDbContextSeed
    {
        public static void SeedAsync(AppDbContext context)
        {
            if(!context.Coupons.Any())
            {
                var coupons = new List<Coupon>()
                {
                    new Coupon()
                    {
                        CouponId = 1,
                        CouponCode = "10OFF",
                        DiscountAmount = 10,
                        MinAmount = 20
                    },
                    new Coupon()
                    {
                        CouponId = 2,
                        CouponCode = "20OFF",
                        DiscountAmount = 20,
                        MinAmount = 40
                    }
                };
                context.Coupons.AddRange(coupons);
                context.SaveChanges();
            }
        }
    }
}
