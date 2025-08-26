namespace IyiOlusAdminPanel.Models.Questions
{
    public class Question
    {
        public Guid Id { get; set; }
        public string ProfileQuestion { get; set; } = default!;
        public QuestionTypes QuestionType { get; set; }
        public Guid ProfileTypeId { get; set; }
        public ProfileType ProfileType { get; set; } = default!;

    }
}
