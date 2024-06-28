using BE_task.Interfaces;
using BE_task.Middlewares;
using BE_task.Services;

namespace BE_task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<IPluginManager, PluginManager>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(s =>
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "BE task.xml");

                s.IncludeXmlComments(filePath, true);
                s.CustomSchemaIds((type) => type.Name.Replace("DTO", string.Empty));
            });

            var app = builder.Build();

            app.UseExceptionMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
