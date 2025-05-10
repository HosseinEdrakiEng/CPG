using System.ComponentModel.DataAnnotations;

namespace CPG.Api.Models
{
    public class CreatePurchaseRequestDto
    {
        [Required]
        public string OrderId { get; set; }

        [Required]
        public long Amount { get; set; }

        [Required]
        public string CallbackUrl { get; set; }

        [Required]
        public string Phonenumber { get; set; }

        [Required]
        public string NationalCode { get; set; }
    }
}
