using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Demo0704.Context;
using Demo0704.Models;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo0704;

public partial class AddWin : Window
{
    private DbUser10Context _cont;
    private Product _prod;
    public AddWin()
    {
        InitializeComponent();

        _cont = new DbUser10Context();
        Load();
        DataContext = new Product(); 

        SaveButton.Click += Button_Click_Add;


    }
    public AddWin(Product product)
    {
        InitializeComponent();

        _cont = new DbUser10Context();
        Load();
        _prod = product;
        DataContext = _prod;

        SaveButton.Click += Button_Click_Redact;
        
    }

    public void Load()
    {
        List<Maker> makers = _cont.Makers.ToList();
        List<Category> categories = _cont.Categories.ToList();
        List<Izmerenie> izmerenies = _cont.Izmerenies.ToList();
        List<Pointisyee> pointisyees = _cont.Pointisyees.ToList();

        Maker.ItemsSource = makers;
        Categ.ItemsSource = categories;
        Izmer.ItemsSource = izmerenies;
        Postav.ItemsSource = pointisyees;
    }

    private void Button_Click_Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var product = DataContext as Product;
        product.IdCategory = (Categ.SelectedItem as Category).Id;
        product.IdMaker = (Maker.SelectedItem as Maker).Id;
        product.IdIzmerenie = (Izmer.SelectedItem as Izmerenie).Id;
        product.IdPointisyee = (Postav.SelectedItem as Pointisyee).Id;

        try
        {
            _cont.Products.Add(product);
            _cont.SaveChanges();
            Close();
        }
        catch(Exception ex) 
        {
            var mess = MessageBoxManager.GetMessageBoxStandard("error", ex.Message).ShowAsync();
        }
        

    }
    private void Button_Click_Redact(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var product = DataContext as Product;
        product.IdCategory = (Categ.SelectedItem as Category).Id;
        product.IdMaker = (Maker.SelectedItem as Maker).Id;
        product.IdIzmerenie = (Izmer.SelectedItem as Izmerenie).Id;
        product.IdPointisyee = (Postav.SelectedItem as Pointisyee).Id;

        try
        {
            _cont.Products.Update(product);
            _cont.SaveChanges();
            Close();
        }
        catch (Exception ex)
        {

            var mess = MessageBoxManager.GetMessageBoxStandard("error", ex.Message).ShowAsync();
        }
    }
}