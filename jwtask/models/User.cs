using System.ComponentModel;

namespace jwtask.models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        [DefaultValue("User")]
        public string Role { get; set; }
    }

    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
