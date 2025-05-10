using System.ComponentModel.DataAnnotations;

namespace CPG.Api.Models
{
    public class SendOtpRequestDto
    {
        [Required]
        public string Phonenumnber { get; set; }
    }
}
