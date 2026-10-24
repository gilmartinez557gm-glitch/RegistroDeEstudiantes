using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace RegistroDeEstudiantes;

public partial class MainPage : ContentPage
{
    private readonly List<Estudiante> estudiantes = new();

    public MainPage()
    {
        InitializeComponent();
    }

    public class Estudiante
    {
        public string NumeroCuenta { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Edad { get; set; }
        public string Sexo { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Carrera { get; set; } = "";
        public DateTime FechaRegistro { get; set; }
    }

    private void OnTemaToggled(object? sender, ToggledEventArgs e)
    {
        Application.Current.UserAppTheme =
            e.Value ? AppTheme.Dark : AppTheme.Light;
    }

    private async void OnGuardarEstudianteClicked(object? sender, EventArgs e)
    {
        List<string> errores = new();

        string numeroCuenta = NumeroCuentaEntry.Text?.Trim() ?? "";
        string nombre = NombreCompletoEntry.Text?.Trim() ?? "";
        string edadTexto = EdadEntry.Text?.Trim() ?? "";
        string correo = CorreoEntry.Text?.Trim() ?? "";
        string telefono = TelefonoEntry.Text?.Trim() ?? "";
        string direccion = DireccionEntry.Text?.Trim() ?? "";

        // Número de cuenta
        if (string.IsNullOrWhiteSpace(numeroCuenta))
        {
            errores.Add("Ingrese el número de cuenta.");
        }

        // Nombre completo
        if (string.IsNullOrWhiteSpace(nombre))
        {
            errores.Add("Ingrese el nombre completo.");
        }
        else if (nombre.Length < 3)
        {
            errores.Add("El nombre debe tener al menos 3 caracteres.");
        }

        // Edad
        if (string.IsNullOrWhiteSpace(edadTexto))
        {
            errores.Add("Ingrese la edad.");
        }
        else if (!int.TryParse(edadTexto, out int edad))
        {
            errores.Add("La edad debe ser un número entero.");
        }
        else if (edad < 16 || edad > 100)
        {
            errores.Add("La edad debe estar entre 16 y 100 años.");
        }

        // Sexo
        if (SexoPicker.SelectedIndex == -1)
        {
            errores.Add("Seleccione el sexo.");
        }

        // Correo electrónico
        if (string.IsNullOrWhiteSpace(correo))
        {
            errores.Add("Ingrese el correo electrónico.");
        }
        else
        {
            try
            {
                var direccionCorreo = new MailAddress(correo);

                if (direccionCorreo.Address != correo)
                {
                    errores.Add("Ingrese un correo electrónico válido.");
                }
            }
            catch (FormatException)
            {
                errores.Add("Ingrese un correo electrónico válido.");
            }
        }

        // Teléfono
        string telefonoNumerico =
            new string(telefono.Where(char.IsDigit).ToArray());

        if (string.IsNullOrWhiteSpace(telefono))
        {
            errores.Add("Ingrese el teléfono.");
        }
        else if (telefonoNumerico.Length < 8)
        {
            errores.Add("El teléfono debe contener al menos 8 dígitos.");
        }

        // Carrera
        if (CarreraPicker.SelectedIndex == -1)
        {
            errores.Add("Seleccione una carrera.");
        }

        // Mostrar errores en pantalla
        if (errores.Count > 0)
        {
            ErroresLabel.Text =
                "Por favor, corrija los siguientes errores:\n• "
                + string.Join("\n• ", errores);

            ErroresLabel.IsVisible = true;
            return;
        }

        // Ocultar errores si todo es válido
        ErroresLabel.Text = "";
        ErroresLabel.IsVisible = false;

        // Crear el objeto del estudiante
        var estudiante = new Estudiante
        {
            NumeroCuenta = numeroCuenta,
            Nombre = nombre,
            Edad = int.Parse(edadTexto),
            Sexo = SexoPicker.SelectedItem.ToString(),
            Correo = correo,
            Telefono = telefono,
            Direccion = direccion,
            Carrera = CarreraPicker.SelectedItem.ToString(),
            FechaRegistro = FechaRegistroPicker.Date ?? DateTime.Today
        };

        // Evitar números de cuenta duplicados
        if (estudiantes.Any(e =>
            e.NumeroCuenta.Equals(
                numeroCuenta,
                StringComparison.OrdinalIgnoreCase)))
        {
            ErroresLabel.Text = "Ya existe un estudiante con ese número de cuenta.";
            ErroresLabel.IsVisible = true;
            return;
        }

        // Guardar en la lista
        estudiantes.Add(estudiante);
        // Resumen de los datos
        string resumen =
            $"Número de cuenta: {numeroCuenta}\n" +
            $"Nombre: {nombre}\n" +
            $"Edad: {edadTexto} años\n" +
            $"Sexo: {SexoPicker.SelectedItem}\n" +
            $"Correo: {correo}\n" +
            $"Teléfono: {telefono}\n" +
            $"Dirección: {(string.IsNullOrWhiteSpace(direccion) ? "No indicada" : direccion)}\n" +
            $"Carrera: {CarreraPicker.SelectedItem}\n" +
            $"Fecha de registro: {FechaRegistroPicker.Date:dd/MM/yyyy}";

        await DisplayAlertAsync(
             "Registro de estudiante",
             resumen,
              "Aceptar");
    }

    private async void OnVerEstudiantesClicked(object? sender, EventArgs e)
    {
        if (estudiantes.Count == 0)
        {
            await DisplayAlertAsync(
                "Estudiantes registrados",
                "Todavía no hay estudiantes guardados.",
                "Aceptar");

            return;
        }

        string resumen = "";

        foreach (var estudiante in estudiantes)
        {
            resumen +=
                $"Cuenta: {estudiante.NumeroCuenta}\n" +
                $"Nombre: {estudiante.Nombre}\n" +
                $"Edad: {estudiante.Edad}\n" +
                $"Sexo: {estudiante.Sexo}\n" +
                $"Correo: {estudiante.Correo}\n" +
                $"Teléfono: {estudiante.Telefono}\n" +
                $"Carrera: {estudiante.Carrera}\n" +
                "-------------------------\n";
        }

        await DisplayAlertAsync(
            "Estudiantes registrados",
            resumen,
            "Cerrar");
    }
}
