using Maui_Shopping_APP.Views;

namespace Maui_Shopping_APP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("main", typeof(MainPage));
            Routing.RegisterRoute("cart", typeof(CartPage));
        }
    }
}
