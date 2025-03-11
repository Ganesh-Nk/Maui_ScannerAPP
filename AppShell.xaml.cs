using Maui_Shopping_APP.ViewModels;
using Maui_Shopping_APP.Views;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui_Shopping_APP
{
    public partial class AppShell : Shell
    {
        private readonly CartViewModel _cartViewModel;
        public ICommand ViewCartCommand { get; }
        public ICommand LogoutCommand { get; }

        public AppShell()
        {
            InitializeComponent();
            _cartViewModel = new CartViewModel();

            ViewCartCommand = new Command(ExecuteViewCart);
            LogoutCommand = new Command(ExecuteLogout);

            BindingContext = this;
        }

        private void ExecuteViewCart()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    await Navigation.PushAsync(new CartPage(_cartViewModel));
                    Current.FlyoutIsPresented = false;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            });
        }

        private void ExecuteLogout()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
                    if (confirm)
                    {
                        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
                        await Shell.Current.GoToAsync("//login");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            });
        }
    }
}
