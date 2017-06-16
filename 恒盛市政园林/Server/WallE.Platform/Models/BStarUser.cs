namespace WallE.Platform.Models
{
    public class BStarUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public int UserRole { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RegisterTime { get; set; }
        public string LoginTime { get; set; }
        public int Status { get; set; }
    }
}
