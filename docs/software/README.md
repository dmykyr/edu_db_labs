# Реалізація інформаційного та програмного забезпечення

В рамках проєкту розробляється: 
## SQL-скрипт для створення на початкового наповнення бази даних

```sql
-- -----------------------------------------------------
-- Schema project_db
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `project_db`;
USE `project_db` ;

-- -----------------------------------------------------
-- Table `project_db`.`roles`
-- -----------------------------------------------------

DROP TABLE IF EXISTS `project_db`.`roles` ;

CREATE TABLE IF NOT EXISTS `project_db`.`roles` (
   `Id` INT NOT NULL AUTO_INCREMENT,
   `Name` VARCHAR(30) NULL DEFAULT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE INDEX `Id` (`Id` ASC) VISIBLE,
    UNIQUE INDEX `Name` (`Name` ASC) VISIBLE);

-- -----------------------------------------------------
-- Table `project_db`.`users`
-- -----------------------------------------------------

DROP TABLE IF EXISTS `project_db`.`users` ;

CREATE TABLE IF NOT EXISTS `project_db`.`users` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Login` VARCHAR(45) NOT NULL,
    `Picture` MEDIUMBLOB NOT NULL,
    `Password` BLOB NOT NULL,
    `Email` VARCHAR(50) NOT NULL,
    `Role` VARCHAR(30) NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE INDEX `Id` (`Id` ASC) VISIBLE,
    UNIQUE INDEX `Login` (`Login` ASC) VISIBLE,
    UNIQUE INDEX `Email` (`Email` ASC) VISIBLE);


-- -----------------------------------------------------
-- Table `project_db`.`members`
-- -----------------------------------------------------

DROP TABLE IF EXISTS `project_db`.`members` ;

CREATE TABLE IF NOT EXISTS `project_db`.`members` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `RoleId` INT NOT NULL,
    `UserId` INT NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE INDEX `Id` (`Id` ASC) VISIBLE,
    INDEX `roleFK_idx` (`RoleId` ASC) VISIBLE,
    INDEX `userFK_idx` (`UserId` ASC) VISIBLE,
    CONSTRAINT `roleFK`
    FOREIGN KEY (`RoleId`)
    REFERENCES `project_db`.`roles` (`Id`),
    CONSTRAINT `userFK`
    FOREIGN KEY (`UserId`)
    REFERENCES `project_db`.`users` (`Id`));

-- -----------------------------------------------------
-- Table `project_db`.`projects`
-- -----------------------------------------------------

DROP TABLE IF EXISTS `project_db`.`projects` ;

CREATE TABLE IF NOT EXISTS `project_db`.`projects` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50) NOT NULL,
    `Description` VARCHAR(100) NOT NULL,
    `Status` VARCHAR(20) NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE INDEX `Id` (`Id` ASC) VISIBLE);

-- -----------------------------------------------------
-- Table `project_db`.`payments`
-- -----------------------------------------------------

DROP TABLE IF EXISTS `project_db`.`payments` ;

CREATE TABLE IF NOT EXISTS `project_db`.`payments` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `CardNumber` INT NOT NULL,
    `CardCVV` INT NOT NULL,
    `CardExpireDate` DATETIME NOT NULL,
    `Email` VARCHAR(50) NOT NULL,
    `ProjectId` INT NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE INDEX `Id` (`Id` ASC) VISIBLE,
    INDEX `ProjectId` (`ProjectId` ASC) VISIBLE,
    CONSTRAINT `payments_ibfk_1`
    FOREIGN KEY (`ProjectId`)
    REFERENCES `project_db`.`projects` (`Id`));

-- -----------------------------------------------------
-- Table `project_db`.`permissions`
-- -----------------------------------------------------

DROP TABLE IF EXISTS `project_db`.`permissions` ;

