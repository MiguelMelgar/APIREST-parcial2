using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCursos.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string CursoNombre { get; set; }
        public int Creditos { get; set; }
        public int HorasSemanal { get; set; }
        public int Ciclo { get; set; }

        [ForeignKey("Docente")]
        public int IdDocente { get; set; }

        public Docente? Docente { get; set; }
    }

}
