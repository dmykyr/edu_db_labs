namespace DbREstService.DTOs
{
    public class TaskDTO
    {
        public string Name { get; set; } = null!;

        public string Developer { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime Deadline { get; set; }

        public Models.Task ToModel() => new Models.Task
        {
            Name = Name,
            Developer = Developer,
            Status = Status,
            Deadline = Deadline
        };
    }
}
