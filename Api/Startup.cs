using EFDataAccess.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Api", Version = "v1" });
			});

			services.AddDbContext<PeopleContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("Default"));
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint(url: "v1/swagger.json", name: "Api v1");
			});

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			EnsureSeedData(app);
		}

		private void EnsureSeedData(IApplicationBuilder appBuilder)
		{
			using var serviceScope = appBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

			using var applicationContext = serviceScope.ServiceProvider.GetRequiredService<PeopleContext>();
			applicationContext.Database.Migrate();
		}
	}
}
