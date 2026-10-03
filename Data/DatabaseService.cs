using SQLite;
using RegistroDeEstudiantes.Models;

namespace RegistroDeEstudiantes.Data;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService()
    {
        string dbPath = Path.Combine(
            FileSystem.AppDataDirectory,
            "estudiantes.db3");

        _database = new SQLiteAsyncConnection(dbPath);
    }

    public async Task InitializeAsync()
    {
        await _database.CreateTableAsync<Estudiante>();
    }

    public async Task<int> GuardarEstudianteAsync(
        Estudiante estudiante)
    {
        return await _database.InsertAsync(estudiante);
    }

    public async Task<List<Estudiante>> ObtenerEstudiantesAsync()
    {
        return await _database
            .Table<Estudiante>()
            .ToListAsync();
    }
}