namespace IyiOlusAdminPanel.Models.Questions
{
    public class UpdateQuestionDto
    {
        public Guid Id { get; set; }
        public string ProfileQuestion { get; set; } = default!;
        public QuestionTypes QuestionType { get; set; }
    }
}
