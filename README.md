<div>
	<div>
		<img src=https://raw.githubusercontent.com/Byron2016/00_forImages/main/images/Logo_01_00.png align=left alt=MyLogo width=200>
	</div>
	&nbsp;
	<div>
		<h1>065_01_CSharpOnionArch</h1>
	</div>
</div>

&nbsp;

## Project Description

**SolutionApplication** is a practice ot the using of an **Onion Clean Architecture**. following Fernando DevTeam504's tutorial [Aplicando Patrón Repositorio en .NET Core](https://www.youtube.com/watch?v=bstBdEjfuZ0).
&nbsp;

## Stepts
	
1. Create these new projects with these caracteristics:
	- Projects Name: WebAPI
	- Solution Name: SolutionApplication
	- Framework: .NET 6.0 (Long-term support) 
	- Authentication type: None
	- Configure for HTTPS: true
	- Use controllers (uncheck to use minimal APIs): true
	- Enable OpenAPI support: true
	- Do not use top-level-statements: true

2. Inside solution create a new **Class Library** projects with these caracteristics:
	- Projects Name: SolutionApplication.Database
	- Framework: .NET 6.0 (Long-term support)  
	
		- Add folders:
		```
		SolutionApplication.Database
		└─── Context 
		│
		└─── DbModels
	
		```
		
		- to **SolutionApplication.Database** add packages:
		```c#
		dotnet add package Microsoft.EntityFrameworkCore --version 6.0.9
		dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.9
		dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.9--version 6.1.0
		```
		Note: Microsoft.EntityFrameworkCore.Tools is used for migrations.
		
		- Add classes ApplicationDBContext
		```c#
		using Microsoft.EntityFrameworkCore;
		using SolutionApplication.Database.DbModels;
		
		namespace SolutionApplication.Database.Context
		{
			public  class ApplicationDBContext : DbContext
			{
				public DbSet<Speaker> Speakers { get; set; }
		
				protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
				{
					var connectionString = "Server=localhost;Database=db_bancoOnion_00;User Id=sa;Password=123456;";
					optionsBuilder.UseSqlServer(connectionString);
				}
			}
		}
		```
		
		- Add classes Speaker
		```c#
		using System.ComponentModel.DataAnnotations;
		
		namespace SolutionApplication.Database.DbModels
		{
			public class Speaker
			{
				[Key]
				public int SpeakerId { get; set; }
		
				[Required(ErrorMessage = "This field is required")]
				[StringLength(100, ErrorMessage = "Max length is 100 caracteres")]
				public string SpeakerName { get; set; }
			}
		}
		```
	
	- Projects Name: SolutionApplication.DTOs
	- Framework: .NET 6.0 (Long-term support)
	
		- Add folders:
		```
		SolutionApplication.DTOs
		└─── Models 
		│
		└─── 
	
		```
	
		- Add classes SpeakerDTO
		```c#
		namespace SolutionApplication.DTOs.Models
		{
			public class SpeakerDTO
			{
				public int SpeakerId { get; set; }
		
				public string SpeakerName { get; set; }
			}
		}
		```
		
		- Add classes SpeakerDTO
		```c#
		namespace SolutionApplication.DTOs.Models
		{
			public class SpeakerDTO
			{
				public int SpeakerId { get; set; }
		
				public string SpeakerName { get; set; }
			}
		}
		```
			
	- Projects Name: SolutionApplication.Repository
	- Framework: .NET 6.0 (Long-term support)
	
		- Add folders:
		```
		SolutionApplication.DTOs
		└─── Interface 
		│
		└─── Repository
	
		```
		
		- References
			- Add reference to SolutionApplication.DTOs
			- Add reference to SolutionApplication.Database
	
		- Add interface ISpeakerRepository
		```c#
		using SolutionApplication.DTOs.Models;
		
		namespace SolutionApplication.Repository.Interface
		{
			public interface ISpeakerRepository
			{
				Task<List<SpeakerDTO>> GetSpeakers();
			}
		}
		```
		
		- Add classes SpeakerDTO
		```c#
		using Microsoft.EntityFrameworkCore;
		using SolutionApplication.Database.Context;
		using SolutionApplication.Database.DbModels;
		using SolutionApplication.DTOs.Models;
		using SolutionApplication.Repository.Interface;
		
		namespace SolutionApplication.Repository.Repository
		{
			public class SpeakerRepository : ISpeakerRepository
			{
				private readonly ApplicationDBContext _context;
		
				public SpeakerRepository(ApplicationDBContext context)
				{
					_context = context;
				}
		
				public async Task<List<SpeakerDTO>> GetSpeakers()
				{
					List<Speaker> speakerFromDB = await _context.Speakers.ToListAsync();
		
					List<SpeakerDTO> speakerDTO = speakerFromDB.Select(speaker => new SpeakerDTO()
					{
						SpeakerId = speaker.SpeakerId,
						SpeakerName = speaker.SpeakerName
					}).ToList();
		
					return speakerDTO;
				}
			}
		}
		```
		
