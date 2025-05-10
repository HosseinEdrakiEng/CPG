using Application.Common;
using System.ComponentModel.DataAnnotations;

namespace CPG.Api.Models
{
    public class CreatePurchaseDetailRequestDto
    {
        [Required]
        public long Amount { get; set; }

        [Required]
        public string TrackingCode { get; set; }

        [Required]
        public PurchaseDetailType Type { get; set; }
    }
}
