using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Demo0704.Models;

namespace Demo0704;

public partial class EditorWin : Window
{
    public EditorWin()
    {
        InitializeComponent();
    }

    public EditorWin(Product product)
    {
        InitializeComponent();
    }
}