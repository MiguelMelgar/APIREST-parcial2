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

            // Configuraci�n de la base de datos
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configuraci�n de los controladores y JSON
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            // Configuraci�n de Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuraci�n de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PermitirTodo",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            var app = builder.Build();

            // Configuraci�n del entorno de desarrollo
     //       if (app.Environment.IsDevelopment())
         //   {
                app.UseSwagger();
                app.UseSwaggerUI();
          //  }

            // Configuraci�n de los middlewares
            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Uso de la pol�tica CORS configurada
            app.UseCors("PermitirTodo");

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
