using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Maui_Shopping_APP.Messages;
using Maui_Shopping_APP.Views;
namespace Maui_Shopping_APP.ViewModels
    
{
    namespace Maui_Shopping_APP.ViewModels
    {
        public class AddItemViewModel : INotifyPropertyChanged
        {
            private int _cartItemCount;
            private bool _isCartBadgeVisible;

            public event PropertyChangedEventHandler PropertyChanged;

            public AddItemViewModel()
            {
                AddItemsCommand = new RelayCommand(AddItems);
                ViewCartCommand = new RelayCommand(ViewCart);
                WeakReferenceMessenger.Default.Register<Messages.CartBadgeMessage>(this, (recipient, message) =>
                {
                    CartItemCount = message.ItemCount; 
                    IsCartBadgeVisible = CartItemCount > 0;
                });
            }

            public int CartItemCount
            {
                get => _cartItemCount;
                set
                {
                    if (_cartItemCount != value)
                    {
                        _cartItemCount = value;
                        OnPropertyChanged(nameof(CartItemCount));
                    }
                }
            }

            public bool IsCartBadgeVisible
            {
                get => _isCartBadgeVisible;
                set
                {
                    if (_isCartBadgeVisible != value)
                    {
                        _isCartBadgeVisible = value;
                        OnPropertyChanged(nameof(IsCartBadgeVisible));
                    }
                }
            }

            public ICommand AddItemsCommand { get; }
            private void AddItems() { }

            public ICommand ViewCartCommand { get; }
            private async void ViewCart() { await Shell.Current.GoToAsync(nameof(CartPage)); }

            protected void OnPropertyChanged(string propertyName) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
