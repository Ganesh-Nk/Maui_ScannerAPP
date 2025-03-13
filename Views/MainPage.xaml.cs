using CommunityToolkit.Mvvm.Messaging;
using Maui_Shopping_APP.ViewModels;
using Microsoft.Maui.Controls;
using System;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using Maui_Shopping_APP.Messages;
using Maui_Shopping_APP.Helpers;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using FFImageLoading.Maui;
using System.Timers;

namespace Maui_Shopping_APP.Views
{
    public partial class MainPage : ContentPage
    {
       
        private readonly CartViewModel _cartViewModel;
        private bool isScanning = false;

        public MainPage()
        {
            InitializeComponent();
            _cartViewModel = ServiceHelper.GetService<CartViewModel>();
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
            if (isScanning) return;
            isScanning = true;

            try
            {
                var scanner = new CameraBarcodeReaderView();
                scanner.BarcodesDetected += async (s, args) =>
                {
                    if (!isScanning || args.Results.Length == 0) return;
                    
                    isScanning = false;
                    scanner.IsDetecting = false;

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();
                        string barcode = args.Results[0].Value;
                        await DisplayAlert("Success", $"Barcode scanned: {barcode}", "OK");
                        _cartViewModel.AddItem(barcode);
                        WeakReferenceMessenger.Default.Send(new CartBadgeMessage(_cartViewModel.CartItemCount));
                    });

                    scanner.Handler?.DisconnectHandler();
                };

                await Navigation.PushAsync(new ContentPage { Content = scanner });
            }
            catch (Exception ex)
            {
                isScanning = false;
                await DisplayAlert("Error", $"Failed to initialize scanner: {ex.Message}", "OK");
            }
        }
    }
}
