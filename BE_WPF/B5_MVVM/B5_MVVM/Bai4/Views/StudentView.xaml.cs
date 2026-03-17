using System.Windows;
using Bai4.ViewModels;

namespace Bai4.Views;

public partial class StudentView : Window
{
    private const string JsonFileName = "data.json";

    public StudentView()
    {
        InitializeComponent();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        MainViewModel? viewModel = DataContext as MainViewModel;
        if (viewModel is not null)
        {
            viewModel.SaveToFile(JsonFileName);
        }
    }

    private void Load_Click(object sender, RoutedEventArgs e)
    {
        MainViewModel? viewModel = DataContext as MainViewModel;
        if (viewModel is not null)
        {
            viewModel.LoadFromFile(JsonFileName);
        }
    }
}