CREATE TABLE IF NOT EXISTS `project_db`.`permissions` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Permission` VARCHAR(50) NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE INDEX `Id` (`Id` ASC) VISIBLE);


-- -----------------------------------------------------
-- Table `project_db`.`projects_members`
-- -----------------------------------------------------

DROP TABLE IF EXISTS `project_db`.`projects_members` ;

CREATE TABLE IF NOT EXISTS `project_db`.`projects_members` (
    `MemberId` INT NOT NULL,
    `ProjectId` INT NOT NULL,
    INDEX `ProjectId` (`ProjectId` ASC) VISIBLE,
    INDEX `MemberId` (`MemberId` ASC) VISIBLE,
    CONSTRAINT `projects_members_ibfk_1`
    FOREIGN KEY (`ProjectId`)
    REFERENCES `project_db`.`projects` (`Id`),
    CONSTRAINT `projects_members_ibfk_2`
    FOREIGN KEY (`MemberId`)
    REFERENCES `project_db`.`members` (`Id`));

-- -----------------------------------------------------
-- Table `project_db`.`reviews`
-- -----------------------------------------------------

DROP TABLE IF EXISTS `project_db`.`reviews` ;

CREATE TABLE IF NOT EXISTS `project_db`.`reviews` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Text` VARCHAR(100) NOT NULL,
    `Rate` INT NOT NULL,
    `ProjectId` INT NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE INDEX `Id` (`Id` ASC) VISIBLE,
    INDEX `ProjectId` (`ProjectId` ASC) VISIBLE,
    CONSTRAINT `reviews_ibfk_1`
    FOREIGN KEY (`ProjectId`)
    REFERENCES `project_db`.`projects` (`Id`));

-- -----------------------------------------------------
-- Table `project_db`.`role_grant`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `project_db`.`role_grant` ;

CREATE TABLE IF NOT EXISTS `project_db`.`role_grant` (
   `RoleId` INT NOT NULL,
   `PermissionId` INT NOT NULL,
    INDEX `RoleId` (`RoleId` ASC) VISIBLE,
    INDEX `PermissionId` (`PermissionId` ASC) VISIBLE,
    CONSTRAINT `role_grant_ibfk_1`
    FOREIGN KEY (`RoleId`)
    REFERENCES `project_db`.`roles` (`Id`),
    CONSTRAINT `role_grant_ibfk_2`
    FOREIGN KEY (`PermissionId`)
    REFERENCES `project_db`.`permissions` (`Id`));


-- -----------------------------------------------------
-- Table `project_db`.`tasks`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `project_db`.`tasks` ;

CREATE TABLE IF NOT EXISTS `project_db`.`tasks` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50) NOT NULL,
    `Developer` VARCHAR(45) NOT NULL,
    `Status` VARCHAR(20) NOT NULL,
    `Deadline` DATETIME NOT NULL,
    `ProjectId` INT NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE INDEX `Id` (`Id` ASC) VISIBLE,
    INDEX `ProjectId` (`ProjectId` ASC) VISIBLE,
    CONSTRAINT `tasks_ibfk_1`
    FOREIGN KEY (`ProjectId`)
    REFERENCES `project_db`.`projects` (`Id`));

-- Inserting data into the roles table
INSERT INTO `project_db`.`roles` (`Name`) VALUES
('Teamlead'),
('Developer'),
('Admin');

-- Inserting data into the users table
INSERT INTO `project_db`.`users` (`Login`, `Picture`, `Password`, `Email`, `Role`) VALUES
('admin_user', 'admin_picture_blob', 'admin_password_blob', 'admin@example.com', 'Teamlead'),
('dev_user1', 'dev_picture_blob1', 'dev_password_blob1', 'dev1@example.com', 'Developer'),
('dev_user2', 'dev_picture_blob2', 'dev_password_blob2', 'dev2@example.com', 'Developer'),
('manager_user', 'manager_picture_blob', 'manager_password_blob', 'manager@example.com', 'Admin');

