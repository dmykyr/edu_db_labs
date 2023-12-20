using DbREstService.Models;

namespace DbREstService.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Login { get; set; } = null!;

        public byte[] Picture { get; set; } = null!;

        public byte[] Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public static UserResponse ConvertFromModel(User user)
        {
            return new UserResponse()
            {
                Id = user.Id,
                Login = user.Login,
                Picture = user.Picture,
                Password = user.Password,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}