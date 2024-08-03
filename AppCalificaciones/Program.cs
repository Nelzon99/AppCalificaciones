using AppCalificaciones.Models;
using AppCalificaciones.Interfaces;
using AppCalificaciones.DAL;
using AppCalificaciones.BAL;

class Program
{
    static void Main()
    {
        // Crear una instancia de BusinessAccessLayer
        IBusinessAccessLayer bal = new BusinessAccessLayer();

        // Crear una instancia de DataAccessLayerSQLiteDBLocal
        IDataAccessLayerSQLiteDBLocal dal = new DataAccessLayerSQLiteDBLocal();

        string nombreMateria = bal.SolicitarNombreMateria();
        
        Console.WriteLine();
        int cantidadEstudiantes = bal.SolicitarCantidadEstudiantes();

        List<EstudiantesCalificaciones> listaEstudiantesCalificaciones = new();
        EstudiantesCalificaciones estudianteCalificaciones;

        // Procesar cada estudiante
        for (int i = 1; i <= cantidadEstudiantes; i++)
        {
            Console.WriteLine();
            string nombreEstudiante = bal.SolicitarNombreEstudiante($"Ingrese el nombre del estudiante {i}:");

            Console.WriteLine($"\nIngrese las notas de {nombreEstudiante}:");

            int nota1 = bal.SolicitarNota("Nota del primer examen en base a 100: ");
            int nota2 = bal.SolicitarNota("Nota del segundo examen en base a 100: ");
            int nota3 = bal.SolicitarNota("Nota del tercer examen en base a 100: ");
            int nota4 = bal.SolicitarNota("Nota del cuarto examen en base a 100: ");
            int notaAcumulativo = bal.SolicitarNota("Nota de acumulativo en base a 100: ");

            int notaOro1 = bal.ConvertirNotaAPuntosOro(nota1, 15);
            int notaOro2 = bal.ConvertirNotaAPuntosOro(nota2, 15);
            int notaOro3 = bal.ConvertirNotaAPuntosOro(nota3, 15);
            int notaOro4 = bal.ConvertirNotaAPuntosOro(nota4, 15);
            int notaOroAcumulativo = bal.ConvertirNotaAPuntosOro(notaAcumulativo, 40);
            int notaFinal = notaOro1 + notaOro2 + notaOro3 + notaOro4 + notaOroAcumulativo;
            DateTime fechaHora = DateTime.Now;

            estudianteCalificaciones = new()
            {
                NotaExamen1 = nota1,
                NotaExamen2 = nota2,
                NotaExamen3 = nota3,
                NotaExamen4 = nota4,
                NotaAcumulativo = notaAcumulativo,
                FechaHora = fechaHora,
                NombreMateria = nombreMateria,
                NombreEstudiante = nombreEstudiante,
                NotaOroExamen1 = notaOro1,
                NotaOroExamen2 = notaOro2,
                NotaOroExamen3 = notaOro3,
                NotaOroExamen4 = notaOro4,
                NotaOroAcumulativo = notaOroAcumulativo,
                NotaFinal = notaFinal
            };

            listaEstudiantesCalificaciones.Add(estudianteCalificaciones);

            // Mostrar resumen de cálculos para el estudiante actual
            Console.WriteLine($"\nResumen de las notas de {nombreEstudiante}");
            Console.WriteLine($"Fecha y hora: {fechaHora.ToString("dd/MM/yyyy hh:mm:ss tt")}");
            Console.WriteLine($"Materia: {nombreMateria}");
            Console.WriteLine($"Nota oro del primer examen: {notaOro1}");
            Console.WriteLine($"Nota oro del segundo examen: {notaOro2}");
            Console.WriteLine($"Nota oro del tercer examen: {notaOro3}");
            Console.WriteLine($"Nota oro del cuarto examen: {notaOro4}");
            Console.WriteLine($"Nota oro acumulativo: {notaOroAcumulativo}");
            Console.WriteLine($"Nota final: {notaFinal}");
        }

        // Definir la ruta del archivo
        string rutaArchivo = @"C:\morazanfiles\notas.csv";

        // Crear el contenido CSV
        string contenidoCSV = bal.CrearContenidoCSV(listaEstudiantesCalificaciones);

        // Guardar el contenido en el archivo
        bal.GuardarEnArchivo(rutaArchivo, contenidoCSV);

        // Guarda en la base de datos
        dal.InsertNotas(listaEstudiantesCalificaciones);

        Console.WriteLine("\nArchivo CSV guardado en: " + rutaArchivo);

        // Pausar la ejecución para ver los resultados
        Console.ReadKey();
    }
}