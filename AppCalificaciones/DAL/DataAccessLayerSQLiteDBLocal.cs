using System.Data.SQLite;
using AppCalificaciones.Interfaces;
using AppCalificaciones.Models;
using Microsoft.Extensions.Configuration;

namespace AppCalificaciones.DAL
{
    public class DataAccessLayerSQLiteDBLocal : IDataAccessLayerSQLiteDBLocal
    {
        private readonly string _connectionString;

        public DataAccessLayerSQLiteDBLocal()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) 
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Obtener la cadena de conexión desde la configuración
            _connectionString = configuration.GetConnectionString("SQLiteDB");
        }

        public DataAccessLayerSQLiteDBLocal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertNotas(List<EstudiantesCalificaciones> estudiantesCalificaciones)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    // Crear la tabla si no existe
                    string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS EstudiantesCalificaciones (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        NotaExamen1 INTEGER,
                        NotaExamen2 INTEGER,
                        NotaExamen3 INTEGER,
                        NotaExamen4 INTEGER,
                        NotaAcumulativo INTEGER,
                        NotaOroExamen1 INTEGER,
                        NotaOroExamen2 INTEGER,
                        NotaOroExamen3 INTEGER,
                        NotaOroExamen4 INTEGER,
                        NotaOroAcumulativo INTEGER,
                        NotaFinal INTEGER,
                        NombreEstudiante TEXT,
                        NombreMateria TEXT,
                        FechaHora TEXT
                    )";

                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Preparar el comando para insertar usuarios
                    string insertDataQuery = @"
                    INSERT INTO EstudiantesCalificaciones (
                        NotaExamen1, NotaExamen2, NotaExamen3, NotaExamen4, NotaAcumulativo,
                        NotaOroExamen1, NotaOroExamen2, NotaOroExamen3, NotaOroExamen4, NotaOroAcumulativo,
                        NotaFinal, NombreEstudiante, NombreMateria, FechaHora
                    ) VALUES (
                        @NotaExamen1, @NotaExamen2, @NotaExamen3, @NotaExamen4, @NotaAcumulativo,
                        @NotaOroExamen1, @NotaOroExamen2, @NotaOroExamen3, @NotaOroExamen4, @NotaOroAcumulativo,
                        @NotaFinal, @NombreEstudiante, @NombreMateria, @FechaHora
                    )";

                    using (var command = new SQLiteCommand(insertDataQuery, connection))
                    {
                        // Añadir los parámetros
                        command.Parameters.Add(new SQLiteParameter("@NotaExamen1"));
                        command.Parameters.Add(new SQLiteParameter("@NotaExamen2"));
                        command.Parameters.Add(new SQLiteParameter("@NotaExamen3"));
                        command.Parameters.Add(new SQLiteParameter("@NotaExamen4"));
                        command.Parameters.Add(new SQLiteParameter("@NotaAcumulativo"));
                        command.Parameters.Add(new SQLiteParameter("@NotaOroExamen1"));
                        command.Parameters.Add(new SQLiteParameter("@NotaOroExamen2"));
                        command.Parameters.Add(new SQLiteParameter("@NotaOroExamen3"));
                        command.Parameters.Add(new SQLiteParameter("@NotaOroExamen4"));
                        command.Parameters.Add(new SQLiteParameter("@NotaOroAcumulativo"));
                        command.Parameters.Add(new SQLiteParameter("@NotaFinal"));
                        command.Parameters.Add(new SQLiteParameter("@NombreEstudiante"));
                        command.Parameters.Add(new SQLiteParameter("@NombreMateria"));
                        command.Parameters.Add(new SQLiteParameter("@FechaHora"));

                        // Iniciar una transacción para mejorar el rendimiento
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                foreach (var estudiante in estudiantesCalificaciones)
                                {
                                    // Asignar los valores de los parámetros
                                    command.Parameters["@NotaExamen1"].Value = estudiante.NotaExamen1;
                                    command.Parameters["@NotaExamen2"].Value = estudiante.NotaExamen2;
                                    command.Parameters["@NotaExamen3"].Value = estudiante.NotaExamen3;
                                    command.Parameters["@NotaExamen4"].Value = estudiante.NotaExamen4;
                                    command.Parameters["@NotaAcumulativo"].Value = estudiante.NotaAcumulativo;
                                    command.Parameters["@NotaOroExamen1"].Value = estudiante.NotaOroExamen1;
                                    command.Parameters["@NotaOroExamen2"].Value = estudiante.NotaOroExamen2;
                                    command.Parameters["@NotaOroExamen3"].Value = estudiante.NotaOroExamen3;
                                    command.Parameters["@NotaOroExamen4"].Value = estudiante.NotaOroExamen4;
                                    command.Parameters["@NotaOroAcumulativo"].Value = estudiante.NotaOroAcumulativo;
                                    command.Parameters["@NotaFinal"].Value = estudiante.NotaFinal;
                                    command.Parameters["@NombreEstudiante"].Value = estudiante.NombreEstudiante;
                                    command.Parameters["@NombreMateria"].Value = estudiante.NombreMateria;
                                    command.Parameters["@FechaHora"].Value = estudiante.FechaHora.ToString("dd/MM/yyyy hh:mm:ss tt");

                                    // Ejecutar el comando
                                    command.ExecuteNonQuery();
                                }

                                // Confirmar la transacción
                                transaction.Commit();
                                Console.WriteLine($"\nNúmero total de registros insertados en base de datos: {estudiantesCalificaciones.Count}");
                            }
                            catch (Exception ex)
                            {
                                // En caso de error, revertir la transacción
                                transaction.Rollback();
                                Console.WriteLine($"\nError al insertar datos: {ex.Message}");
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al insertar datos: {ex.Message}");
            }

        }


    }
}
