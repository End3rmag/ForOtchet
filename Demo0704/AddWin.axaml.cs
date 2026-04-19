using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Demo0704.Context;
using Demo0704.Models;
using MsBox.Avalonia;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Demo0704;

public partial class AddWin : Window
{
    private PostgresContext _cont;
    private PostgresContext _contlook;
    private Product _prod;
    private string _selectPhoto;
    private bool _isEditWin = false;
    public AddWin()
    {
        InitializeComponent();

        _cont = new PostgresContext();
        _contlook = new PostgresContext();
        _isEditWin = false;
        Load();
        DataContext = new Product();
        SaveButton.Click += Button_Click_Add;
        SelectImageButton.Click += SelectImageButton_Click;

    }
    public AddWin(Product product)
    {
        InitializeComponent();

        _cont = new PostgresContext();
        _contlook = new PostgresContext();
        _prod = product;
        _isEditWin = true;
        Load();
        DataContext = _prod;
        _selectPhoto = _prod.Picture;
        SaveItemComboBox();
        SaveButton.Click += Button_Click_Redact;
        SelectImageButton.Click += SelectImageButton_Click;


    }

    public void Load()
    {
        List<Maker> makers = _contlook.Makers.ToList();
        List<Category> categories = _contlook.Categories.ToList();
        List<Izmerenie> izmerenies = _contlook.Izmerenies.ToList();
        List<Postavshik> pointisyees = _contlook.Postavshiks.ToList();

        Maker.ItemsSource = makers;
        Categ.ItemsSource = categories;
        Izmer.ItemsSource = izmerenies;
        Postav.ItemsSource = pointisyees;

        if(_isEditWin == true)
        {
            Titles.Text = "Окно Редактирования";
        }
        else
        {
            Titles.Text = "Окно Добавления";
        }
    }

    public void SaveItemComboBox()
    {
        if(_prod != null)
        {
            if(Maker.ItemsSource is List<Maker> makers)
            {
                Maker.SelectedItem = makers.FirstOrDefault(m => m.Id == _prod.IdMaker);
            }

            if (Categ.ItemsSource is List<Category> categories)
            {
                Categ.SelectedItem = categories.FirstOrDefault(m => m.Id == _prod.IdCategory);
            }

            if (Izmer.ItemsSource is List<Izmerenie> izmerenies)
            {
                Izmer.SelectedItem = izmerenies.FirstOrDefault(m => m.Id == _prod.IdIzmerenie);
            }

            if (Postav.ItemsSource is List<Postavshik> pointisyees)
            {
                Postav.SelectedItem = pointisyees.FirstOrDefault(m => m.Id == _prod.IdPostavshik);
            }
        }
    }


    private async void SelectImageButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var files = await StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
        {
            Title = "Выберите фото",
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new FilePickerFileType("Images")
                {
                    Patterns = new[] {"*.png","*.jpeg","*.jpg","*.bmp","*.gif"},
                    MimeTypes = new[] {"image/png", "image/jpeg" }
                } 
            }
        });
        if(files.Count() > 0)
        {
            var file = files[0];
            var source = file.Path.LocalPath;

            var path = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net9.0", "");
            var folder = Path.Combine(path, "Picture");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileExtension = Path.GetExtension(source);
            string fileName = $"{Guid.NewGuid()}{fileExtension}";
            string destPath = Path.Combine(folder, fileName);

            File.Copy(source, destPath, true);

            _selectPhoto = $"Picture/{fileName}";

            var product = DataContext as Product;
            if (product != null)
            {
                product.Picture = _selectPhoto;
            }

            ProductImage.Source = new Bitmap(destPath);
            ImagePathText.Text = _selectPhoto;
        }
    }


    private void Button_Click_Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var product = DataContext as Product;
        product.IdCategory = (Categ.SelectedItem as Category).Id;
        product.IdMaker = (Maker.SelectedItem as Maker).Id;
        product.IdIzmerenie = (Izmer.SelectedItem as Izmerenie).Id;
        product.IdPostavshik = (Postav.SelectedItem as Postavshik).Id;
        product.Picture = _selectPhoto ;

        try
        {
                _cont.Products.Add(product);
                _cont.SaveChanges();
                Close();
        }
        catch (Exception ex)
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
        product.IdPostavshik = (Postav.SelectedItem as Postavshik).Id;
        if (!string.IsNullOrEmpty(_selectPhoto))
        {
            product.Picture = _selectPhoto;
        }


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