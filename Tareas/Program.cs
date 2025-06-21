// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        HttpClient client = new HttpClient();
        string url = "https://jsonplaceholder.typicode.com/todos";

        try
        {
            // Obtener datos de la API
            var response = await client.GetStringAsync(url);

            // Deserializar a lista de objetos Tarea
            var tareas = JsonSerializer.Deserialize<List<Tarea>>(response);

            if (tareas != null)
            {
                // Mostrar tareas pendientes
                Console.WriteLine("=== TAREAS PENDIENTES ===");
                foreach (var tarea in tareas.Where(t => !t.Completed))
                {
                    Console.WriteLine($"Título: {tarea.Title} | Estado: PENDIENTE");
                }

                // Mostrar tareas completadas
                Console.WriteLine("\n=== TAREAS COMPLETADAS ===");
                foreach (var tarea in tareas.Where(t => t.Completed))
                {
                    Console.WriteLine($"Título: {tarea.Title} | Estado: COMPLETADA");
                }

                // Guardar todo en archivo tareas.json
                string jsonResultado = JsonSerializer.Serialize(tareas, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("tareas.json", jsonResultado);

                Console.WriteLine("\nArchivo 'tareas.json' guardado correctamente.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}