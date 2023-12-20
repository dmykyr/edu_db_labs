namespace DbREstService.Responses
{
    public class TaskResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Developer { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime Deadline { get; set; }

        public static TaskResponse ConvertFromModel(Models.Task task)
        {
            return new TaskResponse
            {
                Id = task.Id,
                Name = task.Name,
                Developer = task.Developer,
                Status = task.Status,
                Deadline = task.Deadline
            };
        }
    }
}