-- Inserting data into the members table
INSERT INTO `project_db`.`members` (`RoleId`, `UserId`) VALUES
(1, 1),
(2, 2),
(2, 3),
(3, 4);

-- Inserting data into the projects table
INSERT INTO `project_db`.`projects` (`Name`, `Description`, `Status`) VALUES
('Project A', 'Description for Project A', 'Active'),
('Project B', 'Description for Project B', 'Inactive'),
('Project C', 'Description for Project C', 'Pending');

-- Inserting data into the permissions table
INSERT INTO `project_db`.`permissions` (`Permission`) VALUES
('Read'),
('Write'),
('Execute');

-- Inserting data into the role_grant table
INSERT INTO `project_db`.`role_grant` (`RoleId`, `PermissionId`) VALUES
(1, 1),
(1, 2),
(2, 1),
(2, 3),
(3, 2),
(3, 3);
-- Inserting data into the payments table
INSERT INTO `project_db`.`payments` (`Id`, `CardNumber`, `CardCVV`, `CardExpireDate`, `Email`, `ProjectId`) VALUES
(1, 123456, 123, '2023-12-31', 'payment1@example.com', 1),
(2, 987654, 456, '2023-11-30', 'payment2@example.com', 2),
(3, 111122, 789, '2023-10-31', 'payment3@example.com', 3);

-- Inserting data into the projects_members table
INSERT INTO `project_db`.`projects_members` (`MemberId`, `ProjectId`) VALUES
(1, 1),
(2, 1),
(3, 2),
(4, 3);

-- Inserting data into the reviews table
INSERT INTO `project_db`.`reviews` (`Text`, `Rate`, `ProjectId`) VALUES
('Good project!', 5, 1),
('Could be better', 3, 2),
('Excellent work', 5, 3);

-- Inserting data into the tasks table
INSERT INTO `project_db`.`tasks` (`Name`, `Developer`, `Status`, `Deadline`, `ProjectId`) VALUES
('Task 1', 'dev_user1', 'In Progress', '2023-11-15', 1),
('Task 2', 'dev_user2', 'To Do', '2023-12-01', 2),
('Task 3', 'dev_user1', 'Completed', '2023-10-31', 3);


```

## RESTfull сервіс для управління даними

### Моделі бази даних


```cs
public partial class Member
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int UserId { get; set; }

    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}

public partial class Payment
{
    public int Id { get; set; }

    public int CardNumber { get; set; }

    public int CardCvv { get; set; }

    public DateTime CardExpireDate { get; set; }

    public string Email { get; set; } = null!;

    public int ProjectId { get; set; }

    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}

public partial class Permission
{
    public int Id { get; set; }

    public string Permission1 { get; set; } = null!;
}

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}

public partial class ProjectsMember
{
    public int MemberId { get; set; }

    public int ProjectId { get; set; }

    [ForeignKey("MemberId")]
    public virtual Member Member { get; set; } = null!;

    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}

public partial class Review
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int Rate { get; set; }

    public int ProjectId { get; set; }

    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}

public partial class RoleGrant
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    [ForeignKey("PermissionId")]
    public virtual Permission Permission { get; set; } = null!;

    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; } = null!;
}

