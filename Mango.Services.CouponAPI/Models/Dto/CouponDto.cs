using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models.Dto
{
    public class CouponDto : CreateCouponDto
    {
        [Key]
        public int CouponID { get; set; }
    }
    public class CreateCouponDto
    {

        [Required]
        public string CouponCode { get; set; }
        [Required]
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }


    }

}
