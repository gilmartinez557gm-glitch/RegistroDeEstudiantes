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
        // VALIDAR NOMBRE

        if (string.IsNullOrWhiteSpace(NombreEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese el nombre del estudiante.",
                "Aceptar");

            return;
        }

        // VALIDAR APELLIDO

        if (string.IsNullOrWhiteSpace(ApellidoEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese el apellido del estudiante.",
                "Aceptar");

            return;
        }

        // VALIDAR EDAD

        if (!int.TryParse(
                EdadEntry.Text,
                out int edad) || edad <= 0)
        {
            await DisplayAlert(
                "Error",
                "Ingrese una edad válida.",
                "Aceptar");

            return;
        }

        // VALIDAR SEXO

        if (SexoPicker.SelectedIndex == -1)
        {
            await DisplayAlert(
                "Error",
                "Seleccione el sexo del estudiante.",
                "Aceptar");

            return;
        }

        // VALIDAR CORREO

        if (string.IsNullOrWhiteSpace(CorreoEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese el correo electrónico.",
                "Aceptar");

            return;
        }

        // VALIDAR TELEFONO

        if (string.IsNullOrWhiteSpace(TelefonoEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese el teléfono.",
                "Aceptar");

            return;
        }

        // VALIDAR DIRECCION

        if (string.IsNullOrWhiteSpace(DireccionEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese la dirección.",
                "Aceptar");

            return;
        }

        // VALIDAR CARRERA

        if (CarreraPicker.SelectedIndex == -1)
        {
            await DisplayAlert(
                "Error",
                "Seleccione la carrera.",
                "Aceptar");

            return;
        }

        // CREAR OBJETO ESTUDIANTE

        Estudiante estudiante = new Estudiante
        {
            Nombre = NombreEntry.Text.Trim(),

            Apellido = ApellidoEntry.Text.Trim(),

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

        // GUARDAR EN SQLITE

        await _databaseService.GuardarEstudianteAsync(
            estudiante);

        // MOSTRAR CONFIRMACIÓN

        await DisplayAlert(
            "Registro exitoso",
            $"El estudiante se guardó correctamente.\nID generado: {estudiante.ID}",
            "Aceptar");

        // LIMPIAR CAMPOS

        NombreEntry.Text = string.Empty;

        ApellidoEntry.Text = string.Empty;

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