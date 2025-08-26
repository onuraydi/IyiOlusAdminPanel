using Microsoft.CodeAnalysis;

namespace IyiOlusAdminPanel.Models.Users
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public bool Gender { get; set; }
        public virtual RegisterResponse RegisterResponse { get; set; } = default!;
    }
}
