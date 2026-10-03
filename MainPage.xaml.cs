using RegistroDeEstudiantes.Data;
using RegistroDeEstudiantes.Models;

namespace RegistroDeEstudiantes;

public partial class MainPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public MainPage()
    {
        InitializeComponent();

        _databaseService = new DatabaseService();

        FechaRegistroPicker.Date = DateTime.Today;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _databaseService.InitializeAsync();
    }

    private async void OnGuardarEstudianteClicked(
        object sender,
        EventArgs e)
    {
        // Validar número de cuenta
        if (string.IsNullOrWhiteSpace(NumeroCuentaEntry.Text))
        {
            await DisplayAlert("Error", "Ingrese el número de cuenta.", "Aceptar");
            return;
        }

        // Validar nombre completo
        if (string.IsNullOrWhiteSpace(NombreCompletoEntry.Text))
        {
            await DisplayAlert("Error", "Ingrese el nombre completo del estudiante.", "Aceptar");
            return;
        }

        // Validar edad
        if (!int.TryParse(
                EdadEntry.Text,
                out int edad) || edad <= 0)
        {
            await DisplayAlert( "Error", "Ingrese una edad válida.", "Aceptar");
            return;
        }

        // Validar sexo
        if (SexoPicker.SelectedIndex == -1)
        {
            await DisplayAlert(
                "Error",
                "Seleccione el sexo del estudiante.",
                "Aceptar");
            return;
        }

        // Validar correo
        if (string.IsNullOrWhiteSpace(CorreoEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese el correo electrónico.",
                "Aceptar");
            return;
        }

        // Validar teléfono
        if (string.IsNullOrWhiteSpace(TelefonoEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese el teléfono.",
                "Aceptar");
            return;
        }

        // Validar dirección
        if (string.IsNullOrWhiteSpace(DireccionEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese la dirección.",
                "Aceptar");
            return;
        }

        // Validar carrera
        if (CarreraPicker.SelectedIndex == -1)
        {
            await DisplayAlert(
                "Error",
                "Seleccione la carrera.",
                "Aceptar");
            return;
        }

        // Crear estudiante
        Estudiante estudiante = new Estudiante
        {
            NumeroCuenta = NumeroCuentaEntry.Text.Trim(),

            NombreCompleto = NombreCompletoEntry.Text.Trim(),

            Edad = edad,

            Sexo = SexoPicker.SelectedItem?.ToString()
                   ?? string.Empty,

            Correo = CorreoEntry.Text.Trim(),

            Telefono = TelefonoEntry.Text.Trim(),

            Direccion = DireccionEntry.Text.Trim(),

            Carrera = CarreraPicker.SelectedItem?.ToString()
                      ?? string.Empty,

            FechaRegistro = FechaRegistroPicker.Date ?? DateTime.Today,
        };

        // Guardar en SQLite
        await _databaseService.GuardarEstudianteAsync(
            estudiante);

        await DisplayAlert(
            "Registro exitoso",
            $"El estudiante se guardó correctamente.\nID generado: {estudiante.ID}",
            "Aceptar");

        // Limpiar formulario
        NumeroCuentaEntry.Text = string.Empty;

        NombreCompletoEntry.Text = string.Empty;

        EdadEntry.Text = string.Empty;

        SexoPicker.SelectedIndex = -1;

        CorreoEntry.Text = string.Empty;

        TelefonoEntry.Text = string.Empty;

        DireccionEntry.Text = string.Empty;

        CarreraPicker.SelectedIndex = -1;

        FechaRegistroPicker.Date = DateTime.Today;
    }

    private async void OnVerEstudiantesClicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PushAsync(
            new Views.EstudiantesPage());
    }
}