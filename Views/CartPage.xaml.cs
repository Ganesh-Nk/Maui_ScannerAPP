using Microsoft.Maui.Controls;
using Maui_Shopping_APP.ViewModels;

namespace Maui_Shopping_APP.Views
{
    public partial class CartPage : ContentPage
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        private readonly CartViewModel ViewModel;

        public CartPage(CartViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel ?? new CartViewModel();
            BindingContext = ViewModel;
        }

        public void AddItemToCart(string barcode)
        {
            ViewModel.AddItem(barcode);
        }
        public class CartItem
        {
            public required string Barcode { get; set; }
            public int Quantity { get; set; }
        }

    }
}
