using CommunityToolkit.Mvvm.Messaging;
using Maui_Shopping_APP.ViewModels;
using Microsoft.Maui.Controls;
using System;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using Maui_Shopping_APP.Messages;


namespace Maui_Shopping_APP.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly CartViewModel _cartViewModel;

        public MainPage()
        {
            InitializeComponent();
            _cartViewModel = new CartViewModel();
            BindingContext = _cartViewModel;

        
            WeakReferenceMessenger.Default.Register<CartBadgeMessage>(this, (r, m) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _cartViewModel.CartItemCount = m.ItemCount;
                    _cartViewModel.IsCartBadgeVisible = m.ItemCount > 0;
                });
            });
        }
        private async void OnMenuClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Menu", "Cancel", null, "Cart Page", "Logout");

            if (action == "Cart Page")
            {
                await Navigation.PushAsync(new CartPage(_cartViewModel));
            }
            else if (action == "Logout")
            {
                bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
                if (Application.Current != null)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
         
                    throw new InvalidOperationException("Application.Current is null");
                }
            }
        }

        private async void OnCartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPage(_cartViewModel));
        }
        private async void OnScanBarcodeClicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = new CameraBarcodeReaderView();
                scanner.BarcodesDetected += async (s, args) =>
                {
                    if (args.Results.Length > 0)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await Navigation.PopAsync();
                            string barcode = args.Results[0].Value;
                            await DisplayAlert("Success", $"Barcode scanned: {barcode}", "OK");
                            _cartViewModel.AddItem(barcode);
                            WeakReferenceMessenger.Default.Send(new CartBadgeMessage(_cartViewModel.CartItemCount));
                        });
                    }
                };

                await Navigation.PushAsync(new ContentPage { Content = scanner });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to initialize scanner: {ex.Message}", "OK");
            }
        }
    }
}
