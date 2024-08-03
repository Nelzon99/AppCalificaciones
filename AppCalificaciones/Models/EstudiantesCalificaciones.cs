using System.ComponentModel.DataAnnotations;

namespace AppCalificaciones.Models
{
    public class EstudiantesCalificaciones
    {
        [Key]
        public int Id { get; set; }
        public int NotaExamen1 { get; set; }
        public int NotaExamen2 { get; set; }
        public int NotaExamen3 { get; set; }
        public int NotaExamen4 { get; set; }
        public int NotaAcumulativo { get; set; }
        public int NotaOroExamen1 { get; set; }
        public int NotaOroExamen2 { get; set; }
        public int NotaOroExamen3 { get; set; }
        public int NotaOroExamen4 { get; set; }
        public int NotaOroAcumulativo { get; set; }
        public int NotaFinal { get; set; }
        public string NombreEstudiante { get; set; }
        public string NombreMateria { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
