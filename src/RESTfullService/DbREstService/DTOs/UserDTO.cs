using DbREstService.Models;

namespace DbREstService.DTOs
{
    public class UserDTO
    {
        public string Login { get; set; } = null!;

        public byte[] Picture { get; set; } = null!;

        public byte[] Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public User ToModel()
        {
            return new User()
            {
                Login = Login,
                Picture = Picture,
                Password = Password,
                Email = Email,
                Role = Role
            };
        }
    }
}
