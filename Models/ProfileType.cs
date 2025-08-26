using IyiOlusAdminPanel.Models.Questions;

namespace IyiOlusAdminPanel.Models
{
    public class ProfileType
    {
        public ProfileTypes Type { get; set; }
        public virtual ICollection<Question> Questions { get; set; } = default!;
    }
}
