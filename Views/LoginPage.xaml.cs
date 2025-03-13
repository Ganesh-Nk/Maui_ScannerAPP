using Maui_Shopping_APP.ViewModels;
using System.Threading.Tasks; 

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
