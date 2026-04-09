using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Demo0704.Models;

public partial class Product
{
    public int Id { get; set; }

    
    public string Articul { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int IdIzmerenie { get; set; }

    public decimal Price { get; set; }

    public float MaxDiscount { get; set; }

    public int IdMaker { get; set; }

    public int IdPointisyee { get; set; }

    public int IdCategory { get; set; }

    public float DiscountNow { get; set; }

    public float InSklad { get; set; }

    public string? Discription { get; set; }

    public string? Picture { get; set; }

    public virtual Category IdCategoryNavigation { get; set; } = null!;

    public virtual Izmerenie IdIzmerenieNavigation { get; set; } = null!;

    public virtual Maker IdMakerNavigation { get; set; } = null!;

    public virtual Pointisyee IdPointisyeeNavigation { get; set; } = null!;

    public virtual ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();


    //для смены 
    public Brush Borderbruh
    {
        get
        {
            if (DiscountNow >= 15)
            {
                return new SolidColorBrush(Color.Parse("#7fff00"));
                
            }
            return null;
        }
    }

    private Bitmap Image
    {
        get
        {
            if (Picture != null && Picture != "")
            {
                Bitmap bitmap = new Bitmap(Path.Combine(Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net9.0", ""), Picture));
                return bitmap;
            }
            else
            {
                return new Bitmap(Path.Combine(Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net9.0", ""), "Picture/picture.png"));
            }


        }
    }
}
