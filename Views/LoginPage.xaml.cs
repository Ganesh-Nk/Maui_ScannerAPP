using Maui_Shopping_APP.ViewModels;

namespace Maui_Shopping_APP.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}
