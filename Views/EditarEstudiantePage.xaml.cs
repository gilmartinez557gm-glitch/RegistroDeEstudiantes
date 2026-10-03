using RegistroDeEstudiantes.Data;
using RegistroDeEstudiantes.Models;

namespace RegistroDeEstudiantes.Views;

public partial class EditarEstudiantePage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private readonly Estudiante _estudiante;

    public EditarEstudiantePage(Estudiante estudiante)
    {
        InitializeComponent();

        _databaseService = new DatabaseService();
        _estudiante = estudiante;

        CargarDatos();
    }

    private void CargarDatos()
    {
        NumeroCuentaEntry.Text = _estudiante.NumeroCuenta;

        NombreCompletoEntry.Text = _estudiante.NombreCompleto;

        EdadEntry.Text = _estudiante.Edad.ToString();

        SexoPicker.SelectedItem = _estudiante.Sexo;

        CorreoEntry.Text = _estudiante.Correo;

        TelefonoEntry.Text = _estudiante.Telefono;

        DireccionEntry.Text = _estudiante.Direccion;

        CarreraPicker.SelectedItem = _estudiante.Carrera;

        FechaRegistroPicker.Date = _estudiante.FechaRegistro;
    }

    private async void OnGuardarCambiosClicked(
        object sender,
        EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NumeroCuentaEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese el número de cuenta.",
                "Aceptar");
            return;
        }

        if (string.IsNullOrWhiteSpace(NombreCompletoEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese el nombre completo del estudiante.",
                "Aceptar");
            return;
        }

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

        if (SexoPicker.SelectedIndex == -1)
        {
            await DisplayAlert(
                "Error",
                "Seleccione el sexo del estudiante.",
                "Aceptar");
            return;
        }

        if (string.IsNullOrWhiteSpace(CorreoEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese el correo electrónico.",
                "Aceptar");
            return;
        }

        if (!CorreoEntry.Text.Contains("@") ||
            !CorreoEntry.Text.Contains("."))
        {
            await DisplayAlert(
                "Error",
                "Ingrese un correo electrónico válido.",
                "Aceptar");
            return;
        }

        if (string.IsNullOrWhiteSpace(TelefonoEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese el teléfono.",
                "Aceptar");
            return;
        }

        if (string.IsNullOrWhiteSpace(DireccionEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese la dirección.",
                "Aceptar");
            return;
        }

        if (CarreraPicker.SelectedIndex == -1)
        {
            await DisplayAlert(
                "Error",
                "Seleccione la carrera.",
                "Aceptar");
            return;
        }

        _estudiante.NumeroCuenta =
            NumeroCuentaEntry.Text.Trim();

        _estudiante.NombreCompleto =
            NombreCompletoEntry.Text.Trim();

        _estudiante.Edad = edad;

        _estudiante.Sexo =
            SexoPicker.SelectedItem?.ToString()
            ?? string.Empty;

        _estudiante.Correo =
            CorreoEntry.Text.Trim();

        _estudiante.Telefono =
            TelefonoEntry.Text.Trim();

        _estudiante.Direccion =
            DireccionEntry.Text.Trim();

        _estudiante.Carrera =
            CarreraPicker.SelectedItem?.ToString()
            ?? string.Empty;;

        _estudiante.FechaRegistro =
            FechaRegistroPicker.Date ?? DateTime.Now;

        await _databaseService.ActualizarEstudianteAsync(
            _estudiante);

        await DisplayAlert(
            "Actualización exitosa",
            "El estudiante fue actualizado correctamente.",
            "Aceptar");

        await Navigation.PopAsync();
    }

    private async void OnCancelarClicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PopAsync();
    }
}