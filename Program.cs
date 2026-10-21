using System.IO;

//Crear un nuevo fichero con el nombre que el usuario le pase por consola
Console.WriteLine("Escribe como quieres que se llame la carpeta: ");
string carpetaBase = Console.ReadLine();

Console.WriteLine("1. Añadir registros en el fichero");
Console.WriteLine("2. Eliminar todos los registros del fichero");
Console.WriteLine("3. Eliminar el fichero");
Console.WriteLine("4. Visualizar los registros");
Console.WriteLine("5. Cerrar la aplicación");