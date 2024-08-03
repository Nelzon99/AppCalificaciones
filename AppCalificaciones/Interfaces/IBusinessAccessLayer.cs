using AppCalificaciones.Models;

namespace AppCalificaciones.Interfaces
{
    public interface IBusinessAccessLayer
    {
        bool EsNombreMateriaValido(string entrada);
        string SolicitarNombreMateria();
        bool EsNumeroEntero(string valor);
        int SolicitarCantidadEstudiantes();
        bool EsNombreEstudianteValido(string entrada);
        string SolicitarNombreEstudiante(string mensaje);
        int SolicitarNota(string mensaje);
        int ConvertirNotaAPuntosOro(int nota, double PuntosOro);
        string CrearContenidoCSV(List<EstudiantesCalificaciones> lista);
        void GuardarEnArchivo(string ruta, string contenido);
    }
}
