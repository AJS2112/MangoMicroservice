using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> GetCouponByIdAsync(int couponId);
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteCouponAsync(int couponId);
    }
}
