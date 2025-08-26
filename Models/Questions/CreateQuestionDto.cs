namespace IyiOlusAdminPanel.Models.Questions
{
    public class CreateQuestionDto
    {
        public string ProfileQuestion { get; set; } = default!;
        public QuestionTypes QuestionType { get; set; }
        public Guid ProfileTypeId { get; set; }
    }
}
