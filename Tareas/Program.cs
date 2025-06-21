// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Text.Json;
using Tareas;
Console.WriteLine("\nConsumo de API.");

HttpClient client = new HttpClient();

string url = "https://jsonplaceholder.typicode.com/todos/";
HttpResponseMessage response = await client.GetAsync(url);
response.EnsureSuccessStatusCode();

string responseBody = await response.Content.ReadAsStringAsync();
List<Tarea> tareas = JsonSerializer.Deserialize<List<Tarea>>(responseBody);
Console.WriteLine("\n=== TAREAS NO COMPLETADAS ===");
foreach (var tarea in tareas)
{
    if (!tarea.completed)
    {
        Console.WriteLine($"TITULO: {tarea.title}  |ESTADO: {tarea.completed}");
    }
}

List<Tarea> tareasCompletadas = new List<Tarea>();
Console.WriteLine("\n=== TAREAS COMPLETADAS ===");
foreach (var tarea in tareas)
{
    //Tarea nuevaTarea = new Tarea(tarea.userId, tarea.id, tarea.title, tarea.completed);
    if (tarea.completed)
    {
        Tarea nuevaTarea = new Tarea { userId = tarea.id, id = tarea.id, title = tarea.title, completed = tarea.completed };
        tareasCompletadas.Add(nuevaTarea);
        Console.WriteLine($"TITULO: {tarea.title}  |ESTADO: {tarea.completed}");
    }
}
string jsonString = JsonSerializer.Serialize<List<Tarea>>(tareasCompletadas);

// Crear archivo
string nombreArchivo = "tareas.json";
string ruta = "C:\\Users\\Val\\tl1-tp10-2025-valencaceresm\\Tareas";
StreamWriter nuevoArchivo = new StreamWriter(ruta + nombreArchivo);
nuevoArchivo.WriteLine(jsonString);
nuevoArchivo.Close();