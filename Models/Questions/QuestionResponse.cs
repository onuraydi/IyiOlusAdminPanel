namespace IyiOlusAdminPanel.Models.Questions
{
        public class QuestionResponse
        {
            public List<Question> Items { get; set; } = default!;
            public int pageNumber { get; set; }
            public int pageSize { get; set; }
            public int totalPages { get; set; }
            public int totalItems { get; set; }
            public bool hasPreviousPage { get; set; }
            public bool hasNextPage { get; set; }
        }
}
