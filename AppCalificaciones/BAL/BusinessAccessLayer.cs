using System;
using System.Text.RegularExpressions;
using AppCalificaciones.Interfaces;
using AppCalificaciones.Models;

namespace AppCalificaciones.BAL
{
    public class BusinessAccessLayer : IBusinessAccessLayer
    {
        // Función para validar si una cadena es un nombre de materia válido
        public bool EsNombreMateriaValido(string entrada)
        {
            // Expresión regular para validar que la cadena solo contenga letras, números y un espacio
            // La expresión permite letras en español (incluyendo acentos), números y un solo espacio entre palabras.
            string patron = @"^[a-zA-ZáéíóúüñÁÉÍÓÚÜÑ0-9]+( [a-zA-ZáéíóúüñÁÉÍÓÚÜÑ0-9]+)*$";

            return Regex.IsMatch(entrada, patron);
        }

        // Función para solicitar el nombre de la materia
        public string SolicitarNombreMateria()
        {
            string entrada;
            while (true)
            {
                Console.WriteLine("Ingrese el nombre de la materia:");
                entrada = Console.ReadLine();

                if (EsNombreMateriaValido(entrada))
                {
                    return entrada.Trim();
                }
                else
                {
                    Console.WriteLine("\n¡El nombre de materia no es válida! Asegúrese de que solo contenga letras, números y solo un espacio seguido.");
                }
            }
        }

        // Función para validar si una cadena es un número entero
        public bool EsNumeroEntero(string valor)
        {
            return int.TryParse(valor, out _);
        }

        // Función para solicitar la cantidad de estudiantes
        public int SolicitarCantidadEstudiantes()
        {
            int cantidad;
            while (true)
            {
                Console.WriteLine("Ingrese la cantidad de estudiantes:");
                string entrada = Console.ReadLine();

                if (EsNumeroEntero(entrada))
                {
                    cantidad = Convert.ToInt32(entrada);
                    if (cantidad > 0)
                    {
                        return cantidad;
                    }
                    else
                    {
                        Console.WriteLine("\n¡Debe ingresar un número mayor a 0!");
                    }
                }
                else
                {
                    Console.WriteLine("\n¡Debe ingresar un número entero!");
                }
            }
        }

        // Función para validar si una cadena es un nombre de estudiante válido
        public bool EsNombreEstudianteValido(string entrada)
        {
            // Expresión regular para validar que la cadena solo contenga letras y un solo espacio seguido
            // La expresión permite letras en español (incluyendo acentos) y un solo espacio entre palabras.
            string patron = @"^[a-zA-ZáéíóúüñÁÉÍÓÚÜÑ]+( [a-zA-ZáéíóúüñÁÉÍÓÚÜÑ]+)*$";

            return Regex.IsMatch(entrada, patron);
        }

        // Función para solicitar el nombre del estudiante
        public string SolicitarNombreEstudiante(string mensaje)
        {
            string entrada;
            while (true)
            {
                Console.WriteLine(mensaje);
                entrada = Console.ReadLine();

                if (EsNombreEstudianteValido(entrada))
                {
                    return entrada.Trim();
                }
                else
                {
                    Console.WriteLine("\n¡La cadena ingresada no es válida! Asegúrese de que solo contenga letras y un solo espacio entre palabras.");
                }
            }
        }

        // Función para solicitar una nota y validar la entrada
        public int SolicitarNota(string mensaje)
        {
            int nota;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                if (EsNumeroEntero(entrada))
                {
                    nota = Convert.ToInt32(entrada);
                    if (nota >= 0 && nota <= 100)
                    {
                        return nota;
                    }
                    else
                    {
                        Console.WriteLine("\n¡La nota debe estar entre 0 y 100!");
                    }
                }
                else
                {
                    Console.WriteLine("\n¡Debe ingresar un número entero!");
                }
            }
        }

        // Función para calcular el porcentaje y redondear a entero
        public int ConvertirNotaAPuntosOro(int nota, double PuntosOro)
        {
            double porcentaje = nota * (PuntosOro / 100);
            return (int)(Math.Floor(porcentaje + 0.5));
        }

        // Función para crear el contenido en formato CSV
        public string CrearContenidoCSV(List<EstudiantesCalificaciones> lista)
        {
            // Definir los encabezados del CSV
            string[] encabezado = {
                "Fecha/Hora",
                "NombreMateria",
                "NombreEstudiante",
                "NotaOroExamen1",
                "NotaOroExamen2",
                "NotaOroExamen3",
                "NotaOroExamen4",
                "NotaOroAcumulativo",
                "NotaFinal"
            };

            // Crear la cadena con los encabezados
            string contenido = string.Join(",", encabezado) + Environment.NewLine;

            // Agregar los datos de la lista al contenido CSV
            foreach (var estudiante in lista)
            {
                string fila = string.Join(",",
                    estudiante.FechaHora.ToString("dd/MM/yyyy hh:mm:ss tt"),
                    estudiante.NombreMateria,
                    estudiante.NombreEstudiante,
                    estudiante.NotaOroExamen1,
                    estudiante.NotaOroExamen2,
                    estudiante.NotaOroExamen3,
                    estudiante.NotaOroExamen4,
                    estudiante.NotaOroAcumulativo,
                    estudiante.NotaFinal
                );
                contenido += fila + Environment.NewLine;
            }

            return contenido;
        }

        // Función para guardar el contenido en un archivo
        public void GuardarEnArchivo(string ruta, string contenido)
        {
            // Asegurarse de que el directorio existe
            Directory.CreateDirectory(Path.GetDirectoryName(ruta));

            // Escribir el contenido en el archivo con codificación UTF-8
            File.WriteAllText(ruta, contenido, System.Text.Encoding.UTF8);
        }
    }
}
