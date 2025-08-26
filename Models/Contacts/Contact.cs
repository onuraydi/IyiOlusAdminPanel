using IyiOlusAdminPanel.Models.Users;

namespace IyiOlusAdminPanel.Models.Contacts
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Subject { get; set; } = default!;
        public string Message { get; set; } = default!;
        public bool isRead { get; set; }
        public Guid UserId { get; set; }
        public UserResponse UserResponse { get; set; } = default!;
    }
}
