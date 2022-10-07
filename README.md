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
	
	- Add classes ApplicationDBContext
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
	
	
	

	
	