public partial class Task
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Developer { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime Deadline { get; set; }

    public int ProjectId { get; set; }

    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public byte[] Picture { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
```

### Контекст бази даних

```cs
public partial class ProjectDbContext : DbContext
{
    public ProjectDbContext()
    {
    }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectsMember> ProjectsMembers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleGrant> RoleGrants { get; set; }

    public virtual DbSet<Models.Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user=root;password=zxcfvb05D;database=project_db", ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("members");

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.HasIndex(e => e.RoleId, "roleFK_idx");

            entity.HasIndex(e => e.UserId, "userFK_idx");

            entity.HasOne(d => d.Role).WithMany(p => p.Members)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roleFK");

            entity.HasOne(d => d.User).WithMany(p => p.Members)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userFK");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payments");

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.HasIndex(e => e.ProjectId, "ProjectId");

            entity.Property(e => e.CardCvv).HasColumnName("CardCVV");
            entity.Property(e => e.CardExpireDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);

            entity.HasOne(d => d.Project).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_ibfk_1");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.Property(e => e.Permission1)
                .HasMaxLength(50)
                .HasColumnName("Permission");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("projects");

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<ProjectsMember>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("projects_members");

            entity.HasIndex(e => e.MemberId, "MemberId");

            entity.HasIndex(e => e.ProjectId, "ProjectId");

            entity.HasOne(d => d.Member).WithMany()
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("projects_members_ibfk_2");

            entity.HasOne(d => d.Project).WithMany()
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("projects_members_ibfk_1");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reviews");

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.HasIndex(e => e.ProjectId, "ProjectId");

            entity.Property(e => e.Text).HasMaxLength(100);

            entity.HasOne(d => d.Project).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<RoleGrant>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("role_grant");

            entity.HasIndex(e => e.PermissionId, "PermissionId");

            entity.HasIndex(e => e.RoleId, "RoleId");

            entity.HasOne(d => d.Permission).WithMany()
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_grant_ibfk_2");

            entity.HasOne(d => d.Role).WithMany()
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_grant_ibfk_1");
        });

        modelBuilder.Entity<Models.Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tasks");

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.HasIndex(e => e.ProjectId, "ProjectId");

            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.Developer).HasMaxLength(45);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tasks_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.HasIndex(e => e.Login, "Login").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(45);
            entity.Property(e => e.Password).HasColumnType("blob");
            entity.Property(e => e.Picture).HasColumnType("mediumblob");
            entity.Property(e => e.Role).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
