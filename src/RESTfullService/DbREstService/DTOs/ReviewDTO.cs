using DbREstService.Models;

namespace DbREstService.DTO
{
    public class ReviewDTO
    {
        public string Text { get; set; } = null!;

        public int Rate { get; set; }

        public Review ToModel() => new Review() { Text = Text, Rate = Rate };
    }
}