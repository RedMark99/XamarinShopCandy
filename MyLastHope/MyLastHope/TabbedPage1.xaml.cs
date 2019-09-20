using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace MyLastHope
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : Xamarin.Forms.TabbedPage
    {
        public TabbedPage1()
        {


            NavigationPage.SetTitleIconImageSource(this, "Candy.png");
            Title = "Магазин сладостей";

            this.Children.Add(new Page1());
            this.Children.Add(new Page3());
            this.Children.Add(new Page2());
            //this.Children.Add(new Page4());




            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            BarBackgroundColor = Color.FromHex("111111");
            BarTextColor = Color.White;
            SelectedTabColor = Color.FromHex("894343");
            UnselectedTabColor = Color.White;



        }
    }
}