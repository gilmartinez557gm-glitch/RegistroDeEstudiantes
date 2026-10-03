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
}