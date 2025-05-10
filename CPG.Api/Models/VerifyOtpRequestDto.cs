using System.ComponentModel.DataAnnotations;

namespace CPG.Api.Models
{
    public class VerifyOtpRequestDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string Phonenumnber { get; set; }
    }
}