```

### Вхідний файл програми 
```cs
public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("MainConnectionString")
                ?? throw new InvalidOperationException("Connection string 'ProjectDbContext' not found.");

            builder.Services.AddDbContext<ProjectDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            builder.Services.AddSingleton<IConfigurationRoot>(option => {
                return configuration;
            });

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
```

###  реалізація CRUD 

#### ProjectsController

```cs
[Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public ProjectsController(ProjectDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Retrieves all projects
        /// </summary>
        /// <response code="200"> Project list </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetProjects()
        {
            try
            {
                var projects = await System.Threading.Tasks.Task.Run(() =>
                    _context.Projects
                        .Select(ProjectResponse.ConvertFromModel)
                        .ToList()
                );

                return projects;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Retrieves a specific project by unique id
        /// </summary>
        /// <response code="200"> Specific Project </response>
        /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet("{projectId:int}")]
        public async Task<ActionResult<ProjectResponse>> GetProject(int projectId)
        {
            try
            {
                var project = await _context.Projects.FirstAsync(p => p.Id == projectId);

                if (project == null)
                {
                    return BadRequest("InvalidIndexError: Project with such Id does not exist");
                }

                return ProjectResponse.ConvertFromModel(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Update specific values for project by unique id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     [
        ///       {
        ///         "op":"replace",
        ///         "path":"/{parameter name}",
        ///         "value": "{parameter value}"
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200"> An updated Project </response>
        /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPatch("{projectId:int}")]
        public async Task<ActionResult<ProjectResponse>> PatchProject(
            int projectId, 
            [FromBody] JsonPatchDocument<Project> patchDoc)
        {
            try
            {
                var project = await _context.Projects.FirstAsync(p => p.Id == projectId);

                if (project == null)
                {
                    return BadRequest("InvalidIndexError: Project with such Id does not exist");
                }

                patchDoc.ApplyTo(project, ModelState);
                await _context.SaveChangesAsync();

                return RedirectToAction("GetProject", new { projectId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Add Project to db
        /// </summary>
        /// <response code="200"> A newly created Project </response>
        /// <response code="400">
        ///     DuplicateProjectNameError: project with such name already exists&#xA;
        ///     InvalidIndexError: Project with such Id does not exist
        /// </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPost]
        public async Task<ActionResult<ProjectResponse>> CreateProject([FromBody] ProjectDTO projectDTO)
        {
            try
            {
                if (await _context.Projects.FirstOrDefaultAsync(p => p.Name == projectDTO.Name) != null)
                {
                    return BadRequest("DuplicateProjectNameError: project with such name already exists");
                }

                var entityEntry = _context.Projects.Add(projectDTO.ToModel());
                await _context.SaveChangesAsync();

                return RedirectToAction("GetProject", new { projectId = entityEntry.Entity.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Remove Project from db by unique id
        /// </summary>
        /// <response code="200"> Just removed Project </response>
        /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpDelete("{projectId:int}")]
        public async Task<ActionResult<ProjectResponse>> DeleteProject(int projectId)
        {
            try
            {
                var project = await _context.Projects
                    .Include(p => p.Tasks)
                    .Include(p => p.Payments)
                    .Include(p => p.Reviews)
                    .FirstAsync(p => p.Id == projectId);

                if (project == null)
                {
                    return BadRequest("InvalidIndexError: Project with such Id does not exist");
                }

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();

                return ProjectResponse.ConvertFromModel(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Retrieves reviews related to specific project
        /// </summary>
        /// <response code="200"> List of reviews </response>
        /// <response code="400">
        ///     InvalidIndexError: Project with such Id does not exist&#xA;
        ///     Selected project is not finished
        /// </response>
        /// <response code="500"> Internal Server Error </response>
        
        [HttpGet("{projectId:int}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetProjectReviews(int projectId)
        {
            try
            {
                var project = await _context.Projects
                        .Include(p => p.Reviews)
                        .FirstOrDefaultAsync(p => p.Id == projectId);
                if (project == null)
                {
                    return BadRequest("InvalidIndexError: Project with such Id does not exist");
                }

                if (project.Status != "Finished")
                {
                    return BadRequest("Selected project is not finished");
                }

                return project.Reviews.Select(ReviewResponse.ConvertFromModel).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Add review to specific project
        /// </summary>
        /// <response code="200"> List of reviews </response>
        /// <response code="400">
        ///     InvalidIndexError: Project with such Id does not exist&#xA;
        ///     InvalidIndexError: Project id in review and parameter are different&#xA;
        ///     Selected project is not finished
        /// </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPost("{projectId:int}/reviews")]
        public async Task<ActionResult<IEnumerable<Review>>> AddReview(
            int projectId, 
            [FromBody] ReviewDTO reviewDTO)
        {
            try
            {
                var project = await _context.Projects.FindAsync(projectId);

                if (project == null) return BadRequest("InvalidIndexError: Project with such Id does not exist");

                if (project.Status != "Finished") return BadRequest("Selected project is not finished");

                _context.Reviews.Add(reviewDTO.ToModel());
                await _context.SaveChangesAsync();

                return RedirectToAction("GetProjectReviews", new { projectId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
```

#### Response для проєкту

```cs
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
```

#### DTO для створення проєкту 
```cs
public class ProjectDTO
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Status { get; set; } = null!;

    public Project ToModel() =>  new Project { Name = Name, Description = Description, Status = Status };
}
```

#### Response для відгуку

```cs
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
```

#### DTO для створення відгуку

```cs
public class ReviewDTO
{
    public string Text { get; set; } = null!;

    public int Rate { get; set; }

    public Review ToModel() => new Review() { Text = Text, Rate = Rate };
}
```

#### TaskController 

```cs
[Route("api/tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ProjectDbContext _context;

    public TasksController(ProjectDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Retrieves tasks related to specific project
    /// </summary>
    /// <response code="200"> List of tasks </response>
    /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
    /// <response code="500"> Internal Server Error </response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResponse>>> GetTasks(
        [FromQuery(Name = "projectId")] int projectId)
    {
        try
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null)
            {
                return BadRequest("InvalidIndexError: Project with such Id does not exist");
            }

            return project.Tasks.Select(TaskResponse.ConvertFromModel).ToList();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    /// <summary>
    ///     Retrieves specific task by unique id
    /// </summary>
    /// <response code="200"> Specific Task </response>
    /// <response code="400">  InvalidIndexError: Task with such Id does not exist </response>
    /// <response code="500"> Internal Server Error </response>
    [HttpGet("{taskId:int}")]
    public async Task<ActionResult<TaskResponse>> GetTask(int taskId)
    {
        try
        {
            var task = await _context.Tasks.FirstAsync(t => t.Id == taskId);

            if (task == null) return BadRequest("InvalidIndexError: Task with such Id does not exist");

            return TaskResponse.ConvertFromModel(task);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    /// <summary>
    ///     Update specific values for task by unique id
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     [
    ///       {
    ///         "op":"replace",
    ///         "path":"/{parameter name}",
    ///         "value": "{parameter value}"
    ///       }
    ///     ]
    ///     
    /// </remarks>
    /// <response code="200"> An updated Task </response>
    /// <response code="400"> InvalidIndexError: Task with such Id does not exist </response>
    /// <response code="500"> Internal Server Error </response>
    [HttpPatch("{taskId:int}")]
    public async Task<ActionResult<TaskResponse>> PatchTask(
        int taskId,
        [FromBody] JsonPatchDocument<Models.Task> patchDoc)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task == null)
        {
            return BadRequest("InvalidIndexError: Task with such Id does not exist");
        }

        patchDoc.ApplyTo(task, ModelState);
        _context.SaveChanges();

        return RedirectToAction("GetTask", new { taskId });
    }

    /// <summary>
    ///     Add Task to db
    /// </summary>
    /// <response code="200"> A newly created Task </response>
    /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
    /// <response code="500"> Internal Server Error </response>
    [HttpPost]
    public async Task<ActionResult<TaskResponse>> CreateTask(
        [FromQuery] int projectId,
        [FromBody] TaskDTO taskDTO)
    {
        try
        {
            if (!await _context.Projects.AnyAsync(p => p.Id == projectId))
            {
                return BadRequest("InvalidIndexError: Project with such Id does not exist");
            }

            var task = taskDTO.ToModel();
            task.ProjectId = projectId;

            var entityEntry = _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetTask", new { taskId = entityEntry.Entity.Id });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    /// <summary>
    ///     Remove Task from db by unique id
    /// </summary>
    /// <response code="200"> Just removed Task </response>
    /// <response code="400"> InvalidIndexError: Task with such Id does not exist </response>
    /// <response code="500"> Internal Server Error </response>
    [HttpDelete("{taskId:int}")]
    public async Task<ActionResult<TaskResponse>> DeleteTask(int taskId)
    {
        try
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
            {
                return BadRequest("InvalidIndexError: Task with such Id does not exist");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return TaskResponse.ConvertFromModel(task);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
```

#### Response для завдання

```cs
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
```

#### DTO для створення завдання

```cs
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
```

#### UsersController

```cs
[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ProjectDbContext _context;

    public UsersController(ProjectDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Retrieves all users related to a specific Project
    /// </summary>
    /// <response code="200"> Users list </response>
    /// <response code="400"> InvalidIndexError: Project with such Id does not exist </response>
    /// <response code="500"> Internal Server Error </response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers([FromQuery] int projectId)
    {
        try
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return BadRequest($"InvalidIndexError: Project with such Id does not exist");
            }

            return (await GetProjectUsers(project.Id)).Select(UserResponse.ConvertFromModel).ToList();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    /// <summary>
    ///     Retrieves specific User by unique Id
    /// </summary>
    /// <response code="200"> Specific User </response>
    /// <response code="400"> InvalidIndexError: User with such Id does not exist </response>
    /// <response code="500"> Internal Server Error </response>
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserResponse>> GetUser(int userId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return BadRequest($"InvalidIndexError: User with such Id does not exist");
            }

            return UserResponse.ConvertFromModel(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    /// <summary>
    ///     Update specific values for user by unique id
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     [
    ///       {
    ///         "op":"replace",
    ///         "path":"/{parameter name}",
    ///         "value": "{parameter value}"
    ///       }
    ///     ]
    ///     
    /// </remarks>
    /// <response code="200"> An updated User </response>
    /// <response code="400"> InvalidIndexError: User with such Id does not exist </response>
    /// <response code="500"> Internal Server Error </response>
    [HttpPatch("{userId:int}")]
    public async Task<ActionResult<UserResponse>> PatchUser(
        int userId,
        [FromBody] JsonPatchDocument<User> patchDoc)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return BadRequest("InvalidIndexError: User with such Id does not exist");
            }

            patchDoc.ApplyTo(user, ModelState);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetUser", new { userId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    /// <summary>
    ///     Connect user to specific project
    /// </summary>
    /// <response code="201"> User connected successfully. Member Id: {memberId} </response>
    /// <response code="400">
    ///     InvalidIndexError: User with such Id does not exist&#xA;
    ///     InvalidIndexError: Project with such name does not exist&#xA;
    ///     DuplicateProjectMember: User with such Id already connected to selected project&#xA;
    ///     InvalidNameError: Role with such name does not exist
    /// </response>
    /// <response code="500"> Internal Server Error </response>
    [HttpPost("{userId:int}/projects/{projectId:int}")]
    public async Task<IActionResult> PostUser(
        [FromQuery] string roleName,
        int userId,
        int projectId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("InvalidIndexError: User with such Id does not exist");
            }

            var role = await _context.Roles.FirstAsync(r => r.Name == roleName);
            if (role == null)
            {
                return BadRequest("InvalidNameError: Role with such name does not exist");
            }

            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return BadRequest("InvalidIndexError: Project with such name does not exist");
            }

            var projectUsers = await GetProjectUsers(projectId);
            if (projectUsers.Any(pu => pu.Members.Any(m => m.RoleId == role.Id))) 
            {
                return BadRequest("DuplicateProjectMember: User with such Id already connected to selected project");
            }

            var addedEntityEntry = await _context.Members
                .AddAsync(new Member() { RoleId = role.Id, UserId = userId });
            var member = addedEntityEntry.Entity;

            await _context.SaveChangesAsync();


            return Created($"User connected successfully. Member Id: {member.Id}", member);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    /// <summary>
    ///     Remove User from db by unique id
    /// </summary>
    /// <response code="200"> Just removed User </response>
    /// <response code="400"> InvalidIndexError: User with such Id does not exist </response>
    /// <response code="500"> Internal Server Error </response>
    [HttpDelete("{userId:int}")]
    public async Task<ActionResult<UserResponse>> DeleteUser(int userId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return BadRequest("InvalidIndexError: User with such Id does not exist");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return UserResponse.ConvertFromModel(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    private async Task<List<User>> GetProjectUsers(int projectId)
    {
        return await _context.Users
            .Include(u => u.Members)
            .Where(u => _context.ProjectsMembers
                .Where(pm => pm.ProjectId == projectId)
                .Any(pm => u.Members.Any(m => m.Id == pm.MemberId)))
            .ToListAsync();
    }
}
```

#### Response для користувача

```cs
public class UserResponse
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public byte[] Picture { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public static UserResponse ConvertFromModel(User user)
    {
        return new UserResponse()
        {
            Id = user.Id,
            Login = user.Login,
            Picture = user.Picture,
            Password = user.Password,
            Email = user.Email,
            Role = user.Role
        };
    }
}
```