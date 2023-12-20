using DbREstService.Models;

namespace DbREstService.Responses
{
    public class ReviewResponse
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int Rate { get; set; }

        public static ReviewResponse ConvertFromModel(Review review)
        {
            return new ReviewResponse() { Id = review.Id, Text = review.Text, Rate = review.Rate };
        }
    }
}