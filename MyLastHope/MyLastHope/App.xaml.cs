using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyLastHope
{
    public partial class App : Application
    {
        [Obsolete]
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SplagePage());
            //MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("111111"));
            //NavigationPage.SetTitleIconImageSource(this, "Candy.png");

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
