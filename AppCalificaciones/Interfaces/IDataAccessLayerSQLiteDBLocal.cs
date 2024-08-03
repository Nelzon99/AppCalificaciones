using AppCalificaciones.Models;

namespace AppCalificaciones.Interfaces
{
    public interface IDataAccessLayerSQLiteDBLocal
    {
        void InsertNotas(List<EstudiantesCalificaciones> users);
    }
}
