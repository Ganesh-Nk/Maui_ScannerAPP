using System;

namespace Maui_Shopping_APP.Models
{
    public class Item
    {
        public string Name { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;

        public Item() { }

        public Item(string name, string barcode)
        {
            Name = name;
            Barcode = barcode;
        }

        public override string ToString() => $"{Name} ({Barcode})";
    }
}
