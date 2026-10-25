using System;
using System.IO;

try {
    Console.Write("Escribe cómo quieres que se llame la carpeta base: ");
    string carpetaBase = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(carpetaBase)) {
        throw new ArgumentException("El nombre de la carpeta no puede estar vacío ni ser nulo.");
    }

    Console.Write("Escribe el nombre del archivo (ej. registro.txt): ");
    string nombreArchivo = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(nombreArchivo)) {
        throw new ArgumentException("El nombre del archivo no puede estar vacío ni ser nulo.");
    }

    string rutaArchivo = Path.Combine(carpetaBase, nombreArchivo);


    try {
        Directory.CreateDirectory(carpetaBase);
        Console.WriteLine($"\nCarpeta lista en: {carpetaBase}\n");
    } catch (PathTooLongException) {
        Console.WriteLine("La ruta de la carpeta es demasiado larga.");
    } catch (DirectoryNotFoundException) {
        Console.WriteLine("La ruta especificada no existe o no es válida.");
    }

    bool salir = false;

    while (!salir) {
        Console.WriteLine("\n          MENÚ DE GESTIÓN DE FICHEROS             \n");
        Console.WriteLine("1. Crear un nuevo fichero con el nombre que el usuario le pase por consola.");
        Console.WriteLine("2. Añadir registros en el fichero.");
        Console.WriteLine("3. Eliminar todos los registros del fichero.");
        Console.WriteLine("4. Eliminar el fichero.");
        Console.WriteLine("5. Visualizar los registros.");
        Console.WriteLine("6. Cerrar la aplicación.\n");
        Console.Write("Selecciona una opción (1-6): ");

        string opcion = Console.ReadLine();

        switch (opcion) {
            case "1":

                try {
                    if (File.Exists(rutaArchivo)) {
                        Console.WriteLine($"El archivo '{rutaArchivo}' ya existía.");
                    } else {
                        using (FileStream archivo = File.Create(rutaArchivo)) { }
                        Console.WriteLine($"Archivo '{rutaArchivo}' creado exitosamente.");
                    }
                    Console.WriteLine($"Ruta completa: {Path.GetFullPath(rutaArchivo)}");
                } catch (PathTooLongException) {
                    Console.WriteLine("Error: La ruta del archivo supera el límite permitido.");
                } catch (UnauthorizedAccessException) {
                    Console.WriteLine("Error: No tienes permisos para crear el archivo.");
                } catch (IOException ex) {
                    Console.WriteLine("Error de E/S al crear el archivo: " + ex.Message);
                }
                break;

            case "2":
                try {
                    Console.Write("Introduce el registro a añadir: ");
                    string nuevoRegistro = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(nuevoRegistro)) {
                        throw new ArgumentException("No se puede añadir un registro vacío.");
                    }

                    File.AppendAllText(rutaArchivo, nuevoRegistro + Environment.NewLine);
                    Console.WriteLine("Registro añadido con éxito.");
                } catch (ArgumentException ex) {
                    Console.WriteLine("Error de validación: " + ex.Message);
                } catch (FileNotFoundException) {
                    Console.WriteLine("Error: El fichero no existe. Créalo primero usando la Opción 1.");
                } catch (IOException ex) {
                    Console.WriteLine("Error al escribir en el fichero: " + ex.Message);
                }
                break;

            case "3":
                try {
                    if (!File.Exists(rutaArchivo)) {
                        throw new FileNotFoundException();
                    }

                    File.WriteAllText(rutaArchivo, string.Empty);
                    Console.WriteLine($"Todos los registros de '{rutaArchivo}' han sido eliminados.");
                } catch (FileNotFoundException) {
                    Console.WriteLine("Error: El fichero no existe.");
                } catch (IOException ex) {
                    Console.WriteLine("Error al vaciar el fichero: " + ex.Message);
                }
                break;

            case "4":
                try {
                    if (!File.Exists(rutaArchivo)) {
                        throw new FileNotFoundException();
                    }

                    File.Delete(rutaArchivo);
                    Console.WriteLine($"El archivo '{rutaArchivo}' ha sido eliminado correctamente.");
                } catch (FileNotFoundException) {
                    Console.WriteLine("Error: El fichero no existe para ser eliminado.");
                } catch (UnauthorizedAccessException) {
                    Console.WriteLine("Error: Permiso denegado al intentar eliminar el archivo.");
                } catch (IOException ex) {
                    Console.WriteLine("Error al intentar eliminar el archivo: " + ex.Message);
                }
                break;

            case "5":
                try {
                    string[] lineas = File.ReadAllLines(rutaArchivo);

                    if (lineas.Length == 0) {
                        Console.WriteLine("El fichero está vacío.");
                    } else {
                        Console.WriteLine($"\n--- REGISTROS EN '{rutaArchivo}' ---");
                        int contador = 1;
                        foreach (string linea in lineas) {
                            Console.WriteLine($"{contador}. {linea}");
                            contador++;
                        }
                    }
                } catch (FileNotFoundException) {
                    Console.WriteLine("Error: El fichero no existe.");
                } catch (IOException ex) {
                    Console.WriteLine("Error al leer el fichero: " + ex.Message);
                }
                break;

            case "6":
                salir = true;
                Console.WriteLine("Cerrando la aplicación...");
                break;

            default:
                Console.WriteLine("Opción no válida. Por favor, elige un número del 1 al 6.");
                break;
        }
    }
}
catch (ArgumentException ex) {
    Console.WriteLine("Error de validación inicial: " + ex.Message);
} catch (NotSupportedException) {
    Console.WriteLine("Error: La ruta contiene caracteres no permitidos.");
} catch (UnauthorizedAccessException) {
    Console.WriteLine("Error: No tienes permisos suficientes para realizar estas operaciones.");
} catch (IOException ex) {
    Console.WriteLine("Error general de E/S: " + ex.Message);
} catch (Exception ex) {
    Console.WriteLine("Error inesperado: " + ex.Message);
}