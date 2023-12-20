using DbREstService.Models;

namespace DbREstService.Responses
{
    public class ProjectResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Status { get; set; } = null!;

        public static ProjectResponse ConvertFromModel(Project project)
        {
            return new ProjectResponse()
            {
                Id = project.Id,
                Description = project.Description,
                Status = project.Status,
                Name = project.Name
            };
        }
    }
}
