using RegistroDeEstudiantes.Data;

namespace RegistroDeEstudiantes.Views;

public partial class EstudiantesPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public EstudiantesPage()
    {
        InitializeComponent();

        _databaseService = new DatabaseService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _databaseService.InitializeAsync();

        await CargarEstudiantesAsync();
    }

    private async Task CargarEstudiantesAsync()
    {
        var estudiantes =
            await _databaseService.ObtenerEstudiantesAsync();

        EstudiantesCollectionView.ItemsSource = estudiantes;
    }
    private async void OnEditarEstudianteClicked(
    object sender,
    EventArgs e)
    {
        var button = (Button)sender;

        var estudiante = (Models.Estudiante)button.BindingContext;

        await Navigation.PushAsync(
            new EditarEstudiantePage(estudiante));
    }
    private async void OnEliminarEstudianteClicked(
    object sender,
    EventArgs e)
    {
        var button = (Button)sender;

        var estudiante = (Models.Estudiante)button.BindingContext;

        bool confirmar = await DisplayAlert(
            "Confirmar eliminación",
            $"¿Desea eliminar a {estudiante.NombreCompleto}?",
            "Sí",
            "No");

        if (!confirmar)
            return;

        await _databaseService.EliminarEstudianteAsync(estudiante);

        await DisplayAlert(
            "Eliminado",
            "El estudiante fue eliminado correctamente.",
            "Aceptar");

        await CargarEstudiantesAsync();
    }
}