3. Front End
	- References
		- Add reference to SolutionApplication.DTOs
		- Add reference to SolutionApplication.Database
		
	- Controllers
		- Add a **API Controller**
		```c#
		using Microsoft.AspNetCore.Mvc;
		using SolutionApplication.DTOs.Models;
		using SolutionApplication.Repository.Interface;
		
		namespace WebAPI.Controllers
		{
			[Route("api/[controller]")]
			[ApiController]
			public class SpeakerController : ControllerBase
			{
				private readonly ISpeakerRepository _speakerRepository;
		
				public SpeakerController(ISpeakerRepository speakerRepository)
				{
					_speakerRepository = speakerRepository;
				}
		
				[HttpGet]
				public async Task<IActionResult> GetSpeakers()
				{
					List<SpeakerDTO> speakerFromRepository = await _speakerRepository.GetSpeakers();
		
					return Ok(speakerFromRepository);
				}
			}
		}
		```
		
	- Controllers
		- Add a **API Controller**
		```c#
		using SolutionApplication.Database.Context;
		using SolutionApplication.Repository.Interface;
		using SolutionApplication.Repository.Repository;
		
		namespace WebAPI
		{
			public class Program
			{
				public static void Main(string[] args)
				{
					var builder = WebApplication.CreateBuilder(args);
		
					builder.Services.AddDbContext<ApplicationDBContext>();
		
					builder.Services.AddScoped<ISpeakerRepository, SpeakerRepository>();
					
					....
				}
			}
		}
		```
	- Avoid migration error
		- ERROR: Your startup project 'WebAPI' doesn't reference Microsoft.EntityFrameworkCore.Design. This package is required for the Entity Framework Core Tools to work. Ensure your startup project is correct, install the package, and try again.
		```c#
		dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.9--version 6.1.0
		```
	- Ejecute migration
		- Go to Package Manager Console View and select SolutionApplication.Database project.
			- add-migration MyFirstMigration -o Migrations
				- Remove-migration 
				- Get-Migration
			- update-database -Verbose
			
	- Add GetSpeakersById method
	
		- Add interface ISpeakerRepository
		```c#
		using SolutionApplication.DTOs.Models;
		
		namespace SolutionApplication.Repository.Interface
		{
			public interface ISpeakerRepository
			{
				....
				Task<SpeakerDTO> GetSpeakersById(int id);
			}
		}
		```
		
		- Add classes SpeakerDTO
		```c#
		....
		
		namespace SolutionApplication.Repository.Repository
		{
			public class SpeakerRepository : ISpeakerRepository
			{
				....
		
				public async Task<SpeakerDTO?> GetSpeakersById(int id)
				{
					Speaker? speakerFromDB = await _context.Speakers.FirstOrDefaultAsync(sp => sp.SpeakerId == id);
		
					SpeakerDTO speakerDTO;
		
					if (speakerFromDB != null)
					{
						speakerDTO = new SpeakerDTO()
						{
							SpeakerId = speakerFromDB.SpeakerId,
							SpeakerName = speakerFromDB.SpeakerName
						};
		
						return speakerDTO;
					}
		
					return null;
				}
			}
		}
		```