using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Maui_Shopping_APP.Messages;

namespace Maui_Shopping_APP.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        private ObservableCollection<CartItem> _items;
        public ObservableCollection<CartItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private int _cartItemCount;
        public int CartItemCount
        {
            get => _cartItemCount;
            set => SetProperty(ref _cartItemCount, value);
        }

        private bool _isCartBadgeVisible;
        public bool IsCartBadgeVisible
        {
            get => _isCartBadgeVisible;
            set => SetProperty(ref _isCartBadgeVisible, value);
        }

        public ICommand RemoveItemCommand { get; }

        public CartViewModel()
        {
            _items = new ObservableCollection<CartItem>();
            RemoveItemCommand = new Command<CartItem>(RemoveItem);
            CartItemCount = 0;
            IsCartBadgeVisible = false;
        }

        private void RemoveItem(CartItem item)
        {
            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    Items.Remove(item);
                }
                CartItemCount = Items.Sum(i => i.Quantity);
                IsCartBadgeVisible = CartItemCount > 0;
                WeakReferenceMessenger.Default.Send(new CartBadgeMessage(CartItemCount));
            }
        }

        public void AddItem(string barcode)
        {
            var existingItem = Items.FirstOrDefault(i => i.Barcode == barcode);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                Items.Add(new CartItem { Barcode = barcode, Quantity = 1 });
            }
            CartItemCount = Items.Sum(i => i.Quantity);
            IsCartBadgeVisible = CartItemCount > 0;
            WeakReferenceMessenger.Default.Send(new CartBadgeMessage(CartItemCount));
        }
    }

    public class CartItem : ObservableObject
    {
        private string _barcode = string.Empty;
        public string Barcode
        {
            get => _barcode;
            set => SetProperty(ref _barcode, value);
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
    }
}
