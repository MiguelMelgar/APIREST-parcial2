using ApiCursos.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ApiCursos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuración de la base de datos
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configuración de los controladores y JSON
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            // Configuración de Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuración de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PermitirTodo",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            var app = builder.Build();

            // Configuración del entorno de desarrollo
     //       if (app.Environment.IsDevelopment())
         //   {
                app.UseSwagger();
                app.UseSwaggerUI();
          //  }

            // Configuración de los middlewares
            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Uso de la política CORS configurada
            app.UseCors("PermitirTodo");

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
