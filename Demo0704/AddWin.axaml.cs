using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Demo0704.Context;
using Demo0704.Models;

namespace Demo0704;

public partial class AddWin : Window
{
    private DbUser10Context _cont;
    private Product _prod;
    public AddWin()
    {
        InitializeComponent();
        _cont = new DbUser10Context();
        DataContext = _prod;
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        
        Close();

    }
}