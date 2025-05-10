namespace Domain.Entites
{
    public class Otp
    {
        public long Id { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public string Phonenumber { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString("N");
        public string Value { get; set; }
        public DateTime ExpireTime { get; set; } = DateTime.Now.AddMinutes(1);
    }
}
