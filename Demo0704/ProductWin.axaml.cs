using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Demo0704.Context;
using Demo0704.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Demo0704;

public partial class ProductWin : Window
{
    private PostgresContext _con;
    private User _user;
    public ProductWin()
    {
        InitializeComponent();
        _con = new PostgresContext();

        OrderDiscount.SelectedIndex = 0;
        OrderPrice.SelectedIndex = 0;
        GetProduct();
    }

    public ProductWin(User user)
    {
        InitializeComponent();
        _con = new PostgresContext();
        DataContext = user;
        _user = user;
        OrderDiscount.SelectedIndex = 0;
        OrderPrice.SelectedIndex = 0;
        GetProduct();
    }

    public void GetProduct()
    {
        var con = new PostgresContext();

        var _products = con.Products.Include(x => x.IdMakerNavigation).ToList();

        //для поиска
        if (!string.IsNullOrEmpty(SearchBox.Text))
        {
            _products = _products.Where(x => (x.Name?.ToLower().Contains(SearchBox.Text.ToLower()) ?? false) ||
            (x.Discription?.ToLower().Contains(SearchBox.Text.ToLower()) ?? false) ||
            (x.IdMakerNavigation?.Name?.ToLower().Contains(SearchBox.Text.ToLower()) ?? false)).ToList();
        }

        //Сортировка по цене
        if (OrderPrice.SelectedIndex != -1)
        {
            if (OrderPrice.SelectedIndex == 0)
            {
                _products = _products.OrderBy(x => x.Price).ToList();
            }
            else if (OrderPrice.SelectedIndex == 1)
            {
                _products = _products.OrderByDescending(x => x.Price).ToList();
            }
        }


        //Фильтрация по скидке
        if (OrderDiscount.SelectedIndex != 1 || OrderDiscount.SelectedIndex == 0)
        {
            if (OrderDiscount.SelectedIndex == 1)
            {
                _products = _products.Where(x => x.DiscountNow <= 9.99).ToList();
            }
            else if (OrderDiscount.SelectedIndex == 2)
            {
                _products = _products.Where(x => x.DiscountNow == 10 && x.DiscountNow <= 14.99).ToList();
            }
            else if (OrderDiscount.SelectedIndex == 3)
            {
                _products = _products.Where(x => x.DiscountNow >= 15).ToList();
            }
        }


        //Вывод списка продуктов
        ListProducts.ItemsSource = _products;

       

    }

    private void ComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        GetProduct();
    }

    private void OrderDiscount_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        GetProduct();
    }

    private void SearchBox_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        GetProduct();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new AddWin().ShowDialog(this);
        GetProduct();
    }

    private async void ListProducts_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {

        if (ListProducts.SelectedItem is Product selectedProduct)
        {
            _con.Entry(selectedProduct).State = EntityState.Detached;
            await new AddWin(selectedProduct).ShowDialog(this);
            GetProduct();
            ListProducts.SelectedItem = null;
        }

    }
}