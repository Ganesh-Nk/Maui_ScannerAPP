using System;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using Maui_Shopping_APP.ViewModels.Maui_Shopping_APP.ViewModels;
using Maui_Shopping_APP.Messages;
using Microsoft.Maui.Layouts;

namespace Maui_Shopping_APP.Views
{
    public partial class AddItemPage : ContentPage
    {
        private int scannedItemCount = 0;

        public AddItemPage()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<CartBadgeMessage>(this, (recipient, message) =>
            {
                scannedItemCount = message.ItemCount;
                UpdateBadgeCounter();
            });
            BindingContext = new AddItemViewModel();
        }

        private void UpdateBadgeCounter()
        {
            if (BindingContext is AddItemViewModel viewModel)
            {
                viewModel.CartItemCount = scannedItemCount;
                viewModel.IsCartBadgeVisible = scannedItemCount > 0;
            }
        }
        private bool isScanning = false;

        private async void OnAddItemsClicked(object sender, EventArgs e)
        {
            if (isScanning) return;
            isScanning = true;

            var scanner = new CameraBarcodeReaderView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                AutomationId = "BarcodeScanner"
            };

            async void BarcodeDetectedHandler(object s, BarcodeDetectionEventArgs args)
            {
                if (!isScanning || args.Results.Length == 0)
                    return;

                isScanning = false; 
                scanner.IsDetecting = false; 
                scanner.BarcodesDetected -= BarcodeDetectedHandler; 
                string scannedBarcode = args.Results[0].Value;
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await Navigation.PopModalAsync();
                    await DisplayAlert("Success", $"Item scanned: {scannedBarcode}", "OK");
                    scannedItemCount++;
                    UpdateBadgeCounter();
                    WeakReferenceMessenger.Default.Send(new CartBadgeMessage(scannedItemCount));
                });
                scanner.Handler?.DisconnectHandler();
            }

            scanner.BarcodesDetected += BarcodeDetectedHandler;
            var scannerPage = new BarcodeScannerPage(scanner);
            await Navigation.PushModalAsync(scannerPage);
        }
        private async void OnViewCartClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CartPage));
        }
    }
    public class BarcodeScannerPage : ContentPage
    {
        public BarcodeScannerPage(CameraBarcodeReaderView scanner)
        {
            Title = "Scan Item";
            Content = new Grid
            {
                Children = { scanner }
            };
        }
    }
}
