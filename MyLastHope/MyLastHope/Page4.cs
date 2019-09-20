using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using SQLite;
using System.IO;

namespace MyLastHope
{
    public class Page4 : ContentPage
    {
        List<Test> rock { get; set; }
        Test test = new Test();

        Switch accept = new Switch
        {
            IsToggled = false,
            OnColor = Color.White,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center
        };

        public Page4()
        {

            //Параметры страницы
            Title = "Добавление";
            IconImageSource = "Accept.png";
            BackgroundColor = Color.FromHex("1E1E1E");

            ListView listView = new ListView() { };


            StackLayout stackLayout = new StackLayout()
            {
                Children = { accept, listView }

            };

            accept.Toggled += switcher_Toggled;
            this.Content = stackLayout;

            void switcher_Toggled(object sender, ToggledEventArgs e)
            {
                rock = new List<Test>() { };

                rock = new List<Test>
                    {

                        new Test {Id=0, ImagePath="Show.png", Name="Мишка",Type="Мармелад",Price = 50},
                        new Test {Id=1, ImagePath="Bear.jpg", Name="Зайка",Type="Мармелад",Price = 50},
                        new Test {Id=2, ImagePath="Bear.png", Name="Птичка",Type="Мармелад",Price = 50},
                        new Test {Id=3, ImagePath="bear.png", Name="Квадратики",Type="Леденец",Price = 30},
                        new Test {Id=4, ImagePath="Bear.png", Name="Треугольники",Type="Леденец",Price = 30},
                        new Test {Id=5, ImagePath="Bear.png", Name="Кольца",Type="Леденец",Price = 30}
                    };




                //for (int i = 0; i < rock.Count; i++)
                //{
                //    string dbfilename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MyCans.db3");
                //    SQLiteConnection sql = new SQLiteConnection(dbfilename);
                //    sql.CreateTable<Test>();
                //    var primal = sql.Table<Test>().OrderByDescending(c => c.Id).FirstOrDefault();
                //    test = new Test()
                //    {
                //        Id = (primal == null ? 1 : primal.Id + 1),
                //        Name = rock[i].Name,
                //        Type = rock[i].Type,
                //        ImagePath = rock[i].ImagePath,
                //        Price = Convert.ToInt32(rock[i].Price),
                //    };
                //    sql.Insert(test);
                //}


            }

        }
    }
}
