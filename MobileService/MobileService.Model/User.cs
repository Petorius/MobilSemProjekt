namespace MobileService.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string HashPassword { get; set; }
        public string Salt { get; set; }
    }
}
