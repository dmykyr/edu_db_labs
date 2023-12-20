using DbREstService.Models;

namespace DbREstService.DTOs
{
    public class ProjectDTO
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Status { get; set; } = null!;

        public Project ToModel() =>  new Project { Name = Name, Description = Description, Status = Status };
    }
}