using System.Windows;
using CardInput.ViewModels;

namespace CardInput.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}