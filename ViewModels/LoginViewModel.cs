using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui_Shopping_APP.Views;
using System.Windows.Input;

namespace Maui_Shopping_APP.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private string _cashierId;
        public string CashierId
        {
            get => _cashierId;
            set => SetProperty(ref _cashierId, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand OnLoginAsyncCommand { get; }

        public LoginViewModel()
        {
            _cashierId = string.Empty;
            _password = string.Empty;
            OnLoginAsyncCommand = new AsyncRelayCommand(OnLoginAsync);
        }

        private async Task OnLoginAsync()
        {
            if (string.IsNullOrWhiteSpace(CashierId) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Login Failed", "Cashier ID and Password are required.", "OK");
                return;
            }

            if (CashierId == "admin" && Password == "1234")
            {
                try
                {
                    Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                    await Shell.Current.GoToAsync("//main");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Navigation Error", ex.Message, "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Login Failed", "Invalid credentials. Try again.", "OK");
            }
        }
    }
}
