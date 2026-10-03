using SQLite;

namespace RegistroDeEstudiantes.Models;

public class Estudiante
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    public string NumeroCuenta { get; set; } = string.Empty;

    public string NombreCompleto { get; set; } = string.Empty;

    public int Edad { get; set; }

    public string Sexo { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Direccion { get; set; } = string.Empty;

    public string Carrera { get; set; } = string.Empty;

    public DateTime FechaRegistro { get; set; }
}