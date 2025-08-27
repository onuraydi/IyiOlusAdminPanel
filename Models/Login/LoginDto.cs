using System.ComponentModel.DataAnnotations;

namespace IyiOlusAdminPanel.Models.Login
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        public bool RememberMe { get; set; }
    }
}
